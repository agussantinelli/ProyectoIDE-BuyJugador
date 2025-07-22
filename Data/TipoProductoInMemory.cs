using System;
using System.Collections.Generic;
using Dominio_Modelo;

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
    }
}

