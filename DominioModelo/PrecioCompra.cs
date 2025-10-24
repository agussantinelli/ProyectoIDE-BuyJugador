using System.ComponentModel.DataAnnotations.Schema;

namespace DominioModelo
{
    public partial class PrecioCompra
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal Monto { get; set; }

        public int IdProducto { get; set; }

        public int IdProveedor { get; set; }

        public virtual Producto Producto { get; set; } = null!;

        public virtual Proveedor Proveedor { get; set; } = null!;
    }
}
