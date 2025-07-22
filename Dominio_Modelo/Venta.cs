using System;

namespace Dominio_Modelo
{
    public class Venta
    {
        public int IdVenta { get; private set; }
        public DateTime FechaVenta { get; private set; }
        public string EstadoVenta { get; private set; }
        public decimal MontoTotalVenta { get; private set; }
        public int DniVendedor { get; private set; }

        public Venta(int idVenta, DateTime fechaVenta, string estadoVenta, decimal montoTotalVenta, int dniVendedor)
        {
            SetIdVenta(idVenta);
            SetFechaVenta(fechaVenta);
            SetEstadoVenta(estadoVenta);
            SetMontoTotalVenta(montoTotalVenta);
            SetDniVendedor(dniVendedor);
        }

        public void SetIdVenta(int idVenta)
        {
            if (idVenta <= 0)
                throw new ArgumentException("El ID de venta debe ser positivo.", nameof(idVenta));
            IdVenta = idVenta;
        }

        public void SetFechaVenta(DateTime fechaVenta)
        {
            if (fechaVenta == default)
                throw new ArgumentException("La fecha de venta no puede ser vacía.", nameof(fechaVenta));
            FechaVenta = fechaVenta;
        }

        public void SetEstadoVenta(string estadoVenta)
        {
            if (string.IsNullOrWhiteSpace(estadoVenta))
                throw new ArgumentException("El estado de venta no puede ser nulo o vacío.", nameof(estadoVenta));
            EstadoVenta = estadoVenta;
        }

        public void SetMontoTotalVenta(decimal montoTotalVenta)
        {
            if (montoTotalVenta < 0)
                throw new ArgumentException("El monto total de la venta no puede ser negativo.", nameof(montoTotalVenta));
            MontoTotalVenta = montoTotalVenta;
        }

        public void SetDniVendedor(int dniVendedor)
        {
            if (dniVendedor <= 0)
                throw new ArgumentException("El DNI del vendedor debe ser positivo.", nameof(dniVendedor));
            DniVendedor = dniVendedor;
        }
    }
}
