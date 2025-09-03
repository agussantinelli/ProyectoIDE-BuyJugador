using Data;
using DominioModelo;
using DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return await _context.Provincias
                .Select(p => new ProvinciaDTO { IdProvincia = p.IdProvincia, Nombre = p.Nombre })
                .ToListAsync();
        }

        public async Task<ProvinciaDTO?> GetByIdAsync(int id)
        {
            var provincia = await _context.Provincias.FindAsync(id);
            return provincia != null ? new ProvinciaDTO { IdProvincia = provincia.IdProvincia, Nombre = provincia.Nombre } : null;
        }

        public async Task<ProvinciaDTO> CreateAsync(ProvinciaDTO provinciaDto)
        {
            var provincia = new Provincia { Nombre = provinciaDto.Nombre };
            _context.Provincias.Add(provincia);
            await _context.SaveChangesAsync();
            provinciaDto.IdProvincia = provincia.IdProvincia;
            return provinciaDto;
        }

        public async Task<bool> UpdateAsync(int id, ProvinciaDTO provinciaDto)
        {
            var provincia = await _context.Provincias.FindAsync(id);
            if (provincia == null) return false;
            provincia.Nombre = provinciaDto.Nombre;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var provincia = await _context.Provincias.FindAsync(id);
            if (provincia == null) return false;
            _context.Provincias.Remove(provincia);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

