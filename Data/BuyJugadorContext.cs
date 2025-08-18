
using DominioModelo;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class BuyJugadorContext : DbContext
    {
        public BuyJugadorContext(DbContextOptions<BuyJugadorContext> options) : base(options)
        {
        }

        public DbSet<Duenio> Duenios { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<LineaPedido> LineasPedido { get; set; }
        public DbSet<LineaVenta> LineasVenta { get; set; }
        public DbSet<Localidad> Localidades { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Precio> Precios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<TipoProducto> TiposProducto { get; set; }
        public DbSet<Venta> Ventas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aquí puedes agregar configuraciones adicionales de Fluent API si las necesitas.
            // Por ejemplo, para configurar relaciones complejas, claves, etc.
            // Por ahora, lo dejaremos simple y EF Core inferirá la mayoría de las relaciones.
            base.OnModelCreating(modelBuilder);
        }
    }
}