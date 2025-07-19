using System;

namespace Dominio_Modelo
{
    public class Venta
    {
        public int NroVenta { get; private set; }
        public DateTime Fecha { get; private set; }
        public string Estado { get; private set; }
        public decimal MontoTotal { get; private set; }
        public int LegajoCliente { get; private set; }

        public Venta(int nroVenta, DateTime fecha, string estado, decimal montoTotal, int legajoCliente)
        {
            SetNroVenta(nroVenta);
            SetFecha(fecha);
            SetEstado(estado);
            SetMontoTotal(montoTotal);
            SetLegajoCliente(legajoCliente);
        }

        public void SetNroVenta(int nroVenta)
        {
            if (nroVenta <= 0)
                throw new ArgumentException("El número de venta debe ser positivo.", nameof(nroVenta));
            NroVenta = nroVenta;
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

        public void SetLegajoCliente(int legajoCliente)
        {
            if (legajoCliente <= 0)
                throw new ArgumentException("El legajo del cliente debe ser positivo.", nameof(legajoCliente));
            LegajoCliente = legajoCliente;
        }
    }
}
