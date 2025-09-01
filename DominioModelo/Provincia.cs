using System.ComponentModel.DataAnnotations.Schema;

namespace DominioModelo
{
    [Table("Provincias")]
    public class Provincia
    {
        [Column("IdProvincia")]
        public int Id { get; set; }

        public string Nombre { get; set; }

        // Colecci�n para la relaci�n uno-a-muchos con Localidad
        public ICollection<Localidad> Localidades { get; set; } = new List<Localidad>();

        // Agregamos un constructor sin par�metros, fundamental para EF Core.
        public Provincia() { }

        public Provincia(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
    }
}
