using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DominioModelo
{
    public partial class Pedido
    {
        [Key]
        public int IdPedido { get; set; }

        public DateTime Fecha { get; set; }
        [Required]
        [StringLength(50)]

        public string Estado { get; set; }

        public int? IdProveedor { get; set; }

        [ForeignKey("IdProveedor")]
        public virtual Proveedor IdProveedorNavigation { get; set; }

        public virtual ICollection<LineaPedido> LineasPedido { get; set; } = new List<LineaPedido>();
    }
}

