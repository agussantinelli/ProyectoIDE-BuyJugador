using Data;
using DominioModelo;
using DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DominioServicios
{
    public class VentaService
    {
        private readonly BuyJugadorContext _context;

        public VentaService(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<Venta> CrearVentaCompletaAsync(CrearVentaCompletaDTO dto)
        {
            var strategy = _context.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var nuevaVenta = new Venta
                    {
                        IdPersona = dto.IdPersona,
                        Fecha = DateTime.UtcNow,
                        Estado = dto.Finalizada ? "Finalizada" : "Pendiente",
                    };
                    _context.Ventas.Add(nuevaVenta);
                    await _context.SaveChangesAsync(); 

                    var idsProductos = dto.Lineas.Where(l => l.IdProducto.HasValue).Select(l => l.IdProducto!.Value).Distinct().ToList();
                    var productosAfectados = await _context.Productos
                        .Include(p => p.PreciosVenta)
                        .Where(p => idsProductos.Contains(p.IdProducto) && p.Activo)
                        .ToDictionaryAsync(p => p.IdProducto);

                    int nroLineaCounter = 1;
                    foreach (var lineaDto in dto.Lineas)
                    {
                        if (!lineaDto.IdProducto.HasValue || !productosAfectados.TryGetValue(lineaDto.IdProducto.Value, out var producto))
                        {
                            throw new InvalidOperationException($"El producto ID {lineaDto.IdProducto} no es válido o no está activo.");
                        }

                        if (producto.Stock < lineaDto.Cantidad)
                        {
                            throw new InvalidOperationException($"Stock insuficiente para '{producto.Nombre}'. Disponible: {producto.Stock}, Solicitado: {lineaDto.Cantidad}.");
                        }

                        var precioVigente = producto.PreciosVenta
                            .OrderByDescending(p => p.FechaDesde)
                            .FirstOrDefault()?.Monto;

                        if (!precioVigente.HasValue)
                        {
                            throw new InvalidOperationException($"El producto '{producto.Nombre}' no tiene un precio de venta vigente.");
                        }

                        _context.LineaVentas.Add(new LineaVenta
                        {
                            IdVenta = nuevaVenta.IdVenta,
                            NroLineaVenta = nroLineaCounter++,
                            IdProducto = lineaDto.IdProducto,
                            Cantidad = lineaDto.Cantidad,
                            PrecioUnitario = precioVigente.Value
                        });

                        producto.Stock -= lineaDto.Cantidad;
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return nuevaVenta;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }

        public async Task UpdateVentaCompletaAsync(CrearVentaCompletaDTO dto)
        {
            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var ventaExistente = await _context.Ventas
                        .Include(v => v.LineaVenta)
                        .FirstOrDefaultAsync(v => v.IdVenta == dto.IdVenta);

                    if (ventaExistente == null)
                        throw new KeyNotFoundException("La venta que intenta modificar no existe.");

                    if (ventaExistente.Estado.Equals("Finalizada", StringComparison.OrdinalIgnoreCase))
                        throw new InvalidOperationException("No se puede modificar una venta que ya ha sido finalizada.");

                    var cantidadesOriginales = ventaExistente.LineaVenta.ToDictionary(l => l.IdProducto.Value, l => l.Cantidad);
                    var cantidadesNuevas = dto.Lineas.ToDictionary(l => l.IdProducto.Value, l => l.Cantidad);
                    var idsProductosAfectados = cantidadesOriginales.Keys.Union(cantidadesNuevas.Keys).ToList();

                    var productosAfectados = await _context.Productos
                        .Where(p => idsProductosAfectados.Contains(p.IdProducto))
                        .ToDictionaryAsync(p => p.IdProducto);

                    foreach (var idProducto in idsProductosAfectados)
                    {
                        if (!productosAfectados.TryGetValue(idProducto, out var producto)) continue;

                        var cantidadOriginal = cantidadesOriginales.GetValueOrDefault(idProducto, 0);
                        var cantidadNueva = cantidadesNuevas.GetValueOrDefault(idProducto, 0);
                        var diferencia = cantidadNueva - cantidadOriginal;

                        var stockDisponible = producto.Stock + cantidadOriginal;
                        if (stockDisponible < cantidadNueva)
                        {
                            throw new InvalidOperationException($"Stock insuficiente para '{producto.Nombre}'. Disponible: {stockDisponible}, Solicitado: {cantidadNueva}.");
                        }
                        producto.Stock -= diferencia;
                    }

                    _context.LineaVentas.RemoveRange(ventaExistente.LineaVenta);
                    await _context.SaveChangesAsync();

                    int nroLineaCounter = 1;
                    foreach (var lineaDto in dto.Lineas)
                    {
                        _context.LineaVentas.Add(new LineaVenta
                        {
                            IdVenta = ventaExistente.IdVenta,
                            NroLineaVenta = nroLineaCounter++,
                            IdProducto = lineaDto.IdProducto,
                            Cantidad = lineaDto.Cantidad,
                            PrecioUnitario = lineaDto.PrecioUnitario
                        });
                    }

                    if (dto.Finalizada)
                    {
                        ventaExistente.Estado = "Finalizada";
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }

        public IQueryable<Venta> GetVentas()
        {
            return _context.Ventas.Include(v => v.IdPersonaNavigation);
        }

        public async Task<Venta?> GetVentaByIdAsync(int id)
        {
            return await _context.Ventas
                .Include(v => v.IdPersonaNavigation)
                .Include(v => v.LineaVenta)
                    .ThenInclude(lv => lv.IdProductoNavigation)
                        .ThenInclude(p => p.PreciosVenta)
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.IdVenta == id);
        }

        public async Task<bool> DeleteVentaAsync(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null) return false;

            _context.Ventas.Remove(venta);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

