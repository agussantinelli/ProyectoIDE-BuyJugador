using System;
using System.Collections.Generic;
using Dominio_Modelo;

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
