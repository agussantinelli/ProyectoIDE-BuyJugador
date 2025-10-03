using System.ComponentModel.DataAnnotations.Schema;

namespace DominioModelo
{
    public class ProductoProveedor
    {
        public int IdProducto { get; set; }
        public int IdProveedor { get; set; }

        [ForeignKey("IdProducto")]
        public virtual Producto Producto { get; set; }

        [ForeignKey("IdProveedor")]
        public virtual Proveedor Proveedor { get; set; }
    }
}
