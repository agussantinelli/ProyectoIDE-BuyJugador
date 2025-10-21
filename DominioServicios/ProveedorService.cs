using Data;
using DominioModelo;
using DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DominioServicios
{
    public class ProveedorService
    {
        private readonly UnitOfWork _unitOfWork;

        public ProveedorService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ProveedorDTO>> GetAllAsync()
        {
            var proveedores = await _unitOfWork.ProveedorRepository.GetAllAsync();
            return proveedores.Select(p => ProveedorDTO.FromDominio(p)!).ToList();
        }

        public async Task<List<ProveedorDTO>> GetInactivosAsync()
        {
            var proveedores = await _unitOfWork.ProveedorRepository.GetInactivosAsync();
            return proveedores.Select(p => ProveedorDTO.FromDominio(p)!).ToList();
        }

        public async Task<ProveedorDTO?> GetByIdAsync(int id)
        {
            var p = await _unitOfWork.ProveedorRepository.GetByIdAsync(id);
            return p != null ? ProveedorDTO.FromDominio(p) : null;
        }

        public async Task<List<ProveedorDTO>> GetByProductoIdAsync(int idProducto)
        {
            var proveedores = await _unitOfWork.ProveedorRepository.GetByProductoIdAsync(idProducto);
            return proveedores.Select(p => ProveedorDTO.FromDominio(p)!).ToList();
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

            await _unitOfWork.ProveedorRepository.AddAsync(proveedor);
            await _unitOfWork.SaveChangesAsync();

            dto.IdProveedor = proveedor.IdProveedor;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, ProveedorDTO dto)
        {
            var proveedor = await _unitOfWork.ProveedorRepository.GetByIdIgnorandoFiltrosAsync(id);
            if (proveedor == null) return false;

            proveedor.RazonSocial = dto.RazonSocial;
            proveedor.Cuit = dto.Cuit;
            proveedor.Email = dto.Email;
            proveedor.Telefono = dto.Telefono;
            proveedor.Direccion = dto.Direccion;
            proveedor.IdLocalidad = dto.IdLocalidad;

            _unitOfWork.ProveedorRepository.Update(proveedor);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var proveedor = await _unitOfWork.ProveedorRepository.GetByIdAsync(id);
            if (proveedor == null) return false;

            proveedor.Activo = false;
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ReactivarAsync(int id)
        {
            var proveedor = await _unitOfWork.ProveedorRepository.GetByIdIgnorandoFiltrosAsync(id);
            if (proveedor == null) return false;

            proveedor.Activo = true;
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
