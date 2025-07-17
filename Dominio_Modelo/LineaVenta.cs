using System;

namespace Dominio_Modelo
{
    public class LineaVenta
    {
        public int Id { get; private set; }
        public int Numero { get; private set; }
        public int Cantidad { get; private set; }
        public int IdVenta { get; private set; }
        public int IdProducto { get; private set; }

        public LineaVenta(int id, int numero, int cantidad, int idVenta, int idProducto)
        {
            SetId(id);
            SetNumero(numero);
            SetCantidad(cantidad);
            SetIdVenta(idVenta);
            SetIdProducto(idProducto);
        }

        public void SetId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID de la línea de venta debe ser positivo.", nameof(id));
            Id = id;
        }

        public void SetNumero(int numero)
        {
            if (numero <= 0)
                throw new ArgumentException("El número de línea debe ser positivo.", nameof(numero));
            Numero = numero;
        }

        public void SetCantidad(int cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser positiva.", nameof(cantidad));
            Cantidad = cantidad;
        }

        public void SetIdVenta(int idVenta)
        {
            if (idVenta <= 0)
                throw new ArgumentException("El ID de venta debe ser positivo.", nameof(idVenta));
            IdVenta = idVenta;
        }

        public void SetIdProducto(int idProducto)
        {
            if (idProducto <= 0)
                throw new ArgumentException("El ID de producto debe ser positivo.", nameof(idProducto));
            IdProducto = idProducto;
        }
    }
}