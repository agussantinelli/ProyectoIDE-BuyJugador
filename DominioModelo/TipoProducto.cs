using System.ComponentModel.DataAnnotations.Schema;

namespace DominioModelo
{
    [Table("TiposProducto")]
    public class TipoProducto
    {
        [Column("IdTipoProducto")]
        public int Id { get; set; }

        public string Descripcion { get; set; }

        // Colección para la relación uno-a-muchos con Producto
        public ICollection<Producto> Productos { get; set; } = new List<Producto>();

        // Agregamos un constructor sin parámetros, fundamental para EF Core.
        public TipoProducto() { }

        public TipoProducto(int id, string descripcion)
        {
            Id = id;
            Descripcion = descripcion;
        }
    }
}
