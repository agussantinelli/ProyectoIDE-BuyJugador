using Data;
using DominioModelo;
using Microsoft.EntityFrameworkCore;

namespace DominioServicios
{
    public class DuenioService
    {
        private readonly BuyJugadorContext _context;

        public DuenioService(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<Duenio>> GetAllAsync()
        {
            return await _context.Duenios.ToListAsync();
        }

        public async Task<Duenio?> GetByIdAsync(int id)
        {
            return await _context.Duenios.FindAsync(id);
        }

        public async Task<Duenio> CreateAsync(Duenio duenio)
        {
            _context.Duenios.Add(duenio);
            await _context.SaveChangesAsync();
            return duenio;
        }
    }
}
