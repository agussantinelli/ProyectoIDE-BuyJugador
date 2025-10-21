using Data;
using DominioModelo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class PersonaRepository
    {
        private readonly BuyJugadorContext _context;

        public PersonaRepository(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Persona>> GetAllAsync()
        {
            return await _context.Personas
                .Include(p => p.IdLocalidadNavigation)
                    .ThenInclude(l => l.IdProvinciaNavigation)
                .ToListAsync();
        }

        public async Task<IEnumerable<Persona>> GetPersonasActivasParaReporteAsync()
        {
            return await _context.Personas
                .IgnoreQueryFilters()
                .Where(p => p.Estado == true)
                .OrderBy(p => p.NombreCompleto)
                .ToListAsync();
        }

        public async Task<IEnumerable<Persona>> GetInactivosAsync()
        {
            return await _context.Personas
                .IgnoreQueryFilters()
                .Where(p => !p.Estado)
                .Include(p => p.IdLocalidadNavigation)
                    .ThenInclude(l => l.IdProvinciaNavigation)
                .ToListAsync();
        }

        public async Task<Persona?> GetByIdAsync(int id)
        {
            return await _context.Personas
                .Include(p => p.IdLocalidadNavigation)
                    .ThenInclude(l => l.IdProvinciaNavigation)
                .FirstOrDefaultAsync(p => p.IdPersona == id);
        }

        public async Task<Persona?> GetByIdToReactivateAsync(int id)
        {
            return await _context.Personas
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.IdPersona == id);
        }

        public async Task<Persona?> GetByDniAsync(int dni)
        {
            return await _context.Personas
                .IgnoreQueryFilters()
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Dni == dni);
        }

        public async Task AddAsync(Persona entity)
        {
            await _context.Personas.AddAsync(entity);
        }

        public void Update(Persona entity)
        {
            _context.Personas.Update(entity);
        }
    }
}

