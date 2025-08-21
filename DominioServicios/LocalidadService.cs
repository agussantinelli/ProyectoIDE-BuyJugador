using Data;
using DominioModelo;
using Microsoft.EntityFrameworkCore;

namespace DominioServicios
{
    public class LocalidadService
    {
        private readonly BuyJugadorContext _context;

        public LocalidadService(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<Localidad>> GetAllAsync()
        {
            return await _context.Localidades.ToListAsync();
        }

        public async Task<Localidad?> GetByIdAsync(int id)
        {
            return await _context.Localidades.FindAsync(id);
        }
    }
}
