using Data;
using DominioModelo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ProveedorRepository
    {
        private readonly BuyJugadorContext _context;

        public ProveedorRepository(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<Proveedor>> GetAllAsync()
        {
            return await _context.Proveedores
                .Where(p => p.Activo)
                .ToListAsync();
        }

        public async Task<List<Proveedor>> GetInactivosAsync()
        {
            return await _context.Proveedores
                .IgnoreQueryFilters()
                .Where(p => !p.Activo)
                .ToListAsync();
        }

        public async Task<Proveedor?> GetByIdAsync(int id)
        {
            return await _context.Proveedores.FirstOrDefaultAsync(p => p.IdProveedor == id);
        }

        public async Task<Proveedor?> GetByIdIgnorandoFiltrosAsync(int id)
        {
            return await _context.Proveedores
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.IdProveedor == id);
        }

        public async Task<List<Proveedor>> GetByProductoIdAsync(int idProducto)
        {
            return await _context.Proveedores
                .Where(p => p.Activo && p.ProductoProveedores.Any(pp => pp.IdProducto == idProducto))
                .ToListAsync();
        }

        public async Task AddAsync(Proveedor entity)
        {
            await _context.Proveedores.AddAsync(entity);
        }

        public void Update(Proveedor entity)
        {
            _context.Proveedores.Update(entity);
        }
    }
}
