// DominioServicios/PersonaService.cs
using Data;
using DominioModelo;
using DTOs;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace DominioServicios
{
    public class PersonaService
    {
        private readonly BuyJugadorContext _context;

        public PersonaService(BuyJugadorContext context)
        {
            _context = context;
        }

        // # Obtiene todas las personas activas con su localidad y provincia.
        public async Task<IEnumerable<PersonaDTO>> GetAllAsync()
        {
            return await _context.Personas
                .Include(p => p.IdLocalidadNavigation)
                    .ThenInclude(l => l.IdProvinciaNavigation)
                .Select(p => PersonaDTO.FromDominio(p))
                .ToListAsync();
        }

        // # Obtiene todas las personas inactivas (borrado lógico).
        public async Task<IEnumerable<PersonaDTO>> GetInactivosAsync()
        {
            return await _context.Personas
                .IgnoreQueryFilters() // # Ignora el filtro global para buscar también los inactivos.
                .Where(p => !p.Estado)
                .Include(p => p.IdLocalidadNavigation)
                    .ThenInclude(l => l.IdProvinciaNavigation)
                .Select(p => PersonaDTO.FromDominio(p))
                .ToListAsync();
        }

        // # Obtiene una persona por su ID, incluyendo inactivos.
        public async Task<PersonaDTO?> GetByIdAsync(int id)
        {
            var persona = await _context.Personas
                .IgnoreQueryFilters()
                .Include(p => p.IdLocalidadNavigation)
                    .ThenInclude(l => l.IdProvinciaNavigation)
                .FirstOrDefaultAsync(p => p.IdPersona == id);

            return persona != null ? PersonaDTO.FromDominio(persona) : null;
        }

        // # Crea una nueva persona, aplicando hash a la contraseña.
        public async Task<PersonaDTO> CreateAsync(PersonaDTO personaDto)
        {
            var persona = new Persona
            {
                NombreCompleto = personaDto.NombreCompleto,
                Dni = personaDto.Dni,
                Email = personaDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(personaDto.Password),
                Telefono = personaDto.Telefono,
                Direccion = personaDto.Direccion,
                IdLocalidad = personaDto.IdLocalidad,
                FechaIngreso = personaDto.FechaIngreso,
                Estado = true
            };

            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();
            var dtoCreado = PersonaDTO.FromDominio(persona);
            dtoCreado.Password = null;
            return dtoCreado;
        }

        // Actualiza los datos de contacto de una persona.
        public async Task<bool> UpdateAsync(int id, PersonaDTO personaDto)
        {
            var persona = await _context.Personas.IgnoreQueryFilters().FirstOrDefaultAsync(p => p.IdPersona == id);
            if (persona == null) return false;

            persona.Email = personaDto.Email;
            persona.Telefono = personaDto.Telefono;
            persona.Direccion = personaDto.Direccion;
            persona.IdLocalidad = personaDto.IdLocalidad;

            await _context.SaveChangesAsync();
            return true;
        }

        // Realiza un borrado lógico de la persona.
        public async Task<bool> DeleteAsync(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona == null) return false;

            // Implementamos el borrado lógico.
            persona.Estado = false;
            await _context.SaveChangesAsync();
            return true;
        }

        // Reactiva una persona que fue borrada lógicamente.
        public async Task<bool> ReactivarAsync(int id)
        {
            var persona = await _context.Personas.IgnoreQueryFilters().FirstOrDefaultAsync(p => p.IdPersona == id);
            if (persona == null) return false;

            persona.Estado = true;
            await _context.SaveChangesAsync();
            return true;
        }

        // Valida las credenciales del usuario.
        public async Task<PersonaDTO?> LoginAsync(int dni, string password)
        {
            var persona = await _context.Personas
                .IgnoreQueryFilters()
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Dni == dni);

            if (persona == null || !persona.Estado || !BCrypt.Net.BCrypt.Verify(password, persona.Password))
            {
                return null; 
            }

            var personaDto = PersonaDTO.FromDominio(persona);
            personaDto.Password = null; 

            return personaDto;
        }
    }
}
