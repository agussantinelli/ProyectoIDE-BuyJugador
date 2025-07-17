using System;

namespace Dominio_Modelo
{
    public class Precio
    {
        public int Id { get; private set; }
        public decimal Monto { get; private set; }
        public DateTime Fecha { get; private set; }
        public int IdProducto { get; private set; }

        public Precio(int id, decimal monto, DateTime fecha, int idProducto)
        {
            SetId(id);
            SetMonto(monto);
            SetFecha(fecha);
            SetIdProducto(idProducto);
        }

        public void SetId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del precio debe ser positivo.", nameof(id));
            Id = id;
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

        public void SetIdProducto(int idProducto)
        {
            if (idProducto <= 0)
                throw new ArgumentException("El ID del producto debe ser positivo.", nameof(idProducto));
            IdProducto = idProducto;
        }
    }
}