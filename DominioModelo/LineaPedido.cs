using System.ComponentModel.DataAnnotations.Schema;

namespace DominioModelo
{
    public class LineaPedido
    {
        // Propiedad de clave primaria
        [Column("IdLineaPedido")]
        public int Id { get; set; }

        public int Cantidad { get; set; }

        // Claves foráneas para las relaciones
        public int IdPedido { get; set; }
        public int IdProducto { get; set; }

        // Propiedades de navegación para la relación con otras entidades
        [ForeignKey("IdPedido")]
        public Pedido? Pedido { get; set; }

        [ForeignKey("IdProducto")]
        public Producto? Producto { get; set; }

        public LineaPedido() { }

        public LineaPedido(int id, int cantidad, int idPedido, int idProducto)
        {
            Id = id;
            Cantidad = cantidad;
            IdPedido = idPedido;
            IdProducto = idProducto;
        }
    }
}
