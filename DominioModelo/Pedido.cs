using System.Collections.Generic;

namespace DominioModelo
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public int IdProveedor { get; set; }

        public List<LineaPedido> LineasPedido { get; set; } = new List<LineaPedido>();

        public Pedido() { }

        public Pedido(int idPedido, int idProveedor)
        {
            IdPedido = idPedido;
            IdProveedor = idProveedor;
        }
    }
}
