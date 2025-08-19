using DominioModelo;
using System.Collections.Generic;

namespace Dominio_Modelo
{
    public class Venta
    {
        // Se agrega { set; } a las propiedades
        public int IdVenta { get; set; }
        public int IdEmpleado { get; set; }

        public List<LineaVenta> LineasVenta { get; set; } = new List<LineaVenta>();

        // Agregamos un constructor sin parámetros que EF Core necesita
        public Venta() { }

        public Venta(int idVenta, int idEmpleado)
        {
            IdVenta = idVenta;
            IdEmpleado = idEmpleado;
        }
    }
}
