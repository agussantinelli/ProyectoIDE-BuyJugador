using System;
using System.Text.RegularExpressions;

namespace Dominio_Modelo
{
    public abstract class Persona
    {
        public int Legajo { get; private set; }
        public string Nombre { get; private set; }
        public string Mail { get; private set; }
        public string Contrasenia { get; private set; }

        protected Persona(int legajo, string nombre, string mail, string contrasenia)
        {
            SetLegajo(legajo);
            SetNombre(nombre);
            SetMail(mail);
            SetContrasenia(contrasenia);
        }

        public void SetLegajo(int legajo)
        {
            if (legajo <= 0)
                throw new ArgumentException("El legajo debe ser un número positivo.", nameof(legajo));
            Legajo = legajo;
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede ser nulo o vacío.", nameof(nombre));
            Nombre = nombre;
        }

        public void SetMail(string mail)
        {
            if (!EsMailValido(mail))
                throw new ArgumentException("El mail no tiene un formato válido.", nameof(mail));
            Mail = mail;
        }

        public void SetContrasenia(string contrasenia)
        {
            if (string.IsNullOrWhiteSpace(contrasenia))
                throw new ArgumentException("La contraseña no puede ser nula o vacía.", nameof(contrasenia));
            Contrasenia = contrasenia;
        }

        private static bool EsMailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
    }
}