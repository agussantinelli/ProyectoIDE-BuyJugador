using System;
using System.Collections.Generic;

namespace DTOs
{
    public class PedidoDTO
    {
        public int IdPedido { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public int IdProveedor { get; set; }

        // Propiedades para más detalle en la UI
        public string? ProveedorRazonSocial { get; set; }
        public decimal Total { get; set; }
        public List<LineaPedidoDTO> LineasPedido { get; set; } = new List<LineaPedidoDTO>();
    }
}

