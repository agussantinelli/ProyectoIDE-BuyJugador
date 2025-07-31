using System;
using System.Collections.Generic;
using DominioModelo;

namespace Data
{
    public class LineaPedidoInMemory
    {
        public static List<LineaPedido> LineasPedido;

        static LineaPedidoInMemory()
        {
            LineasPedido = new List<LineaPedido>
            {
            };
        }
    }
}
