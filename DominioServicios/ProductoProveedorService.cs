using Data;
using DominioModelo;
using DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DominioServicios
{
    public class ProductoProveedorService
    {
        private readonly UnitOfWork _unitOfWork;

        public ProductoProveedorService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ProductoAsignadoDTO>> GetProductosByProveedorIdAsync(int idProveedor)
        {
            var relaciones = await _unitOfWork.ProductoProveedorRepository.GetProductosByProveedorIdAsync(idProveedor);

            return relaciones.Select(pp => new ProductoAsignadoDTO
            {
                IdProducto = pp.Producto.IdProducto,
                Nombre = pp.Producto.Nombre,
                Descripcion = pp.Producto.Descripcion,
                PrecioCompra = pp.Producto.PreciosCompra
                                .Where(pc => pc.IdProveedor == idProveedor)
                                .Select(pc => pc.Monto)
                                .FirstOrDefault()
            }).ToList();
        }

        public async Task CreateAsync(ProductoProveedorDTO dto)
        {
            var newRelation = new ProductoProveedor
            {
                IdProducto = dto.IdProducto,
                IdProveedor = dto.IdProveedor
            };
            await _unitOfWork.ProductoProveedorRepository.AddAsync(newRelation);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int idProducto, int idProveedor)
        {
            var precioCompra = await _unitOfWork.PrecioCompraRepository.FindTrackedByIdAsync(idProducto, idProveedor);
            if (precioCompra != null)
            {
                _unitOfWork.PrecioCompraRepository.Remove(precioCompra);
            }

            var pp = await _unitOfWork.ProductoProveedorRepository.GetByIdAsync(idProducto, idProveedor);
            if (pp == null)
            {
                return false;
            }
            _unitOfWork.ProductoProveedorRepository.Remove(pp);

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
