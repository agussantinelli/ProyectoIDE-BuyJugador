using DominioModelo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Data
{
    public partial class BuyJugadorContext : DbContext
    {
        public BuyJugadorContext(DbContextOptions<BuyJugadorContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LineaPedido> LineaPedidos { get; set; }
        public virtual DbSet<LineaVenta> LineaVentas { get; set; }
        public virtual DbSet<Localidad> Localidades { get; set; }
        public virtual DbSet<Pedido> Pedidos { get; set; }
        public virtual DbSet<Persona> Personas { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Proveedor> Proveedores { get; set; }
        public virtual DbSet<Provincia> Provincias { get; set; }
        public virtual DbSet<TipoProducto> TiposProductos { get; set; }
        public virtual DbSet<Venta> Ventas { get; set; }
        public virtual DbSet<ProductoProveedor> ProductoProveedores { get; set; }
        public virtual DbSet<PrecioCompra> PreciosCompra { get; set; }   
        public virtual DbSet<PrecioVenta> PreciosVenta { get; set; }     


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductoProveedor>(entity =>
            {
                entity.HasKey(e => new { e.IdProducto, e.IdProveedor });

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.ProductoProveedores)
                    .HasForeignKey(d => d.IdProducto);

                entity.HasOne(d => d.Proveedor)
                    .WithMany(p => p.ProductoProveedores)
                    .HasForeignKey(d => d.IdProveedor);
            });


            modelBuilder.Entity<LineaPedido>(entity =>
            {
                entity.HasKey(e => new { e.IdPedido, e.NroLineaPedido });

                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18,2)");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.LineasPedido)
                    .HasForeignKey(d => d.IdPedido)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.LineaPedido)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);
            });


            modelBuilder.Entity<LineaVenta>(entity =>
            {
                entity.HasKey(e => new { e.IdVenta, e.NroLineaVenta });
                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18,2)");
                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.LineaVenta)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);
            });

            modelBuilder.Entity<Localidad>(entity =>
            {
                entity.HasKey(e => e.IdLocalidad);
                entity.Property(e => e.Nombre).HasMaxLength(100);
                entity.HasOne(d => d.IdProvinciaNavigation)
                    .WithMany(p => p.Localidades)
                    .HasForeignKey(d => d.IdProvincia);
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.IdPedido);
                entity.Property(e => e.Estado).HasMaxLength(50);
                entity.Property(e => e.Fecha).HasColumnType("datetime");
                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdProveedor);
                entity.HasMany(d => d.LineasPedido)
                    .WithOne(p => p.IdPedidoNavigation)
                    .HasForeignKey(p => p.IdPedido)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.IdPersona);
                entity.HasIndex(e => e.Dni).IsUnique();
                entity.Property(e => e.Direccion).HasMaxLength(200);
                entity.Property(e => e.Dni).HasColumnName("DNI");
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.NombreCompleto).HasMaxLength(200);
                entity.Property(e => e.Password).HasMaxLength(100);
                entity.Property(e => e.Telefono).HasMaxLength(50);

                entity.Property(e => e.Estado).HasDefaultValue(true);
                entity.HasQueryFilter(p => p.Estado);

                entity.HasOne(d => d.IdLocalidadNavigation)
                    .WithMany(p => p.Personas)
                    .HasForeignKey(d => d.IdLocalidad);
            });

            modelBuilder.Entity<PrecioCompra>(entity =>
            {
                entity.HasKey(e => new { e.IdProducto, e.IdProveedor });
                entity.Property(e => e.Monto).HasColumnType("decimal(18,2)");

                entity.HasOne(e => e.Producto)
                    .WithMany(p => p.PreciosCompra)
                    .HasForeignKey(e => e.IdProducto)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);

                entity.HasOne(e => e.Proveedor)
                    .WithMany(p => p.PreciosCompra)
                    .HasForeignKey(e => e.IdProveedor)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);
            });


            modelBuilder.Entity<PrecioVenta>(entity =>
            {
                entity.HasKey(e => new { e.IdProducto, e.FechaDesde });
                entity.Property(e => e.Monto).HasColumnType("decimal(18,2)");
                entity.Property(e => e.FechaDesde).HasColumnType("datetime");

                entity.HasOne(e => e.Producto)
                    .WithMany(p => p.PreciosVenta)
                    .HasForeignKey(e => e.IdProducto)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto);
                entity.Property(e => e.Descripcion).HasMaxLength(255);
                entity.Property(e => e.Nombre).HasMaxLength(100);
                entity.Property(e => e.Activo).HasDefaultValue(true);
                entity.HasOne(d => d.IdTipoProductoNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdTipoProducto)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Producto>().HasQueryFilter(p => p.Activo);

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => e.IdProveedor);
                entity.Property(e => e.Cuit).HasMaxLength(20);
                entity.Property(e => e.Direccion).HasMaxLength(200);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.RazonSocial).HasMaxLength(200);
                entity.Property(e => e.Telefono).HasMaxLength(50);
                entity.Property(e => e.Activo).HasDefaultValue(true);
                entity.HasOne(d => d.IdLocalidadNavigation)
                    .WithMany(p => p.Proveedores)
                    .HasForeignKey(d => d.IdLocalidad);
            });

            modelBuilder.Entity<Proveedor>().HasQueryFilter(p => p.Activo);

            modelBuilder.Entity<Provincia>(entity =>
            {
                entity.HasKey(e => e.IdProvincia);
                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<TipoProducto>(entity =>
            {
                entity.HasKey(e => e.IdTipoProducto);
                entity.ToTable("TiposProducto");
                entity.Property(e => e.Descripcion).HasMaxLength(255);
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.HasKey(e => e.IdVenta);
                entity.Property(e => e.Estado).HasMaxLength(50);
                entity.Property(e => e.Fecha).HasColumnType("datetime");
                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdPersona);
                entity.HasMany(d => d.LineaVenta)
                    .WithOne(p => p.IdVentaNavigation)
                    .HasForeignKey(p => p.IdVenta)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ProductoProveedor>().HasQueryFilter(pp => pp.Producto.Activo && pp.Proveedor.Activo);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

