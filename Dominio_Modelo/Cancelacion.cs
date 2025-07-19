namespace Dominio_Modelo
{
    public class Cancelacion
    {
        public int NroVenta { get; private set; }
        public DateTime Fecha { get; private set; }
        public string Motivo { get; private set; }

        public Cancelacion(int nroVenta, DateTime fecha, string motivo)
        {
            SetNroVenta(nroVenta);
            SetFecha(fecha);
            SetMotivo(motivo);
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

        public void SetMotivo(string motivo)
        {
            if (string.IsNullOrWhiteSpace(motivo))
                throw new ArgumentException("El motivo no puede ser nulo o vacío.", nameof(motivo));
            Motivo = motivo;
        }
    }
}
