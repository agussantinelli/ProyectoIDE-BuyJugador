using Data;
using DominioModelo;
using DTOs;
using Microsoft.EntityFrameworkCore;

namespace DominioServicios
{
    public class PersonaService
    {
        private readonly BuyJugadorContext _context;

        public PersonaService(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<PersonaDTO>> GetAllAsync()
        {
            var entidades = await _context.Personas.ToListAsync();
            return entidades.Select(p => PersonaDTO.FromDominio(p)).ToList();
        }

        public async Task<PersonaDTO?> GetByIdAsync(int id)
        {
            var entidad = await _context.Personas.FindAsync(id);
            return PersonaDTO.FromDominio(entidad);
        }

        public async Task<PersonaDTO> CreateAsync(PersonaDTO dto)
        {
            var entidad = dto.ToDominio();
            _context.Personas.Add(entidad);
            await _context.SaveChangesAsync();
            return PersonaDTO.FromDominio(entidad);
        }

        public async Task UpdateAsync(int id, PersonaDTO dto)
        {
            var entidad = await _context.Personas.FindAsync(id);
            if (entidad != null)
            {
                entidad.NombreCompleto = dto.NombreCompleto;
                entidad.Dni = dto.Dni;
                entidad.Email = dto.Email;
                entidad.Password = dto.Password;
                entidad.Telefono = dto.Telefono;
                entidad.Direccion = dto.Direccion;
                entidad.IdLocalidad = dto.IdLocalidad;
                entidad.FechaIngreso = dto.FechaIngreso;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entidad = await _context.Personas.FindAsync(id);
            if (entidad != null)
            {
                _context.Personas.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }
    }
}
