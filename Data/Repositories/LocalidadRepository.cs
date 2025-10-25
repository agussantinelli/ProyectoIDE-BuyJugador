using Data;
using DominioModelo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class LocalidadRepository
    {
        private readonly BuyJugadorContext _context;

        public LocalidadRepository(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<Localidad>> GetAllAsync()
        {
            return await _context.Localidades
                .Include(l => l.IdProvinciaNavigation)
                .ToListAsync();
        }

        public async Task<Localidad?> GetByIdAsync(int id)
        {
            return await _context.Localidades
                .Include(l => l.IdProvinciaNavigation)
                .FirstOrDefaultAsync(l => l.IdLocalidad == id);
        }

        public async Task AddAsync(Localidad entity)
        {
            await _context.Localidades.AddAsync(entity);
        }

        public async Task<List<Localidad>> GetAllOrderedByNameAsync()
        {
            return await _context.Localidades
                .Include(l => l.IdProvinciaNavigation)
                .OrderBy(l => l.Nombre)
                .ToListAsync();
        }
    }
}
