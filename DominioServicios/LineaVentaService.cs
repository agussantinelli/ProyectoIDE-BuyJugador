using Data;
using DominioModelo;
using Microsoft.EntityFrameworkCore;

namespace DominioServicios
{
    public class LineaVentaService
    {
        private readonly BuyJugadorContext _context;

        public LineaVentaService(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<LineaVenta>> GetAllAsync()
        {
            return await _context.LineasVenta.ToListAsync();
        }

        public async Task<LineaVenta?> GetByIdAsync(int id)
        {
            return await _context.LineasVenta.FindAsync(id);
        }

        public async Task<LineaVenta> CreateAsync(LineaVenta lineaVenta)
        {
            _context.LineasVenta.Add(lineaVenta);
            await _context.SaveChangesAsync();
            return lineaVenta;
        }
    }
}
