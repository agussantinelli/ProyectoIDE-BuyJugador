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
    public class PrecioVentaService
    {
        private readonly BuyJugadorContext _context;

        public PrecioVentaService(BuyJugadorContext context)
        {
            _context = context;
        }

        // # (NUEVO) Método para obtener el historial de precios para el reporte gráfico.
        // # Consulta todos los precios, los agrupa por producto y los ordena por fecha.
        public async Task<List<HistorialPrecioProductoDTO>> GetHistorialPreciosAsync()
        {
            var historial = await _context.PreciosVenta
                .AsNoTracking()
                .Include(pv => pv.Producto)
                .OrderBy(pv => pv.FechaDesde)
                .Select(pv => new
                {
                    pv.IdProducto,
                    NombreProducto = pv.Producto.Nombre,
                    pv.FechaDesde,
                    pv.Monto
                })
                .ToListAsync();

            // # Agrupamos en memoria para construir la estructura DTO final.
            var resultado = historial
                .GroupBy(h => new { h.IdProducto, h.NombreProducto })
                .Select(g => new HistorialPrecioProductoDTO
                {
                    IdProducto = g.Key.IdProducto,
                    NombreProducto = g.Key.NombreProducto,
                    Puntos = g.Select(p => new PrecioPuntoDTO
                    {
                        Fecha = p.FechaDesde,
                        Monto = p.Monto
                    }).ToList()
                })
                .ToList();

            return resultado;
        }

        public async Task<PrecioVenta?> GetPrecioVigenteAsync(int idProducto)
        {
            return await _context.PreciosVenta
                .AsNoTracking()
                .Where(pv => pv.IdProducto == idProducto && pv.FechaDesde <= DateTime.UtcNow)
                .OrderByDescending(pv => pv.FechaDesde)
                .FirstOrDefaultAsync();
        }

        public async Task<List<PrecioVentaDTO>> GetAllAsync()
        {
            return await _context.PreciosVenta
                .Include(pv => pv.Producto)
                .Select(pv => new PrecioVentaDTO
                {
                    IdProducto = pv.IdProducto,
                    FechaDesde = pv.FechaDesde,
                    Monto = pv.Monto,
                    NombreProducto = pv.Producto.Nombre
                })
                .ToListAsync();
        }

        public async Task<PrecioVentaDTO?> GetByIdAsync(int idProducto, DateTime fechaDesde)
        {
            var pv = await _context.PreciosVenta
                .Include(p => p.Producto)
                .FirstOrDefaultAsync(p => p.IdProducto == idProducto && p.FechaDesde == fechaDesde);

            if (pv == null) return null;

            return new PrecioVentaDTO
            {
                IdProducto = pv.IdProducto,
                FechaDesde = pv.FechaDesde,
                Monto = pv.Monto,
                NombreProducto = pv.Producto.Nombre
            };
        }

        public async Task<PrecioVentaDTO> CreateAsync(PrecioVentaDTO dto)
        {
            var entity = new PrecioVenta
            {
                IdProducto = dto.IdProducto,
                FechaDesde = dto.FechaDesde.ToUniversalTime(),
                Monto = dto.Monto
            };
            _context.PreciosVenta.Add(entity);
            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task UpdateAsync(int idProducto, DateTime fechaDesde, PrecioVentaDTO dto)
        {
            var entity = await _context.PreciosVenta.FirstOrDefaultAsync(p => p.IdProducto == idProducto && p.FechaDesde == fechaDesde);
            if (entity != null)
            {
                entity.Monto = dto.Monto;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int idProducto, DateTime fechaDesde)
        {
            var entity = await _context.PreciosVenta.FirstOrDefaultAsync(p => p.IdProducto == idProducto && p.FechaDesde == fechaDesde);
            if (entity != null)
            {
                _context.PreciosVenta.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}