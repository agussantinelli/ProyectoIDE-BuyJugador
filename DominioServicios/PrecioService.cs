using Data;
using DominioModelo;
using Microsoft.EntityFrameworkCore;

namespace DominioServicios
{
    public class PrecioService
    {
        private readonly BuyJugadorContext _context;

        public PrecioService(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<Precio>> GetAllAsync()
        {
            return await _context.Precios.ToListAsync();
        }

        public async Task<Precio?> GetByIdAsync(int id)
        {
            return await _context.Precios.FindAsync(id);
        }

        public async Task<Precio> CreateAsync(Precio precio)
        {
            _context.Precios.Add(precio);
            await _context.SaveChangesAsync();
            return precio;
        }
    }
}
