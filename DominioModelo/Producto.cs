using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DominioModelo
{
    public class Producto
    {
        [Column("IdProducto")]
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public int IdTipoProducto { get; set; }

        // Se agregó una propiedad de navegación para la relación con TipoProducto.
        [ForeignKey("IdTipoProducto")]
        public TipoProducto? TipoProducto { get; set; }

        // Colecciones para las relaciones uno-a-muchos
        public ICollection<Precio> Precios { get; set; } = new List<Precio>();
        public ICollection<LineaVenta> LineasVenta { get; set; } = new List<LineaVenta>();
        public ICollection<LineaPedido> LineasPedido { get; set; } = new List<LineaPedido>();

        public Producto() { }

        public Producto(int id, string nombre, string descripcion, int stock, int idTipoProducto)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            Stock = stock;
            IdTipoProducto = idTipoProducto;
        }
    }
}
