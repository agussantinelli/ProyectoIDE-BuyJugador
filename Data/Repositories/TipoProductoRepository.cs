using Data;
using DominioModelo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class TipoProductoRepository
    {
        private readonly BuyJugadorContext _context;

        public TipoProductoRepository(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<TipoProducto>> GetAllAsync()
        {
            return await _context.TiposProductos.ToListAsync();
        }

        public async Task<TipoProducto?> GetByIdAsync(int id)
        {
            return await _context.TiposProductos.FindAsync(id);
        }

        public async Task<TipoProducto?> GetByIdConProductosAsync(int id)
        {
            return await _context.TiposProductos
               .Include(tp => tp.Productos)
               .FirstOrDefaultAsync(tp => tp.IdTipoProducto == id);
        }

        public async Task AddAsync(TipoProducto entity)
        {
            await _context.TiposProductos.AddAsync(entity);
        }

        public void Update(TipoProducto entity)
        {
            _context.TiposProductos.Update(entity);
        }

        public void Remove(TipoProducto entity)
        {
            _context.TiposProductos.Remove(entity);
        }
    }
}
