using Data;
using Microsoft.EntityFrameworkCore;
using DominioModelo;

namespace DominioServicios
{
    public class PedidoService
    {
        private readonly BuyJugadorContext _context;

        public PedidoService(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<Pedido>> GetAllAsync()
        {
            return await _context.Pedidos
                                 .Include(p => p.LineasPedido)
                                 .ToListAsync();
        }

        public async Task<Pedido?> GetByIdAsync(int id)
        {
            return await _context.Pedidos
                                 .Include(p => p.LineasPedido)
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Pedido> CreateAsync(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }
    }
}
