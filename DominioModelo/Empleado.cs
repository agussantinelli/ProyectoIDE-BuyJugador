using System;

namespace DominioModelo
{
    public class Empleado : Persona
    {
        public DateTime FechaIngreso { get; private set; }

        public Empleado(int dni, string nombrePer, string mailPer, string contrasenia, string telefonoPer, DateTime fechaIngreso)
            : base(dni, nombrePer, mailPer, contrasenia, telefonoPer)
        {
            SetFechaIngreso(fechaIngreso);
        }

        public void SetFechaIngreso(DateTime fechaIngreso)
        {
            if (fechaIngreso > DateTime.Now)
                throw new ArgumentException("La fecha de ingreso no puede ser futura.", nameof(fechaIngreso));
            FechaIngreso = fechaIngreso;
        }
    }
}
