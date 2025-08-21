using System.Collections.Generic;

namespace DominioModelo
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public int IdEmpleado { get; set; }

        public List<LineaVenta> LineasVenta { get; set; } = new List<LineaVenta>();

        public Venta() { }

        public Venta(int idVenta, int idEmpleado)
        {
            IdVenta = idVenta;
            IdEmpleado = idEmpleado;
        }
    }
}
