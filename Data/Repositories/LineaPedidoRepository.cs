using Data;
using DominioModelo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class LineaPedidoRepository
    {
        private readonly BuyJugadorContext _context;

        public LineaPedidoRepository(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<LineaPedido>> GetLineasByPedidoIdAsync(int idPedido)
        {
            return await _context.LineaPedidos
                .Where(l => l.IdPedido == idPedido)
                .Include(l => l.IdProductoNavigation)
                    .ThenInclude(p => p.PreciosVenta)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
