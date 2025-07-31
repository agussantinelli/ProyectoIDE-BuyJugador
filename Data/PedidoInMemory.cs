using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DominioModelo;

namespace Data
{
    public class PedidoInMemory
    {
        public static List<Pedido> Pedidos;

        static PedidoInMemory()
        {
            Pedidos = new List<Pedido>
            {
            };
        }
    }
}
