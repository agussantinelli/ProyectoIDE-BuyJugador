using System.ComponentModel.DataAnnotations.Schema;

namespace DominioModelo
{
    [Table("Ventas")]
    public class Venta
    {
        [Column("IdVenta")]
        public int Id { get; set; }

        public int IdEmpleado { get; set; }

        // Colecci�n para la relaci�n uno-a-muchos con LineaVenta
        public List<LineaVenta> LineasVenta { get; set; } = new List<LineaVenta>();

        // Propiedad de navegaci�n para la relaci�n con Empleado
        [ForeignKey("IdEmpleado")]
        public Empleado? Empleado { get; set; }

        // Agregamos un constructor sin par�metros, fundamental para EF Core.
        public Venta() { }

        public Venta(int id, int idEmpleado)
        {
            Id = id;
            IdEmpleado = idEmpleado;
        }
    }
}
