using Data;
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
                }).ToListAsync();
        }

        public async Task<List<PrecioVentaDTO>> GetByProductoAsync(int idProducto)
        {
            return await _context.PreciosVenta
                .Include(pv => pv.Producto)
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

        public async Task<PrecioVentaDTO?> GetByIdAsync(int idProducto, DateTime fechaDesde)
        {
            var e = await _context.PreciosVenta
                .Include(pv => pv.Producto)
                .FirstOrDefaultAsync(x => x.IdProducto == idProducto && x.FechaDesde == fechaDesde);

            return PrecioVentaDTO.FromDominio(e);
        }

        public async Task<PrecioVentaDTO> CreateAsync(PrecioVentaDTO dto)
        {
            var e = dto.ToDominio();
            _context.PreciosVenta.Add(e);
            await _context.SaveChangesAsync();
            return PrecioVentaDTO.FromDominio(e)!;
        }

        public async Task<bool> DeleteAsync(int idProducto, DateTime fechaDesde)
        {
            var e = await _context.PreciosVenta.FindAsync(idProducto, fechaDesde);
            if (e == null) return false;

            _context.PreciosVenta.Remove(e);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
