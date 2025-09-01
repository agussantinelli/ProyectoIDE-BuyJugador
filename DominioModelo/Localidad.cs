using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DominioModelo
{
    public class Localidad
    {
        [Column("IdLocalidad")]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public int IdProvincia { get; set; }

        // Propiedad de navegación para la relación con Provincia
        [ForeignKey("IdProvincia")]
        public Provincia? Provincia { get; set; }

        // Colecciones para las relaciones uno-a-muchos
        public ICollection<Duenio> Duenios { get; set; } = new List<Duenio>();
        public ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
        public ICollection<Proveedor> Proveedores { get; set; } = new List<Proveedor>();

        public Localidad() { }

        public Localidad(int id, string nombre, int idProvincia)
        {
            Id = id;
            Nombre = nombre;
            IdProvincia = idProvincia;
        }
    }
}
