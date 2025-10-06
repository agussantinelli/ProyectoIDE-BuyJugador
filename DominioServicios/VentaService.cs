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
        private readonly PrecioVentaService _precioVentaService;

        public VentaService(BuyJugadorContext context, PrecioVentaService precioVentaService)
        {
            _context = context;
            _precioVentaService = precioVentaService;
        }

        public async Task<Venta> CrearVentaCompletaAsync(CrearVentaCompletaDTO dto)
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

                foreach (var (lineaDto, index) in dto.Lineas.Select((value, i) => (value, i)))
                {
                    if (!lineaDto.IdProducto.HasValue)
                        throw new InvalidOperationException("Se intentó agregar un producto sin ID.");

                    var precioVigente = await _precioVentaService.GetPrecioVigenteAsync(lineaDto.IdProducto.Value);
                    if (precioVigente == null)
                        throw new InvalidOperationException($"El producto ID {lineaDto.IdProducto} no tiene un precio de venta vigente.");

                    var producto = await _context.Productos.FindAsync(lineaDto.IdProducto.Value);
                    if (producto == null || producto.Stock < lineaDto.Cantidad)
                        throw new InvalidOperationException($"Stock insuficiente para el producto ID {lineaDto.IdProducto}.");

                    var nuevaLinea = new LineaVenta
                    {
                        IdVenta = nuevaVenta.IdVenta,
                        NroLineaVenta = index + 1,
                        IdProducto = lineaDto.IdProducto,
                        Cantidad = lineaDto.Cantidad,
                        PrecioUnitario = precioVigente.Monto
                    };
                    _context.LineaVentas.Add(nuevaLinea);

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
        }

        public async Task UpdateVentaCompletaAsync(CrearVentaCompletaDTO dto)
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

                foreach (var lineaOriginal in ventaExistente.LineaVenta)
                {
                    var producto = await _context.Productos.FindAsync(lineaOriginal.IdProducto);
                    if (producto != null)
                    {
                        producto.Stock += lineaOriginal.Cantidad;
                    }
                }

                _context.LineaVentas.RemoveRange(ventaExistente.LineaVenta);
                await _context.SaveChangesAsync();

                int nroLineaCounter = 1;
                foreach (var lineaDto in dto.Lineas)
                {
                    if (!lineaDto.IdProducto.HasValue)
                        throw new InvalidOperationException("Se intentó agregar un producto sin ID.");

                    var precioVigente = await _precioVentaService.GetPrecioVigenteAsync(lineaDto.IdProducto.Value);
                    if (precioVigente == null)
                        throw new InvalidOperationException($"El producto ID {lineaDto.IdProducto} no tiene un precio vigente.");

                    var producto = await _context.Productos.FindAsync(lineaDto.IdProducto.Value);
                    if (producto == null || producto.Stock < lineaDto.Cantidad)
                        throw new InvalidOperationException($"Stock insuficiente para el producto ID {lineaDto.IdProducto}.");

                    var nuevaLinea = new LineaVenta
                    {
                        IdVenta = ventaExistente.IdVenta,
                        NroLineaVenta = nroLineaCounter++,
                        IdProducto = lineaDto.IdProducto,
                        Cantidad = lineaDto.Cantidad,
                        PrecioUnitario = precioVigente.Monto
                    };
                    _context.LineaVentas.Add(nuevaLinea);

                    producto.Stock -= lineaDto.Cantidad;
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