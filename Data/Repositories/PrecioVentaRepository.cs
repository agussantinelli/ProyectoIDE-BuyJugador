using Data;
using DominioModelo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class PrecioVentaRepository
    {
        private readonly BuyJugadorContext _context;

        public PrecioVentaRepository(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<PrecioVenta>> GetHistorialPreciosAsync()
        {
            return await _context.PreciosVenta
                .AsNoTracking()
                .Include(pv => pv.Producto)
                .OrderBy(pv => pv.FechaDesde)
                .ToListAsync();
        }

        public async Task<PrecioVenta?> GetPrecioVigenteAsync(int idProducto)
        {
            return await _context.PreciosVenta
                .AsNoTracking()
                .Where(pv => pv.IdProducto == idProducto && pv.FechaDesde <= DateTime.UtcNow)
                .OrderByDescending(pv => pv.FechaDesde)
                .FirstOrDefaultAsync();
        }

        public async Task<List<PrecioVenta>> GetAllDetalladoAsync()
        {
            return await _context.PreciosVenta
                .Include(pv => pv.Producto)
                .ToListAsync();
        }

        public async Task<PrecioVenta?> GetByIdAsync(int idProducto, DateTime fechaDesde)
        {
            return await _context.PreciosVenta
                .Include(p => p.Producto)
                .FirstOrDefaultAsync(p => p.IdProducto == idProducto && p.FechaDesde == fechaDesde);
        }

        public async Task AddAsync(PrecioVenta entity)
        {
            await _context.PreciosVenta.AddAsync(entity);
        }

        public void Remove(PrecioVenta entity)
        {
            _context.PreciosVenta.Remove(entity);
        }
    }
}
