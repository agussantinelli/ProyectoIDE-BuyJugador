using System;

namespace Dominio_Modelo
{
    public class Cancelacion
    {
        public int Id { get; private set; }
        public string Motivo { get; private set; }
        public DateTime Fecha { get; private set; }
        public int IdVenta { get; private set; }

        public Cancelacion(int id, string motivo, DateTime fecha, int idVenta)
        {
            SetId(id);
            SetMotivo(motivo);
            SetFecha(fecha);
            SetIdVenta(idVenta);
        }

        public void SetId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID de cancelación debe ser positivo.", nameof(id));
            Id = id;
        }

        public void SetMotivo(string motivo)
        {
            if (string.IsNullOrWhiteSpace(motivo))
                throw new ArgumentException("El motivo no puede ser nulo o vacío.", nameof(motivo));
            Motivo = motivo;
        }

        public void SetFecha(DateTime fecha)
        {
            if (fecha == default)
                throw new ArgumentException("La fecha no puede ser nula.", nameof(fecha));
            Fecha = fecha;
        }

        public void SetIdVenta(int idVenta)
        {
            if (idVenta <= 0)
                throw new ArgumentException("El ID de venta debe ser positivo.", nameof(idVenta));
            IdVenta = idVenta;
        }
    }
}