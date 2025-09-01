using System.ComponentModel.DataAnnotations.Schema;

namespace DominioModelo
{
    public class Precio
    {
        [Column("IdPrecio")]
        public int Id { get; set; }

        // El tipo de dato se cambi� a decimal para coincidir con la base de datos
        // y para evitar problemas de precisi�n con dinero.
        public decimal Monto { get; set; }

        // Se agreg� la propiedad que faltaba y que s� estaba en el SQL
        public DateTime FechaDesde { get; set; }

        public int IdProducto { get; set; }

        // Propiedad de navegaci�n a la entidad padre.
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
