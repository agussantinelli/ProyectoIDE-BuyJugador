using Data;
using DominioModelo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DominioServicios
{
    public class ProductoService
    {
        private readonly UnitOfWork _unitOfWork;

        public ProductoService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Producto>> GetAllAsync()
        {
            return await _unitOfWork.ProductoRepository.GetAllAsync();
        }

        public async Task<List<Producto>> GetAllInactivosAsync()
        {
            return await _unitOfWork.ProductoRepository.GetAllInactivosAsync();
        }

        public async Task<Producto?> GetByIdAsync(int id)
        {
            return await _unitOfWork.ProductoRepository.GetByIdAsync(id);
        }

        public async Task<List<Producto>> GetByProveedorIdAsync(int idProveedor)
        {
            return await _unitOfWork.ProductoRepository.GetByProveedorIdAsync(idProveedor);
        }

        public async Task<List<Producto>> GetByTipoProductoIdAsync(int idTipoProducto)
        {
            return await _unitOfWork.ProductoRepository.GetByTipoProductoIdAsync(idTipoProducto);
        }

        public async Task<Producto> CreateAsync(Producto producto)
        {
            await _unitOfWork.ProductoRepository.AddAsync(producto);
            await _unitOfWork.SaveChangesAsync();
            return producto;
        }

        public async Task UpdateAsync(Producto producto)
        {
            var existing = await _unitOfWork.ProductoRepository.GetByIdAsync(producto.IdProducto);
            if (existing != null)
            {
                existing.Nombre = producto.Nombre;
                existing.Descripcion = producto.Descripcion;
                existing.Stock = producto.Stock;
                existing.Activo = producto.Activo;
                existing.IdTipoProducto = producto.IdTipoProducto;

                _unitOfWork.ProductoRepository.Update(existing);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var producto = await _unitOfWork.ProductoRepository.GetByIdAsync(id);
            if (producto != null)
            {
                producto.Activo = false;
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task ReactivarAsync(int id)
        {
            var producto = await _unitOfWork.ProductoRepository.GetByIdAsync(id);
            if (producto != null)
            {
                producto.Activo = true;
                await _unitOfWork.SaveChangesAsync();
            }
        }
        public async Task<List<Producto>> GetProductosBajoStockAsync(int limiteStock)
        {
            return await _unitOfWork.ProductoRepository.GetProductosBajoStockAsync(limiteStock);
        }
    }
}
