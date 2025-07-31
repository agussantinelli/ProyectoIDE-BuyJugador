using System;
using System.Text.RegularExpressions;

namespace DominioModelo
{
    public abstract class Persona
    {
        public int Dni { get; private set; }
        public string NombrePersona { get; private set; }
        public string MailPersona { get; private set; }
        public string Contrasenia { get; private set; }
        public string TelefonoPersona { get; private set; }

        protected Persona(int dni, string nombrePersona, string mailPersona, string contrasenia, string telefonoPersona)
        {
            SetDni(dni);
            SetNombrePersona(nombrePersona);
            SetMailPersona(mailPersona);
            SetContrasenia(contrasenia);
            SetTelefonoPersona(telefonoPersona);
        }

        public void SetDni(int dni)
        {
            if (dni <= 0)
                throw new ArgumentException("El legajo debe ser un número positivo.", nameof(dni));
            Dni = dni;
        }

        public void SetNombrePersona(string nombrePersona)
        {
            if (string.IsNullOrWhiteSpace(nombrePersona))
                throw new ArgumentException("El nombre no puede ser nulo o vacío.", nameof(nombrePersona));
            NombrePersona = nombrePersona;
        }

        public void SetMailPersona(string mailPersona)
        {
            if (!EsMailValido(mailPersona))
                throw new ArgumentException("El mail no tiene un formato válido.", nameof(mailPersona));
            MailPersona = mailPersona;
        }

        public void SetContrasenia(string contrasenia)
        {
            if (string.IsNullOrWhiteSpace(contrasenia))
                throw new ArgumentException("La contraseña no puede ser nula o vacía.", nameof(contrasenia));
            Contrasenia = contrasenia;
        }

        public void SetTelefonoPersona(string telefonoPersona)
        {
            if (string.IsNullOrWhiteSpace(telefonoPersona))
                throw new ArgumentException("El teléfono no puede ser nulo o vacío.", nameof(telefonoPersona));
            TelefonoPersona = telefonoPersona;
        }

        private static bool EsMailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
    }
}