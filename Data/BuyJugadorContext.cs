using System;
using System.Collections.Generic;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data;

public partial class BuyJugadorContext : DbContext
{
    public BuyJugadorContext()
    {
    }

    public BuyJugadorContext(DbContextOptions<BuyJugadorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LineaPedido> LineaPedidos { get; set; }

    public virtual DbSet<LineaVenta> LineaVentas { get; set; }

    public virtual DbSet<Localidad> Localidades { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Precio> Precios { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedor> Proveedores { get; set; }

    public virtual DbSet<Provincia> Provincias { get; set; }

    public virtual DbSet<TipoProducto> TiposProductos { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=BuyJugador;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LineaPedido>(entity =>
        {
            entity.HasKey(e => new { e.IdPedido, e.NroLineaPedido }).HasName("PK__LineaPed__335638C56A652E8B");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.LineaPedidos)
                .HasForeignKey(d => d.IdPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LineaPedi__IdPed__70DDC3D8");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.LineaPedidos)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__LineaPedi__IdPro__71D1E811");
        });

        modelBuilder.Entity<LineaVenta>(entity =>
        {
            entity.HasKey(e => new { e.IdVenta, e.NroLineaVenta }).HasName("PK__LineaVen__159090F8C3994359");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.LineaVenta)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__LineaVent__IdPro__75A278F5");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.LineaVenta)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LineaVent__IdVen__74AE54BC");
        });

        modelBuilder.Entity<Localidad>(entity =>
        {
            entity.HasKey(e => e.IdLocalidad).HasName("PK__Localida__274326123C94DD46");

            entity.Property(e => e.Nombre).HasMaxLength(100);

            entity.HasOne(d => d.IdProvinciaNavigation).WithMany(p => p.Localidades)
                .HasForeignKey(d => d.IdProvincia)
                .HasConstraintName("FK__Localidad__IdPro__59FA5E80");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.IdPedido).HasName("PK__Pedidos__9D335DC3914988F0");

            entity.Property(e => e.Estado).HasMaxLength(50);
            entity.Property(e => e.Fecha).HasColumnType("datetime");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("FK__Pedidos__IdProve__6E01572D");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__Personas__2EC8D2ACAA780A71");

            entity.HasIndex(e => e.Dni, "UQ__Personas__C035B8DD0C35231B").IsUnique();

            entity.Property(e => e.Direccion).HasMaxLength(200);
            entity.Property(e => e.Dni).HasColumnName("DNI");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.NombreCompleto).HasMaxLength(200);
            entity.Property(e => e.Password).HasMaxLength(64);
            entity.Property(e => e.Telefono).HasMaxLength(50);

            entity.HasOne(d => d.IdLocalidadNavigation).WithMany(p => p.Personas)
                .HasForeignKey(d => d.IdLocalidad)
                .HasConstraintName("FK__Personas__IdLoca__68487DD7");
        });

        modelBuilder.Entity<Precio>(entity =>
        {
            entity.HasKey(e => new { e.IdProducto, e.FechaDesde }).HasName("PK__Precios__BFA8316E65DD157C");

            entity.Property(e => e.FechaDesde).HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Precios)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Precios__IdProdu__6477ECF3");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__098892107EA36979");

            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);

            entity.HasOne(d => d.IdTipoProductoNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdTipoProducto)
                .HasConstraintName("FK__Productos__IdTip__619B8048");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PK__Proveedo__E8B631AFDC9F8FEE");

            entity.Property(e => e.Cuit).HasMaxLength(20);
            entity.Property(e => e.Direccion).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.RazonSocial).HasMaxLength(200);
            entity.Property(e => e.Telefono).HasMaxLength(50);

            entity.HasOne(d => d.IdLocalidadNavigation).WithMany(p => p.Proveedores)
                .HasForeignKey(d => d.IdLocalidad)
                .HasConstraintName("FK__Proveedor__IdLoc__5EBF139D");
        });

        modelBuilder.Entity<Provincia>(entity =>
        {
            entity.HasKey(e => e.IdProvincia).HasName("PK__Provinci__EED744559063294B");

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<TipoProducto>(entity =>
        {
            entity.HasKey(e => e.IdTipoProducto).HasName("PK__TiposPro__A974F920A67A2F15");

            entity.ToTable("TiposProducto");

            entity.Property(e => e.Descripcion).HasMaxLength(255);
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__Ventas__BC1240BD1351B925");

            entity.Property(e => e.Estado).HasMaxLength(50);
            entity.Property(e => e.Fecha).HasColumnType("datetime");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdPersona)
                .HasConstraintName("FK__Ventas__IdPerson__6B24EA82");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
