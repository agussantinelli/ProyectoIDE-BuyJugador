using System;
using System.Text.RegularExpressions;

namespace Dominio_Modelo
{
    public abstract class Persona
    {
        public int Dni { get; private set; }
        public string NombrePersona { get; private set; }
        public string MailPersona { get; private set; }
        public string Contrasenia { get; private set; }
        public string TelefonoPersona { get; private set; }

        protected Persona(int dni, string nombrePer, string mailPer, string contrasenia, string telefonoPer)
        {
            SetDni(dni);
            SetNombrePer(nombrePer);
            SetMailPer(mailPer);
            SetContrasenia(contrasenia);
            SetTelefonoPer(telefonoPer);
        }

        public void SetDni(int dni)
        {
            if (dni <= 0)
                throw new ArgumentException("El legajo debe ser un número positivo.", nameof(dni));
            Dni = dni;
        }

        public void SetNombrePer(string nombrePer)
        {
            if (string.IsNullOrWhiteSpace(nombrePer))
                throw new ArgumentException("El nombre no puede ser nulo o vacío.", nameof(nombrePer));
            NombrePer = nombrePer;
        }

        public void SetMailPer(string mailPer)
        {
            if (!EsMailValido(mailPer))
                throw new ArgumentException("El mail no tiene un formato válido.", nameof(mailPer));
            MailPer = mailPer;
        }

        public void SetContrasenia(string contrasenia)
        {
            if (string.IsNullOrWhiteSpace(contrasenia))
                throw new ArgumentException("La contraseña no puede ser nula o vacía.", nameof(contrasenia));
            Contrasenia = contrasenia;
        }

        public void SetTelefonoPer(string telefonoPer)
        {
            if (string.IsNullOrWhiteSpace(telefonoPer))
                throw new ArgumentException("El teléfono no puede ser nulo o vacío.", nameof(telefonoPer));
            TelefonoPer = telefonoPer;
        }

        private static bool EsMailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
    }
}