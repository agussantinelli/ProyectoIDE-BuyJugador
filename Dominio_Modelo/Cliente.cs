using System;

namespace Dominio_Modelo
{
    public class Cliente : Persona
    {
        public DateTime FechaNacimiento { get; private set; }
        public string Telefono { get; private set; }
        public int CodLocalidad { get; private set; }
        public int Edad { get; private set; }

        public Cliente(int legajo, string nombre, string mail, string contrasenia, 
                      DateTime fechaNacimiento, string telefono, int codLocalidad, int edad)
            : base(legajo, nombre, mail, contrasenia)
        {
            SetFechaNacimiento(fechaNacimiento);
            SetTelefono(telefono);
            SetCodLocalidad(codLocalidad);
            SetEdad(edad);
        }

        public void SetFechaNacimiento(DateTime fechaNacimiento)
        {
            if (fechaNacimiento == default || fechaNacimiento > DateTime.Now)
                throw new ArgumentException("Fecha de nacimiento no válida.", nameof(fechaNacimiento));
            FechaNacimiento = fechaNacimiento;
        }

        public void SetTelefono(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono))
                throw new ArgumentException("El teléfono no puede ser nulo o vacío.", nameof(telefono));
            Telefono = telefono;
        }

        public void SetCodLocalidad(int codLocalidad)
        {
            if (codLocalidad <= 0)
                throw new ArgumentException("El código de localidad debe ser positivo.", nameof(codLocalidad));
            CodLocalidad = codLocalidad;
        }

        public void SetEdad(int edad)
        {
            if (edad <= 0)
                throw new ArgumentException("La edad debe ser un número positivo.", nameof(edad));
            Edad = edad;
        }
    }
}
