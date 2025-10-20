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
                .Include(p => p.PreciosVenta)
                .ToListAsync();
        }

        public async Task<List<Producto>> GetAllInactivosAsync()
        {
            return await _context.Productos
                .IgnoreQueryFilters()
                .Where(p => !p.Activo)
                .Include(p => p.IdTipoProductoNavigation)
                .Include(p => p.PreciosVenta)
                .ToListAsync();
        }

        public async Task<Producto?> GetByIdAsync(int id)
        {
            return await _context.Productos
                .IgnoreQueryFilters()
                .Include(p => p.IdTipoProductoNavigation)
                .Include(p => p.PreciosVenta)
                .FirstOrDefaultAsync(p => p.IdProducto == id);
        }

        public async Task<List<Producto>> GetByProveedorIdAsync(int idProveedor)
        {
            return await _context.Productos
                .Where(p => p.ProductoProveedores.Any(pp => pp.IdProveedor == idProveedor))
                .Include(p => p.IdTipoProductoNavigation)
                .Include(p => p.PreciosCompra)
                .Include(p => p.PreciosVenta)
                .ToListAsync();
        }

        public async Task<List<Producto>> GetByTipoProductoIdAsync(int idTipoProducto)
        {
            return await _context.Productos
                .Where(p => p.IdTipoProducto == idTipoProducto)
                .Include(p => p.IdTipoProductoNavigation)
                .Include(p => p.PreciosVenta)
                .ToListAsync();
        }

        public async Task<Producto> CreateAsync(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task UpdateAsync(Producto producto)
        {
            var existing = await _context.Productos
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.IdProducto == producto.IdProducto);

            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(producto);
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
        public async Task<List<Producto>> GetProductosBajoStockAsync(int limiteStock)
        {
            var productos = await _context.Productos
                .Where(p => p.Stock <= limiteStock && p.Stock > 0 && p.Activo)
                .OrderBy(p => p.Stock)
                .ToListAsync();

            return productos;
        }
    }
}
