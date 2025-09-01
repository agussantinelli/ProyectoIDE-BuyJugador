using System.ComponentModel.DataAnnotations.Schema;

namespace DominioModelo
{
    public class LineaVenta
    {
        [Column("IdLineaVenta")]
        public int Id { get; set; }

        public int Cantidad { get; set; }

        public int IdVenta { get; set; }
        public int IdProducto { get; set; }

        // Propiedades de navegación para la relación con otras entidades
        [ForeignKey("IdVenta")]
        public Venta? Venta { get; set; }

        [ForeignKey("IdProducto")]
        public Producto? Producto { get; set; }

        public LineaVenta() { }

        public LineaVenta(int id, int cantidad, int idVenta, int idProducto)
        {
            Id = id;
            Cantidad = cantidad;
            IdVenta = idVenta;
            IdProducto = idProducto;
        }
    }
}
