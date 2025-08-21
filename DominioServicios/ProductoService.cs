using Data;
using DominioModelo;
using Microsoft.EntityFrameworkCore;

namespace DominioServicios
{
    public class ProductoService
    {
        private readonly BuyJugadorContext _context;

        public ProductoService(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<Producto>> GetAllAsync()
        {
            return await _context.Productos.ToListAsync();
        }

        public async Task<Producto?> GetByIdAsync(int id)
        {
            return await _context.Productos.FindAsync(id);
        }

        public async Task<Producto> CreateAsync(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task UpdateAsync(int id, Producto producto)
        {
            var existingProducto = await _context.Productos.FindAsync(id);
            if (existingProducto != null)
            {
                existingProducto.Nombre = producto.Nombre;
                existingProducto.Descripcion = producto.Descripcion;
                existingProducto.Stock = producto.Stock;
                existingProducto.IdTipoProducto = producto.IdTipoProducto;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var productoToDelete = await _context.Productos.FindAsync(id);
            if (productoToDelete != null)
            {
                _context.Productos.Remove(productoToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
