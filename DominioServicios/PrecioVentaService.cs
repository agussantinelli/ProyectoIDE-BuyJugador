using Data;
using DominioModelo;
using DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
                FechaDesde = dto.FechaDesde,
                Monto = dto.Monto
            };

            _context.PreciosVenta.Add(entity);
            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task UpdateAsync(int idProducto, DateTime fechaDesde, PrecioVentaDTO dto)
        {
            var entity = await _context.PreciosVenta
                .FirstOrDefaultAsync(p => p.IdProducto == idProducto && p.FechaDesde == fechaDesde);

            if (entity != null)
            {
                entity.Monto = dto.Monto;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int idProducto, DateTime fechaDesde)
        {
            var entity = await _context.PreciosVenta
                .FirstOrDefaultAsync(p => p.IdProducto == idProducto && p.FechaDesde == fechaDesde);

            if (entity != null)
            {
                _context.PreciosVenta.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
