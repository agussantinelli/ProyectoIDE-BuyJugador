namespace DominioModelo
{
    public abstract class Persona
    {
        public string Nombre { get; set; }
        public int Cuit { get; set; }
        public string Email { get; set; }
        public int Telefono { get; set; }
        public string Direccion { get; set; }
        public int IdLocalidad { get; set; }

        protected Persona(string nombre, int cuit, string email, int telefono, string direccion, int idLocalidad)
        {
            Nombre = nombre;
            Cuit = cuit;
            Email = email;
            Telefono = telefono;
            Direccion = direccion;
            IdLocalidad = idLocalidad;
        }
    }
}
