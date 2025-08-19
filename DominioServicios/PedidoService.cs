using Data;
using Dominio_Modelo; // <-- ESTA LÍNEA ES LA SOLUCIÓN
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                                 .FirstOrDefaultAsync(p => p.IdPedido == id);
        }

        public async Task<Pedido> CreateAsync(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }
    }
}
