using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DominioModelo;

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
