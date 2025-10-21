using Data;
using DominioModelo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ProductoProveedorRepository
    {
        private readonly BuyJugadorContext _context;

        public ProductoProveedorRepository(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<ProductoProveedor>> GetProductosByProveedorIdAsync(int idProveedor)
        {
            return await _context.ProductoProveedores
                .Where(pp => pp.IdProveedor == idProveedor)
                .Include(pp => pp.Producto)
                    .ThenInclude(p => p.PreciosCompra)
                .ToListAsync();
        }

        public async Task AddAsync(ProductoProveedor entity)
        {
            await _context.ProductoProveedores.AddAsync(entity);
        }

        public async Task<ProductoProveedor?> GetByIdAsync(int idProducto, int idProveedor)
        {
            return await _context.ProductoProveedores
                .FirstOrDefaultAsync(x => x.IdProducto == idProducto && x.IdProveedor == idProveedor);
        }

        public void Remove(ProductoProveedor entity)
        {
            _context.ProductoProveedores.Remove(entity);
        }
    }
}
