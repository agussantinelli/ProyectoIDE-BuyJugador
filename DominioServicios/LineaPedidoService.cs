using Data;
using DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DominioServicios
{
    public class LineaPedidoService
    {
        private readonly BuyJugadorContext _context;

        public LineaPedidoService(BuyJugadorContext context)
        {
            _context = context;
        }

        public async Task<List<LineaPedidoDTO>> GetLineasByPedidoIdAsync(int idPedido)
        {
            var pedido = await _context.Pedidos.FindAsync(idPedido);
            if (pedido == null) return new List<LineaPedidoDTO>();

            var lineas = await _context.LineaPedidos
                .Where(l => l.IdPedido == idPedido)
                .Include(l => l.IdProductoNavigation)
                    .ThenInclude(p => p.PreciosVenta)
                .AsNoTracking()
                .ToListAsync();

            return lineas.Select(l => {
                var lineaDto = LineaPedidoDTO.FromDominio(l);
                if (l.IdProductoNavigation != null)
                {
                    var precio = l.IdProductoNavigation.PreciosVenta?
                                  .OrderByDescending(p => p.FechaDesde)
                                  .FirstOrDefault(p => p.FechaDesde <= pedido.Fecha)?.Monto ?? 0;
                    lineaDto.PrecioUnitario = precio;
                }
                return lineaDto;
            }).ToList();
        }
    }
}
