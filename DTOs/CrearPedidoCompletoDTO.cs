using System.Collections.Generic;

namespace DTOs
{
    public class CrearPedidoCompletoDTO
    {
        public int IdProveedor { get; set; }

        public List<LineaPedidoDTO> LineasPedido { get; set; } = new List<LineaPedidoDTO>();

        public bool MarcarComoRecibido { get; set; }
    }
}
