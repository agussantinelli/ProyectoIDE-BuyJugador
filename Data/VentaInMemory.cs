using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio_Modelo;

namespace Data
{
    public class VentaInMemory
    {
        public static List<Venta> Ventas;

        static VentaInMemory()
        {
            Ventas = new List<Venta>
            {
            };
        }
    }
}
