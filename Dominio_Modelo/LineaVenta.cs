using System;

namespace Dominio_Modelo
{
    public class LineaVenta
    {
        public int NroVenta { get; private set; }
        public int IdProducto { get; private set; }
        public int Cantidad { get; private set; }
        public decimal PrecioUnitario { get; private set; }

        public LineaVenta(int nroVenta, int idProducto, int cantidad, decimal precioUnitario)
        {
            SetNroVenta(nroVenta);
            SetIdProducto(idProducto);
            SetCantidad(cantidad);
            SetPrecioUnitario(precioUnitario);
        }

        public void SetNroVenta(int nroVenta)
        {
            if (nroVenta <= 0)
                throw new ArgumentException("El número de venta debe ser positivo.", nameof(nroVenta));
            NroVenta = nroVenta;
        }

        public void SetIdProducto(int idProducto)
        {
            if (idProducto <= 0)
                throw new ArgumentException("El ID de producto debe ser positivo.", nameof(idProducto));
            IdProducto = idProducto;
        }

        public void SetCantidad(int cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser positiva.", nameof(cantidad));
            Cantidad = cantidad;
        }

        public void SetPrecioUnitario(decimal precioUnitario)
        {
            if (precioUnitario <= 0)
                throw new ArgumentException("El precio unitario debe ser positivo.", nameof(precioUnitario));
            PrecioUnitario = precioUnitario;
        }
    }
}
