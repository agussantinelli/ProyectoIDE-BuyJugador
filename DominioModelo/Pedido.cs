using DominioModelo;
using System.Collections.Generic;

namespace Dominio_Modelo
{
    public class Pedido
    {
        // Se agrega { set; } a las propiedades
        public int IdPedido { get; set; }
        public int IdProveedor { get; set; }

        public List<LineaPedido> LineasPedido { get; set; } = new List<LineaPedido>();

        // Agregamos un constructor sin parámetros que EF Core necesita
        public Pedido() { }

        public Pedido(int idPedido, int idProveedor)
        {
            IdPedido = idPedido;
            IdProveedor = idProveedor;
        }
    }
}
