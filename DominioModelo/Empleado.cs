using System.ComponentModel.DataAnnotations.Schema;

namespace DominioModelo
{
    // La clase hereda de Persona para reutilizar propiedades comunes.
    public class Empleado : Persona
    {
        [Column("IdEmpleado")]
        public int Id { get; set; }

        public DateTime FechaAlta { get; set; }

        public Empleado() : base() { }

        public Empleado(int id, string nombre, string cuit, string email, string password, string telefono, string direccion, int idLocalidad, DateTime fechaAlta)
            : base(nombre, cuit, email, password, telefono, direccion, idLocalidad)
        {
            Id = id;
            FechaAlta = fechaAlta;
        }
    }
}
