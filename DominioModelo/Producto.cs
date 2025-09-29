using System.Collections.Generic;

namespace DominioModelo
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public bool Activo { get; set; } // Propiedad restaurada
        public int? IdTipoProducto { get; set; }

        // Propiedades de navegación con los nombres correctos que espera el DbContext
        public virtual TipoProducto IdTipoProductoNavigation { get; set; }
        public virtual ICollection<LineaPedido> LineaPedidos { get; set; } = new List<LineaPedido>();
        public virtual ICollection<LineaVenta> LineaVenta { get; set; } = new List<LineaVenta>();
        public virtual ICollection<Precio> Precios { get; set; } = new List<Precio>();
    }
}

