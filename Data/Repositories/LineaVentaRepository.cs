using Data;
using DominioModelo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class LineaVentaRepository
    {
        private readonly BuyJugadorContext _context;

        public LineaVentaRepository(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<LineaVenta>> GetLineasByVentaIdAsync(int idVenta)
        {
            return await _context.LineaVentas
                .Where(l => l.IdVenta == idVenta)
                .Include(l => l.IdProductoNavigation)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<LineaVenta>> GetAllAsync()
        {
            return await _context.LineaVentas.ToListAsync();
        }

        public async Task<LineaVenta?> GetByIdAsync(int idVenta, int nroLineaVenta)
        {
            return await _context.LineaVentas.FindAsync(idVenta, nroLineaVenta);
        }

        public async Task AddAsync(LineaVenta entity)
        {
            await _context.LineaVentas.AddAsync(entity);
        }

        public void Remove(LineaVenta entity)
        {
            _context.LineaVentas.Remove(entity);
        }
    }
}
