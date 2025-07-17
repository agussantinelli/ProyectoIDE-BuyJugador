using System;

namespace Dominio_Modelo
{
    public class Venta
    {
        public int Id { get; private set; }
        public DateTime Fecha { get; private set; }
        public string Estado { get; private set; }
        public decimal MontoTotal { get; private set; }
        public int IdCliente { get; private set; }

        public Venta(int id, DateTime fecha, string estado, decimal montoTotal, int idCliente)
        {
            SetId(id);
            SetFecha(fecha);
            SetEstado(estado);
            SetMontoTotal(montoTotal);
            SetIdCliente(idCliente);
        }

        public void SetId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID de la venta debe ser positivo.", nameof(id));
            Id = id;
        }

        public void SetFecha(DateTime fecha)
        {
            if (fecha == default)
                throw new ArgumentException("La fecha no puede ser nula.", nameof(fecha));
            Fecha = fecha;
        }

        public void SetEstado(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                throw new ArgumentException("El estado no puede ser nulo o vacío.", nameof(estado));
            Estado = estado;
        }

        public void SetMontoTotal(decimal montoTotal)
        {
            if (montoTotal < 0)
                throw new ArgumentException("El monto total no puede ser negativo.", nameof(montoTotal));
            MontoTotal = montoTotal;
        }

        public void SetIdCliente(int idCliente)
        {
            if (idCliente <= 0)
                throw new ArgumentException("El ID del cliente debe ser positivo.", nameof(idCliente));
            IdCliente = idCliente;
        }
    }
}