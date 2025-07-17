namespace Dominio_Modelo
{
    public class Cliente : Persona
    {
        public string Telefono { get; private set; }
        public int Edad { get; private set; }

        public Cliente(int legajo, string nombre, string mail, string contrasenia, string telefono, int edad)
            : base(legajo, nombre, mail, contrasenia)
        {
            SetTelefono(telefono);
            SetEdad(edad);
        }

        public void SetTelefono(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono))
                throw new System.ArgumentException("El teléfono no puede ser nulo o vacío.", nameof(telefono));
            Telefono = telefono;
        }

        public void SetEdad(int edad)
        {
            if (edad <= 0)
                throw new System.ArgumentException("La edad debe ser un número positivo.", nameof(edad));
            Edad = edad;
        }
    }
}