using Data;
using DominioModelo;
using Microsoft.EntityFrameworkCore;

namespace DominioServicios
{
    public class VentaService
    {
        private readonly BuyJugadorContext _context;

        public VentaService(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<Venta>> GetAllAsync()
        {
            return await _context.Ventas
                                 .Include(v => v.LineasVenta) // Incluye las líneas de venta relacionadas
                                 .ToListAsync();
        }

        public async Task<Venta?> GetByIdAsync(int id)
        {
            return await _context.Ventas
                                 .Include(v => v.LineasVenta)
                                 .FirstOrDefaultAsync(v => v.IdVenta == id);
        }

        public async Task<Venta> CreateAsync(Venta venta)
        {
            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();
            return venta;
        }
    }
}
