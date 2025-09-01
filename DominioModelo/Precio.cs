using System.ComponentModel.DataAnnotations.Schema;

namespace DominioModelo
{
    public class Precio
    {
        [Column("IdPrecio")]
        public int Id { get; set; }

        // El tipo de dato se cambió a decimal para coincidir con la base de datos
        // y para evitar problemas de precisión con dinero.
        public decimal Monto { get; set; }

        // Se agregó la propiedad que faltaba y que sí estaba en el SQL
        public DateTime FechaDesde { get; set; }

        public int IdProducto { get; set; }

        // Propiedad de navegación a la entidad padre.
        [ForeignKey("IdProducto")]
        public Producto? Producto { get; set; }

        public Precio() { }

        public Precio(int id, decimal monto, DateTime fechaDesde, int idProducto)
        {
            Id = id;
            Monto = monto;
            FechaDesde = fechaDesde;
            IdProducto = idProducto;
        }
    }
}
