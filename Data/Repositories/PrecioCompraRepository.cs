using Data;
using DominioModelo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class PrecioCompraRepository
    {
        private readonly BuyJugadorContext _context;

        public PrecioCompraRepository(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<decimal?> GetMontoAsync(int idProducto, int idProveedor)
        {
            var precioCompra = await _context.PreciosCompra
                .AsNoTracking()
                .FirstOrDefaultAsync(pc => pc.IdProducto == idProducto && pc.IdProveedor == idProveedor);

            return precioCompra?.Monto;
        }

        public async Task<List<PrecioCompra>> GetAllDetalladoAsync()
        {
            return await _context.PreciosCompra
                .AsNoTracking()
                .Include(pc => pc.Producto)
                .Include(pc => pc.Proveedor)
                .ToListAsync();
        }

        public async Task<PrecioCompra?> GetByIdAsync(int idProducto, int idProveedor)
        {
            return await _context.PreciosCompra
                .AsNoTracking()
                .Include(p => p.Producto)
                .Include(p => p.Proveedor)
                .FirstOrDefaultAsync(p => p.IdProducto == idProducto && p.IdProveedor == idProveedor);
        }

        public async Task AddAsync(PrecioCompra entity)
        {
            await _context.PreciosCompra.AddAsync(entity);
        }

        public async Task<PrecioCompra?> FindTrackedByIdAsync(int idProducto, int idProveedor)
        {
            return await _context.PreciosCompra
               .FirstOrDefaultAsync(p => p.IdProducto == idProducto && p.IdProveedor == idProveedor);
        }

        public void Remove(PrecioCompra entity)
        {
            _context.PreciosCompra.Remove(entity);
        }
    }
}

