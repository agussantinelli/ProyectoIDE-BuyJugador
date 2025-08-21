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
            modelBuilder.Entity<Producto>().HasKey(p => p.IdProducto);
            modelBuilder.Entity<Venta>().HasKey(v => v.IdVenta);
            modelBuilder.Entity<Duenio>().HasKey(d => d.IdDuenio);
            modelBuilder.Entity<Empleado>().HasKey(e => e.IdEmpleado);
            modelBuilder.Entity<LineaPedido>().HasKey(lp => lp.IdLineaPedido);
            modelBuilder.Entity<LineaVenta>().HasKey(lv => lv.IdLineaVenta);
            modelBuilder.Entity<Localidad>().HasKey(l => l.IdLocalidad);
            modelBuilder.Entity<Pedido>().HasKey(p => p.IdPedido);
            modelBuilder.Entity<Precio>().HasKey(p => p.IdPrecio);
            modelBuilder.Entity<Proveedor>().HasKey(p => p.IdProveedor);
            modelBuilder.Entity<Provincia>().HasKey(p => p.IdProvincia);
            modelBuilder.Entity<TipoProducto>().HasKey(tp => tp.IdTipoProducto);

            base.OnModelCreating(modelBuilder);
        }
    }
}
