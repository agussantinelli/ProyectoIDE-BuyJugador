using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DominioModelo
{
    public partial class PrecioVenta
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal Monto { get; set; }

        public DateTime FechaDesde { get; set; }
        public int IdProducto { get; set; }

        public virtual Producto Producto { get; set; } = null!;
    }
}
