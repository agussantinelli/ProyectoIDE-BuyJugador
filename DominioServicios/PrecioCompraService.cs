using Data;
using DominioModelo;
using DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DominioServicios
{
    public class PrecioCompraService
    {
        private readonly UnitOfWork _unitOfWork;

        public PrecioCompraService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<decimal?> GetMontoAsync(int idProducto, int idProveedor)
        {
            return await _unitOfWork.PrecioCompraRepository.GetMontoAsync(idProducto, idProveedor);
        }

        public async Task<List<PrecioCompraDTO>> GetAllAsync()
        {
            var precios = await _unitOfWork.PrecioCompraRepository.GetAllDetalladoAsync();
            return precios.Select(pc => new PrecioCompraDTO
            {
                IdProducto = pc.IdProducto,
                IdProveedor = pc.IdProveedor,
                Monto = pc.Monto,
                NombreProducto = pc.Producto.Nombre,
                RazonSocialProveedor = pc.Proveedor.RazonSocial
            }).ToList();
        }

        public async Task<PrecioCompraDTO?> GetByIdAsync(int idProducto, int idProveedor)
        {
            var pc = await _unitOfWork.PrecioCompraRepository.GetByIdAsync(idProducto, idProveedor);
            if (pc == null) return null;

            return new PrecioCompraDTO
            {
                IdProducto = pc.IdProducto,
                IdProveedor = pc.IdProveedor,
                Monto = pc.Monto,
                NombreProducto = pc.Producto.Nombre,
                RazonSocialProveedor = pc.Proveedor.RazonSocial
            };
        }

        public async Task<PrecioCompraDTO> CreateAsync(PrecioCompraDTO dto)
        {
            var entity = new PrecioCompra
            {
                IdProducto = dto.IdProducto,
                IdProveedor = dto.IdProveedor,
                Monto = dto.Monto
            };
            await _unitOfWork.PrecioCompraRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return dto;
        }

        public async Task UpdateAsync(int idProducto, int idProveedor, PrecioCompraDTO dto)
        {
            var entity = await _unitOfWork.PrecioCompraRepository.FindTrackedByIdAsync(idProducto, idProveedor);
            if (entity != null)
            {
                entity.Monto = dto.Monto;
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int idProducto, int idProveedor)
        {
            var entity = await _unitOfWork.PrecioCompraRepository.FindTrackedByIdAsync(idProducto, idProveedor);
            if (entity != null)
            {
                _unitOfWork.PrecioCompraRepository.Remove(entity);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
