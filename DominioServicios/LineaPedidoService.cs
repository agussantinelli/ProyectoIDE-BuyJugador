using Data;
using DominioModelo;
using Microsoft.EntityFrameworkCore;

namespace DominioServicios
{
    public class LineaPedidoService
    {
        private readonly BuyJugadorContext _context;

        public LineaPedidoService(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<LineaPedido>> GetAllAsync()
        {
            return await _context.LineasPedido.ToListAsync();
        }

        public async Task<LineaPedido?> GetByIdAsync(int id)
        {
            return await _context.LineasPedido.FindAsync(id);
        }

        public async Task<LineaPedido> CreateAsync(LineaPedido lineaPedido)
        {
            _context.LineasPedido.Add(lineaPedido);
            await _context.SaveChangesAsync();
            return lineaPedido;
        }
    }
}
