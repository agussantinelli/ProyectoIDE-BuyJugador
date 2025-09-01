using System.ComponentModel.DataAnnotations.Schema;

namespace DominioModelo
{
    [Table("TiposProducto")]
    public class TipoProducto
    {
        [Column("IdTipoProducto")]
        public int Id { get; set; }

        public string Descripcion { get; set; }

        // Colecci�n para la relaci�n uno-a-muchos con Producto
        public ICollection<Producto> Productos { get; set; } = new List<Producto>();

        // Agregamos un constructor sin par�metros, fundamental para EF Core.
        public TipoProducto() { }

        public TipoProducto(int id, string descripcion)
        {
            Id = id;
            Descripcion = descripcion;
        }
    }
}
