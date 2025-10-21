using Data;
using DominioModelo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class PedidoRepository
    {
        private readonly BuyJugadorContext _context;

        public PedidoRepository(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<Pedido>> GetAllPedidosDetalladosAsync()
        {
            return await _context.Pedidos
                .AsNoTracking()
                .Include(p => p.IdProveedorNavigation)
                .Include(p => p.LineasPedido)
                .OrderByDescending(p => p.Fecha)
                .ToListAsync();
        }

        public async Task<Pedido?> GetPedidoDetalladoByIdAsync(int id)
        {
            return await _context.Pedidos
                .AsNoTracking()
                .Include(p => p.IdProveedorNavigation)
                .Include(p => p.LineasPedido)
                    .ThenInclude(lp => lp.IdProductoNavigation)
                .FirstOrDefaultAsync(p => p.IdPedido == id);
        }

        public async Task<Pedido?> GetByIdAsync(int id)
        {
            return await _context.Pedidos
               .Include(p => p.LineasPedido)
               .FirstOrDefaultAsync(p => p.IdPedido == id);
        }

        public async Task AddAsync(Pedido entity)
        {
            await _context.Pedidos.AddAsync(entity);
        }

        public void Remove(Pedido entity)
        {
            _context.Pedidos.Remove(entity);
        }

        public void RemoveLineas(IEnumerable<LineaPedido> lineas)
        {
            _context.LineaPedidos.RemoveRange(lineas);
        }

        public async Task AddLineaAsync(LineaPedido linea)
        {
            await _context.LineaPedidos.AddAsync(linea);
        }

        public async Task<int> GetCantidadPedidosPendientesAsync()
        {
            return await _context.Pedidos.CountAsync(p => p.Estado == "Pendiente");
        }
    }
}
