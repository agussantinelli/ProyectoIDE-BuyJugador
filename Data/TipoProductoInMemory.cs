using System;
using System.Collections.Generic;
using DominioModelo;

namespace Data
{
    public class TipoProductoInMemory
    {
        public static List<TipoProducto> TiposProducto;

        static TipoProductoInMemory()
        {
            TiposProducto = new List<TipoProducto>
            {
                new TipoProducto(1, "Componentes"),
                new TipoProducto(2, "Monitores"),
                new TipoProducto(3, "Parlantes")
            };
        }
        public static int NextTipoProductoId => TiposProducto.Any() ? TiposProducto.Max(t => t.IdTipoProducto) + 1 : 1;
    }
}



