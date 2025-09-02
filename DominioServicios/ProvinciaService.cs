using Data;
using DominioModelo;
using DTOs;
using Microsoft.EntityFrameworkCore;

namespace DominioServicios
{
    public class ProvinciaService
    {
        private readonly BuyJugadorContext _context;

        public ProvinciaService(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<ProvinciaDTO>> GetAllAsync()
        {
            var entidades = await _context.Provincias.ToListAsync();
            return entidades.Select(e => ProvinciaDTO.FromDominio(e)).ToList();
        }

        public async Task<ProvinciaDTO?> GetByIdAsync(int id)
        {
            var entidad = await _context.Provincias.FindAsync(id);
            return ProvinciaDTO.FromDominio(entidad);
        }

        public async Task<ProvinciaDTO> CreateAsync(ProvinciaDTO dto)
        {
            var entidad = dto.ToDominio();
            _context.Provincias.Add(entidad);
            await _context.SaveChangesAsync();
            return ProvinciaDTO.FromDominio(entidad);
        }

        public async Task UpdateAsync(int id, ProvinciaDTO dto)
        {
            var entidad = await _context.Provincias.FindAsync(id);
            if (entidad != null)
            {
                entidad.Nombre = dto.Nombre;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entidad = await _context.Provincias.FindAsync(id);
            if (entidad != null)
            {
                _context.Provincias.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }
    }
}
