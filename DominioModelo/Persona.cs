using System.ComponentModel.DataAnnotations.Schema;

namespace DominioModelo
{
    // Usamos 'abstract' para que esta clase solo pueda ser usada por herencia.
    public abstract class Persona
    {
        // Se agregaron las propiedades que faltaban en el modelo y que sí estaban en la base de datos.
        public string Nombre { get; set; }
        public string Cuit { get; set; } // El tipo se cambió a string para que coincida con la base de datos.
        public string Email { get; set; }
        public string Password { get; set; } // Se agregó la propiedad Password que estaba en el SQL.
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public int IdLocalidad { get; set; }

        // Se agregó una propiedad de navegación para la relación con Localidad.
        [ForeignKey("IdLocalidad")]
        public Localidad? Localidad { get; set; }

        // Agregamos un constructor sin parámetros, requerido por Entity Framework.
        public Persona() { }

        // Constructor para la inicialización
        protected Persona(string nombre, string cuit, string email, string password, string telefono, string direccion, int idLocalidad)
        {
            Nombre = nombre;
            Cuit = cuit;
            Email = email;
            Password = password;
            Telefono = telefono;
            Direccion = direccion;
            IdLocalidad = idLocalidad;
        }
    }
}
