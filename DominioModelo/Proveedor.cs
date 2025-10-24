using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DominioModelo
{
    public partial class Proveedor
    {
        public Proveedor()
        {
            Pedidos = new HashSet<Pedido>();
            ProductoProveedores = new HashSet<ProductoProveedor>();
            PreciosCompra = new HashSet<PrecioCompra>();
        }

        [Key]
        public int IdProveedor { get; set; }

        [Required]
        [StringLength(200)]
        public string RazonSocial { get; set; }

        [StringLength(20)]
        public string Cuit { get; set; }

        [StringLength(50)]
        public string Telefono { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(200)]
        public string Direccion { get; set; }

        public int? IdLocalidad { get; set; }

        public bool Activo { get; set; }

        [ForeignKey("IdLocalidad")]
        public virtual Localidad IdLocalidadNavigation { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; }

        public virtual ICollection<PrecioCompra> PreciosCompra { get; set; }

        public virtual ICollection<ProductoProveedor> ProductoProveedores { get; set; }
    }
}
