using Data;
using DominioModelo;
using DTOs;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<PrecioVentaDTO>> GetAllAsync()
        {
            return await _context.PreciosVenta
                .Select(pv => new PrecioVentaDTO
                {
                    IdProducto = pv.IdProducto,
                    FechaDesde = pv.FechaDesde,
                    Monto = pv.Monto,
                    NombreProducto = pv.Producto.Nombre
                }).ToListAsync();
        }

        public async Task<List<PrecioVentaDTO>> GetByProductoAsync(int idProducto)
        {
            return await _context.PreciosVenta
                .Where(pv => pv.IdProducto == idProducto)
                .OrderByDescending(pv => pv.FechaDesde)
                .Select(pv => new PrecioVentaDTO
                {
                    IdProducto = pv.IdProducto,
                    FechaDesde = pv.FechaDesde,
                    Monto = pv.Monto,
                    NombreProducto = pv.Producto.Nombre
                }).ToListAsync();
        }

        public async Task<PrecioVentaDTO> CreateAsync(PrecioVentaDTO dto)
        {
            var nuevo = new PrecioVenta
            {
                IdProducto = dto.IdProducto,
                FechaDesde = dto.FechaDesde,
                Monto = dto.Monto
            };

            _context.PreciosVenta.Add(nuevo);
            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int idProducto, DateTime fechaDesde)
        {
            var entity = await _context.PreciosVenta
                .FirstOrDefaultAsync(pv => pv.IdProducto == idProducto && pv.FechaDesde == fechaDesde);

            if (entity == null)
                return false;

            _context.PreciosVenta.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
