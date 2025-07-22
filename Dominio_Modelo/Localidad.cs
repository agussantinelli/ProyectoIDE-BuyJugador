using System;

namespace Dominio_Modelo
{
    public class Localidad
    {
        public int CodLocalidad { get; private set; }
        public string NombreLocalidad { get; private set; }
        public int CodProvincia { get; private set; }

        public Localidad(int codLocalidad, string nombreLocalidad, int codProvincia)
        {
            SetCodLocalidad(codLocalidad);
            SetNombreLocalidad(nombreLocalidad);
            SetCodProvincia(codProvincia);
        }

        public void SetCodLocalidad(int codLocalidad)
        {
            if (codLocalidad <= 0)
                throw new ArgumentException("El código de localidad debe ser positivo.", nameof(codLocalidad));
            CodLocalidad = codLocalidad;
        }

        public void SetNombreLocalidad(string nombreLoc)
        {
            if (string.IsNullOrWhiteSpace(nombreLoc))
                throw new ArgumentException("El nombre de la localidad no puede ser nulo o vacío.", nameof(nombreLoc));
            NombreLocalidad = nombreLoc;
        }

        public void SetCodProvincia(int codProvincia)
        {
            if (codProvincia <= 0)
                throw new ArgumentException("El código de provincia debe ser positivo.", nameof(codProvincia));
            CodProvincia = codProvincia;
        }
    }
}