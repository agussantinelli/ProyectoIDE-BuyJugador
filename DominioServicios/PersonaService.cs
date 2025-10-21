using Data;
using DominioModelo;
using DTOs;
using BCrypt.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DominioServicios
{
    public class PersonaService
    {
        private readonly UnitOfWork _unitOfWork;

        public PersonaService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PersonaDTO>> GetAllAsync()
        {
            var personas = await _unitOfWork.PersonaRepository.GetAllAsync();
            return personas.Select(p => PersonaDTO.FromDominio(p));
        }

        public async Task<IEnumerable<PersonaSimpleDTO>> GetPersonasActivasParaReporteAsync()
        {
            var personas = await _unitOfWork.PersonaRepository.GetPersonasActivasParaReporteAsync();
            return personas.Select(p => new PersonaSimpleDTO
            {
                IdPersona = p.IdPersona,
                NombreCompleto = p.NombreCompleto
            });
        }

        public async Task<IEnumerable<PersonaDTO>> GetInactivosAsync()
        {
            var personas = await _unitOfWork.PersonaRepository.GetInactivosAsync();
            return personas.Select(PersonaDTO.FromDominio);
        }

        public async Task<PersonaDTO?> GetByIdAsync(int id)
        {
            var persona = await _unitOfWork.PersonaRepository.GetByIdAsync(id);
            return persona != null ? PersonaDTO.FromDominio(persona) : null;
        }

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

            await _unitOfWork.PersonaRepository.AddAsync(persona);
            await _unitOfWork.SaveChangesAsync();

            var dtoCreado = PersonaDTO.FromDominio(persona);
            dtoCreado.Password = null;
            return dtoCreado;
        }

        public async Task<bool> UpdateAsync(int id, PersonaDTO personaDto)
        {
            var persona = await _unitOfWork.PersonaRepository.GetByIdAsync(id);
            if (persona == null) return false;

            persona.Email = personaDto.Email;
            persona.Telefono = personaDto.Telefono;
            persona.Direccion = personaDto.Direccion;
            persona.IdLocalidad = personaDto.IdLocalidad;

            _unitOfWork.PersonaRepository.Update(persona);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var persona = await _unitOfWork.PersonaRepository.GetByIdAsync(id);
            if (persona == null || !persona.Estado) return false;

            persona.Estado = false;
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ReactivarAsync(int id)
        {
            var persona = await _unitOfWork.PersonaRepository.GetByIdToReactivateAsync(id);
            if (persona == null || persona.Estado) return false;

            persona.Estado = true;
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<PersonaDTO?> LoginAsync(int dni, string password)
        {
            var persona = await _unitOfWork.PersonaRepository.GetByDniAsync(dni);

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