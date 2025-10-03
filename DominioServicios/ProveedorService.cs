using Data;
using DominioModelo;
using DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            // Este método, debido al filtro global, solo traerá proveedores activos.
            return await _context.Proveedores
                .Select(p => new ProveedorDTO
                {
                    IdProveedor = p.IdProveedor,
                    RazonSocial = p.RazonSocial,
                    Cuit = p.Cuit,
                    Email = p.Email,
                    Telefono = p.Telefono,
                    Direccion = p.Direccion,
                    IdLocalidad = p.IdLocalidad,
                    Activo = p.Activo
                }).ToListAsync();
        }

        public async Task<List<ProveedorDTO>> GetInactivosAsync()
        {
            return await _context.Proveedores
                .IgnoreQueryFilters() // Ignora el filtro global para buscar entre TODOS los proveedores.
                .Where(p => !p.Activo)
                .Select(p => new ProveedorDTO
                {
                    IdProveedor = p.IdProveedor,
                    RazonSocial = p.RazonSocial,
                    Cuit = p.Cuit,
                    Email = p.Email,
                    Telefono = p.Telefono,
                    Direccion = p.Direccion,
                    IdLocalidad = p.IdLocalidad,
                    Activo = p.Activo
                }).ToListAsync();
        }

        public async Task<ProveedorDTO?> GetByIdAsync(int id)
        {
            // Este método solo encontrará proveedores activos por el filtro global.
            var p = await _context.Proveedores.FirstOrDefaultAsync(p => p.IdProveedor == id);
            if (p == null) return null;

            return ProveedorDTO.FromDominio(p);
        }

        // --- MÉTODO NUEVO AÑADIDO ---
        // Este método es necesario para poder buscar un proveedor para editarlo o reactivarlo,
        // sin importar si está activo o no.
        private async Task<Proveedor?> FindProveedorByIdAsync(int id)
        {
            return await _context.Proveedores
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.IdProveedor == id);
        }

        public async Task<ProveedorDTO> CreateAsync(ProveedorDTO dto)
        {
            var proveedor = new Proveedor
            {
                RazonSocial = dto.RazonSocial,
                Cuit = dto.Cuit,
                Email = dto.Email,
                Telefono = dto.Telefono,
                Direccion = dto.Direccion,
                IdLocalidad = dto.IdLocalidad,
                Activo = true
            };

            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();

            dto.IdProveedor = proveedor.IdProveedor;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, ProveedorDTO dto)
        {
            // --- CORRECCIÓN APLICADA ---
            // Usamos el nuevo método para asegurarnos de encontrar el proveedor aunque esté inactivo.
            var proveedor = await FindProveedorByIdAsync(id);
            if (proveedor == null)
            {
                return false; // Indica que no se encontró el proveedor.
            }

            proveedor.RazonSocial = dto.RazonSocial;
            proveedor.Cuit = dto.Cuit;
            proveedor.Email = dto.Email;
            proveedor.Telefono = dto.Telefono;
            proveedor.Direccion = dto.Direccion;
            proveedor.IdLocalidad = dto.IdLocalidad;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // Para dar de baja, solo tiene sentido buscar entre los activos.
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
            {
                return false;
            }

            proveedor.Activo = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ReactivarAsync(int id)
        {
            // Tu lógica original era correcta, la mantenemos.
            var proveedor = await _context.Proveedores
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.IdProveedor == id);

            if (proveedor == null)
            {
                return false;
            }

            proveedor.Activo = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

