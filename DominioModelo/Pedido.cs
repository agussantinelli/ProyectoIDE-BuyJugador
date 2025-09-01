using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DominioModelo
{
    public class Pedido
    {
        [Column("IdPedido")]
        public int Id { get; set; }

        public int IdProveedor { get; set; }

        // Propiedad de navegación a la colección de líneas de pedido
        public List<LineaPedido> LineasPedido { get; set; } = new List<LineaPedido>();

        // Propiedad de navegación para la relación con Proveedor
        [ForeignKey("IdProveedor")]
        public Proveedor? Proveedor { get; set; }

        public Pedido() { }

        public Pedido(int id, int idProveedor)
        {
            Id = id;
            IdProveedor = idProveedor;
        }
    }
}
