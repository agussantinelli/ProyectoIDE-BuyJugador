using System;

namespace DominioModelo
{
    public class LineaVenta
    {
        public int IdVenta { get; private set; }
        public decimal NroLineaVenta { get; private set; }
        public int IdProducto { get; private set; }
        public int CantidadVenta { get; private set; }

        public LineaVenta(int idVenta, decimal nroLineaVenta, int idProducto, int cantidadVenta)
        {
            SetIdVenta(idVenta);
            SetNroLineaVenta(nroLineaVenta);
            SetIdProducto(idProducto);
            SetCantidadVenta(cantidadVenta);
        }

        public void SetIdVenta(int idVenta)
        {
            if (idVenta <= 0)
                throw new ArgumentException("El ID de venta debe ser positivo.", nameof(idVenta));
            IdVenta = idVenta;
        }

        public void SetNroLineaVenta(decimal nroLineaVenta)
        {
            if (nroLineaVenta <= 0)
                throw new ArgumentException("El número de línea de venta debe ser positivo.", nameof(nroLineaVenta));
            NroLineaVenta = nroLineaVenta;
        }

        public void SetIdProducto(int idProducto)
        {
            if (idProducto <= 0)
                throw new ArgumentException("El ID de producto debe ser positivo.", nameof(idProducto));
            IdProducto = idProducto;
        }

        public void SetCantidadVenta(int cantidadVenta)
        {
            if (cantidadVenta <= 0)
                throw new ArgumentException("La cantidad de venta debe ser positiva.", nameof(cantidadVenta));
            CantidadVenta = cantidadVenta;
        }
    }
}
