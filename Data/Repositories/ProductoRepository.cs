using Data;
using DominioModelo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ProductoRepository
    {
        private readonly BuyJugadorContext _context;

        public ProductoRepository(BuyJugadorContext context)
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

        public async Task<List<Producto>> GetByIdsAsync(List<int> ids)
        {
            return await _context.Productos
                .Where(p => ids.Contains(p.IdProducto) && p.Activo)
                .Include(p => p.PreciosVenta)
                .ToListAsync();
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

        public async Task AddAsync(Producto producto)
        {
            await _context.Productos.AddAsync(producto);
        }

        public void Update(Producto producto)
        {
            var existing = _context.Productos.Local.FirstOrDefault(p => p.IdProducto == producto.IdProducto);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(producto);
            }
            else
            {
                _context.Productos.Update(producto);
            }
        }

        public async Task<List<Producto>> GetProductosBajoStockAsync(int limiteStock)
        {
            return await _context.Productos
                .Where(p => p.Stock <= limiteStock && p.Stock > 0 && p.Activo)
                .OrderBy(p => p.Stock)
                .ToListAsync();
        }
    }
}
