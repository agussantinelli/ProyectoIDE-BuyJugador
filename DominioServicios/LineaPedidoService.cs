using Data;
using DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DominioServicios
{
    public class LineaPedidoService
    {
        // #CAMBIO: Inyectar UnitOfWork en lugar de DbContext.
        private readonly UnitOfWork _unitOfWork;

        public LineaPedidoService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<LineaPedidoDTO>> GetLineasByPedidoIdAsync(int idPedido)
        {
            // #Lógica: Obtener el pedido para consultar su fecha.
            var pedido = await _unitOfWork.PedidoRepository.GetByIdAsync(idPedido);
            if (pedido == null) return new List<LineaPedidoDTO>();

            // #CAMBIO: Delegar la obtención de datos al repositorio.
            var lineas = await _unitOfWork.LineaPedidoRepository.GetLineasByPedidoIdAsync(idPedido);

            // #Lógica de Negocio: Calcular el precio unitario en base a la fecha del pedido.
            // #Esta lógica permanece en el servicio porque coordina datos de diferentes dominios.
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
