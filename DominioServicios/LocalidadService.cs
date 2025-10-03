using Data;
using DTOs;
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

        public async Task<List<LocalidadDTO>> GetAllAsync()
        {
            var entidades = await _context.Localidades
                .Include(l => l.IdProvinciaNavigation)
                .ToListAsync();

            return entidades.Select(LocalidadDTO.FromDominio).ToList();
        }

        public async Task<LocalidadDTO?> GetByIdAsync(int id)
        {
            var entidad = await _context.Localidades
                .Include(l => l.IdProvinciaNavigation)
                .FirstOrDefaultAsync(l => l.IdLocalidad == id);

            return entidad == null ? null : LocalidadDTO.FromDominio(entidad);
        }

        public async Task<LocalidadDTO> CreateAsync(LocalidadDTO dto)
        {
            var entidad = dto.ToDominio();
            _context.Localidades.Add(entidad);
            await _context.SaveChangesAsync();
            return LocalidadDTO.FromDominio(entidad);
        }

        public async Task UpdateAsync(int id, LocalidadDTO dto)
        {
            var entidad = await _context.Localidades.FindAsync(id);
            if (entidad != null)
            {
                entidad.Nombre = dto.Nombre;
                entidad.IdProvincia = dto.IdProvincia;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entidad = await _context.Localidades.FindAsync(id);
            if (entidad != null)
            {
                _context.Localidades.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<LocalidadDTO>> GetAllOrderedAsync()
        {
            var entidades = await _context.Localidades
                .Include(l => l.IdProvinciaNavigation)
                .OrderBy(l => l.Nombre)
                .ToListAsync();

            return entidades.Select(LocalidadDTO.FromDominio).ToList();
        }

    }
}
