using System.ComponentModel.DataAnnotations.Schema;

namespace DominioModelo
{
    // La clase hereda de Persona para reutilizar propiedades comunes.
    public class Duenio : Persona
    {
        // Se usa la anotación [Column("IdDuenio")] para que EF Core sepa que la clave primaria
        // se llama IdDuenio, en lugar de la convención predeterminada de "Id".
        [Column("IdDuenio")]
        public int Id { get; set; }

        // La propiedad IdDuenio original se puede mantener por claridad,
        // pero la anotación la vincula a la columna "Id" en la base de datos.
        // public int IdDuenio { get; set; } 

        // Agregamos un constructor sin parámetros, fundamental para EF Core.
        public Duenio() : base() { }

        // Se mantiene el constructor original, pero se ajusta a las propiedades de Persona
        public Duenio(int id, string nombre, string cuit, string email, string password, string telefono, string direccion, int idLocalidad)
            : base(nombre, cuit, email, password, telefono, direccion, idLocalidad)
        {
            Id = id;
        }
    }
}
