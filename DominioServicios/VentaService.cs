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

        public async Task<List<VentaDTO>> GetAllAsync()
        {
            var ventas = await _context.Ventas
                .Include(v => v.IdPersonaNavigation) 
                .Include(v => v.LineaVenta) 
                    .ThenInclude(lv => lv.IdProductoNavigation) 
                        .ThenInclude(p => p.Precios) 
                .AsNoTracking()
                .ToListAsync();

            return ventas.Select(v => {
                var ventaDto = VentaDTO.FromDominio(v);
                if (v.LineaVenta != null && v.LineaVenta.Any())
                {
                    ventaDto.Total = v.LineaVenta.Sum(lv =>
                    {
                        var precio = lv.IdProductoNavigation?.Precios?.OrderByDescending(p => p.FechaDesde).FirstOrDefault()?.Monto ?? 0;
                        return lv.Cantidad * precio;
                    });
                }
                return ventaDto;
            }).ToList();
        }

        public async Task<VentaDTO?> GetByIdAsync(int id)
        {
            var entidad = await _context.Ventas
                .Include(v => v.IdPersonaNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.IdVenta == id);

            if (entidad == null) return null;

            return VentaDTO.FromDominio(entidad);
        }

        public async Task<VentaDTO> CreateVentaCompletaAsync(CrearVentaCompletaDTO dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var nuevaVenta = new Venta
                {
                    Fecha = DateTime.Now,
                    Estado = "Pendiente",
                    IdPersona = dto.IdPersona
                };
                _context.Ventas.Add(nuevaVenta);
                await _context.SaveChangesAsync();

                int nroLinea = 1;
                foreach (var lineaDto in dto.Lineas)
                {
                    var producto = await _context.Productos.FindAsync(lineaDto.IdProducto);
                    if (producto == null || producto.Stock < lineaDto.Cantidad)
                    {
                        throw new InvalidOperationException($"Stock insuficiente para el producto ID: {lineaDto.IdProducto}.");
                    }
                    producto.Stock -= lineaDto.Cantidad;

                    var nuevaLinea = new LineaVenta
                    {
                        IdVenta = nuevaVenta.IdVenta,
                        NroLineaVenta = nroLinea++,
                        IdProducto = lineaDto.IdProducto,
                        Cantidad = lineaDto.Cantidad
                    };
                    _context.LineaVentas.Add(nuevaLinea);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return VentaDTO.FromDominio(nuevaVenta);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        public async Task UpdateAsync(int id, VentaDTO dto)
        {
            var entidad = await _context.Ventas.FindAsync(id);
            if (entidad != null)
            {
                entidad.Fecha = dto.Fecha;
                entidad.Estado = dto.Estado;
                entidad.IdPersona = dto.IdPersona;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var entidad = await _context.Ventas
                    .Include(v => v.LineaVenta)
                    .FirstOrDefaultAsync(v => v.IdVenta == id);

                if (entidad != null)
                {
                    foreach (var linea in entidad.LineaVenta)
                    {
                        if (linea.IdProducto.HasValue)
                        {
                            var producto = await _context.Productos.FindAsync(linea.IdProducto.Value);
                            if (producto != null)
                            {
                                producto.Stock += linea.Cantidad;
                            }
                        }
                    }

                    _context.Ventas.Remove(entidad);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                else
                {
                    await transaction.RollbackAsync();
                }
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}

