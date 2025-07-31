using System;

namespace DominioModelo
{
    public class Localidad
    {
        public int CodigoLocalidad { get; private set; }
        public string NombreLocalidad { get; private set; }
        public int CodigoProvincia { get; private set; }

        public Localidad(int codigoLocalidad, string nombreLocalidad, int codigoProvincia)
        {
            SetCodigoLocalidad(codigoLocalidad);
            SetNombreLocalidad(nombreLocalidad);
            SetCodigoProvincia(codigoProvincia);
        }

        public void SetCodigoLocalidad(int codigoLocalidad)
        {
            if (codigoLocalidad <= 0)
                throw new ArgumentException("El código de localidad debe ser positivo.", nameof(codigoLocalidad));
            CodigoLocalidad = codigoLocalidad;
        }

        public void SetNombreLocalidad(string nombreLocalidad)
        {
            if (string.IsNullOrWhiteSpace(nombreLocalidad))
                throw new ArgumentException("El nombre de la localidad no puede ser nulo o vacío.", nameof(nombreLocalidad));
            NombreLocalidad = nombreLocalidad;
        }

        public void SetCodigoProvincia(int codigoProvincia)
        {
            if (codigoProvincia <= 0)
                throw new ArgumentException("El código de provincia debe ser positivo.", nameof(codigoProvincia));
            CodigoProvincia = codigoProvincia;
        }
    }
}