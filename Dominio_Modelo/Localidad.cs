using System;

namespace Dominio_Modelo
{
    public class Localidad
    {
        public int CodLocalidad { get; private set; }
        public string Nombre { get; private set; }
        public int CodProvincia { get; private set; }

        public Localidad(int codLocalidad, string nombre, int codProvincia)
        {
            SetCodLocalidad(codLocalidad);
            SetNombre(nombre);
            SetCodProvincia(codProvincia);
        }

        public void SetCodLocalidad(int codLocalidad)
        {
            if (codLocalidad <= 0)
                throw new ArgumentException("El código de localidad debe ser positivo.", nameof(codLocalidad));
            CodLocalidad = codLocalidad;
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede ser nulo o vacío.", nameof(nombre));
            Nombre = nombre;
        }

        public void SetCodProvincia(int codProvincia)
        {
            if (codProvincia <= 0)
                throw new ArgumentException("El código de provincia debe ser positivo.", nameof(codProvincia));
            CodProvincia = codProvincia;
        }
    }
}