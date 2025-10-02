using Data;
using DominioModelo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return await _context.Productos
                .Include(p => p.IdTipoProductoNavigation)
                .Include(p => p.Precios)
                .ToListAsync();
        }

        public async Task<List<Producto>> GetAllInactivosAsync()
        {
            return await _context.Productos
                .IgnoreQueryFilters()
                .Where(p => !p.Activo)
                .Include(p => p.IdTipoProductoNavigation)
                .Include(p => p.Precios)
                .ToListAsync();
        }

        public async Task<Producto?> GetByIdAsync(int id)
        {
            return await _context.Productos
                .IgnoreQueryFilters()
                .Include(p => p.IdTipoProductoNavigation)
                .Include(p => p.Precios)
                .FirstOrDefaultAsync(p => p.IdProducto == id);
        }

        public async Task<Producto> CreateAsync(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task UpdateAsync(Producto producto)
        {
            var existingProducto = await _context.Productos
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.IdProducto == producto.IdProducto);

            if (existingProducto != null)
            {
                _context.Entry(existingProducto).CurrentValues.SetValues(producto);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var producto = await _context.Productos
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.IdProducto == id);

            if (producto != null)
            {
                producto.Activo = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ReactivarAsync(int id)
        {
            var producto = await _context.Productos
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.IdProducto == id);

            if (producto != null)
            {
                producto.Activo = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
