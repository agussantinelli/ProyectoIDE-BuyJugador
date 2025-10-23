using Data;
using DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DominioServicios
{
    public class LineaPedidoService
    {
        private readonly UnitOfWork _unitOfWork;

        public LineaPedidoService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<LineaPedidoDTO>> GetLineasByPedidoIdAsync(int idPedido)
        {
            var pedido = await _unitOfWork.PedidoRepository.GetByIdAsync(idPedido);
            if (pedido == null) return new List<LineaPedidoDTO>();
            var lineas = await _unitOfWork.LineaPedidoRepository.GetLineasByPedidoIdAsync(idPedido);
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
