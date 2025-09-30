using Data;
using DominioModelo;
using DTOs;
using Microsoft.EntityFrameworkCore;

namespace DominioServicios
{
    public class ProveedorService
    {
        private readonly BuyJugadorContext _context;

        public ProveedorService(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<ProveedorDTO>> GetAllAsync()
        {
            var entidades = await _context.Proveedores.ToListAsync();
            return entidades.Select(e => ProveedorDTO.FromDominio(e)).ToList();
        }

        public async Task<ProveedorDTO?> GetByIdAsync(int id)
        {
            var entidad = await _context.Proveedores.FindAsync(id);
            return ProveedorDTO.FromDominio(entidad);
        }


        public async Task<ProveedorDTO> CreateAsync(ProveedorDTO dto)
        {
            var entidad = dto.ToDominio();
            _context.Proveedores.Add(entidad);
            await _context.SaveChangesAsync();
            return ProveedorDTO.FromDominio(entidad);
        }

        public async Task UpdateAsync(int id, ProveedorDTO dto)
        {
            var entidad = await _context.Proveedores.FindAsync(id);
            if (entidad != null)
            {
                entidad.RazonSocial = dto.RazonSocial;
                entidad.Email = dto.Email;
                entidad.Telefono = dto.Telefono;
                entidad.Direccion = dto.Direccion;
                entidad.IdLocalidad = dto.IdLocalidad;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ProveedorDTO>> GetInactivosAsync()
        {
            var entidades = await _context.Proveedores
                .IgnoreQueryFilters()       
                .Where(p => p.Activo == false)
                .ToListAsync();

            return entidades.Select(e => ProveedorDTO.FromDominio(e)).ToList();
        }

        public async Task ReactivarAsync(int id)
        {
            var entidad = await _context.Proveedores
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.IdProveedor == id);

            if (entidad != null)
            {
                entidad.Activo = true;
                await _context.SaveChangesAsync();
            }
        }

    }
}
