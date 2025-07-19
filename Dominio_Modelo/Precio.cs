using System;

namespace Dominio_Modelo
{
    public class Precio
    {
        public int IdPrecio { get; private set; }
        public int IdProducto { get; private set; }
        public decimal Monto { get; private set; }
        public DateTime Fecha { get; private set; }

        public Precio(int idPrecio, int idProducto, decimal monto, DateTime fecha)
        {
            SetIdPrecio(idPrecio);
            SetIdProducto(idProducto);
            SetMonto(monto);
            SetFecha(fecha);
        }

        public void SetIdPrecio(int idPrecio)
        {
            if (idPrecio <= 0)
                throw new ArgumentException("El ID de precio debe ser positivo.", nameof(idPrecio));
            IdPrecio = idPrecio;
        }

        public void SetIdProducto(int idProducto)
        {
            if (idProducto <= 0)
                throw new ArgumentException("El ID de producto debe ser positivo.", nameof(idProducto));
            IdProducto = idProducto;
        }

        public void SetMonto(decimal monto)
        {
            if (monto <= 0)
                throw new ArgumentException("El monto debe ser positivo.", nameof(monto));
            Monto = monto;
        }

        public void SetFecha(DateTime fecha)
        {
            if (fecha == default)
                throw new ArgumentException("La fecha no puede ser nula.", nameof(fecha));
            Fecha = fecha;
        }
    }
}
