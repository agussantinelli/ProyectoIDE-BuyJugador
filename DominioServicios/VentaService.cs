using Data;
using Microsoft.EntityFrameworkCore;
using DominioModelo;


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
                                 .Include(v => v.LineasVenta)
                                 .ToListAsync();
        }

        public async Task<Venta?> GetByIdAsync(int id)
        {
            return await _context.Ventas
                                 .Include(v => v.LineasVenta)
                                 .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Venta> CreateAsync(Venta venta)
        {
            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();
            return venta;
        }
    }
}
