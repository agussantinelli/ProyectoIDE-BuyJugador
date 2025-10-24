using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DominioModelo
{
    public partial class Producto
    {
        public Producto()
        {
            LineaPedido = new HashSet<LineaPedido>();
            LineaVenta = new HashSet<LineaVenta>();
            ProductoProveedores = new HashSet<ProductoProveedor>();
            PreciosCompra = new HashSet<PrecioCompra>();
            PreciosVenta = new HashSet<PrecioVenta>();
        }

        [Key]
        public int IdProducto { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(255)]
        public string Descripcion { get; set; }

        public int Stock { get; set; }

        public int? IdTipoProducto { get; set; }

        public bool Activo { get; set; }

        [ForeignKey("IdTipoProducto")]
        public virtual TipoProducto IdTipoProductoNavigation { get; set; }

        public virtual ICollection<LineaVenta> LineaVenta { get; set; }

        public virtual ICollection<LineaPedido> LineaPedido { get; set; }

        public virtual ICollection<ProductoProveedor> ProductoProveedores { get; set; }

        public virtual ICollection<PrecioCompra> PreciosCompra { get; set; } 

        public virtual ICollection<PrecioVenta> PreciosVenta { get; set; }
    }
}

