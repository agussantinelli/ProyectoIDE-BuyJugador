using System;

namespace Dominio_Modelo
{
    public class Precio
    {
        public int IdProducto { get; private set; }
        public decimal Monto { get; private set; }
        public DateTime FechaDesde { get; private set; }

        public Precio(int idProducto, decimal monto, DateTime fechaDesde)
        {
            SetIdProducto(idProducto);
            SetMonto(monto);
            SetFechaDesde(fechaDesde);
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

        public void SetFechaDesde(DateTime fechaDesde)
        {
            if (fechaDesde == default)
                throw new ArgumentException("La fecha no puede ser nula.", nameof(fechaDesde));
            FechaDesde = fechaDesde;
        }
    }
}
