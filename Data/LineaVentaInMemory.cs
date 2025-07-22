using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio_Modelo;

namespace Data
{
    public class LineaVentaInMemory
    {
        public static List<LineaVenta> LineasVenta;

        static LineaVentaInMemory()
        {
            LineasVenta = new List<LineaVenta>
            {
            };
        }
    }
}
