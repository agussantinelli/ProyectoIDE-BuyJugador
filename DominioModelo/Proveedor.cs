using System.ComponentModel.DataAnnotations.Schema;

namespace DominioModelo
{
    // La clase hereda de Persona para reutilizar propiedades comunes.
    public class Proveedor : Persona
    {
        [Column("IdProveedor")]
        public int Id { get; set; }

        // Se agregaron las propiedades que faltaban en el modelo y que sí estaban en el SQL.
        public string RazonSocial { get; set; }

        // Colección para la relación uno-a-muchos con Pedido
        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

        // Agregamos un constructor sin parámetros, fundamental para EF Core.
        public Proveedor() : base() { }

        public Proveedor(int id, string nombre, string cuit, string email, string password, string telefono, string direccion, int idLocalidad)
            : base(nombre, cuit, email, password, telefono, direccion, idLocalidad)
        {
            Id = id;
            // Se agregó la propiedad RazonSocial
            RazonSocial = nombre;
        }
    }
}
