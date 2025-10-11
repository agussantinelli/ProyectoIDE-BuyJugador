using Data;
using DominioModelo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public static class DbSeeder
{
    public static async Task SeedAsync(BuyJugadorContext context)
    {
        context.Database.EnsureDeleted();  //Descomentar para resetear la base de datos
        context.Database.EnsureCreated();

        await SeedProvinciasYLocalidadesAsync(context);
        await SeedTiposProductoAsync(context);
        await SeedPersonasAsync(context);
        await SeedProveedoresAsync(context);
        await SeedProductosConPreciosVentaAsync(context);
        await SeedRelacionesYPreciosCompraAsync(context);
        await SeedVentasAsync(context);
        await SeedPedidosAsync(context);

        Console.WriteLine("✅ Database seeded successfully!");
    }

    private static async Task SeedProvinciasYLocalidadesAsync(BuyJugadorContext context)
    {
        if (context.Provincias.Any()) return;
        Console.WriteLine("Seeding Provincias y Localidades...");

        using var httpClient = new HttpClient();

        var responseProvincias = await GetWithRetryAsync(httpClient, "https://apis.datos.gob.ar/georef/api/provincias?campos=id,nombre");
        var apiResponseProvincias = JsonSerializer.Deserialize<ApiResponseProvincias>(responseProvincias, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (apiResponseProvincias?.Provincias == null)
        {
            Console.WriteLine("Error al obtener las provincias.");
            return;
        }

        var provincias = apiResponseProvincias.Provincias.Select(p => new Provincia { Nombre = p.Nombre }).ToList();
        var caba = provincias.FirstOrDefault(p => p.Nombre == "Ciudad Autónoma de Buenos Aires");
        if (caba != null) caba.Nombre = "CABA";

        context.Provincias.AddRange(provincias);
        await context.SaveChangesAsync();
        Console.WriteLine("Provincias sembradas.");

        var provinciasDbMap = await context.Provincias.ToDictionaryAsync(p => p.Nombre, p => p.IdProvincia);

        var responseLocalidades = await GetWithRetryAsync(httpClient, "https://apis.datos.gob.ar/georef/api/municipios?campos=nombre,provincia&max=2000");
        var apiResponseLocalidades = JsonSerializer.Deserialize<ApiResponseMunicipios>(responseLocalidades, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (apiResponseLocalidades?.Municipios != null)
        {
            var todasLasLocalidades = apiResponseLocalidades.Municipios.Select(m => {
                var nombreProvincia = m.Provincia.Nombre == "Ciudad Autónoma de Buenos Aires" ? "CABA" : m.Provincia.Nombre;
                return new Localidad
                {
                    Nombre = m.Nombre,
                    IdProvincia = provinciasDbMap.ContainsKey(nombreProvincia) ? provinciasDbMap[nombreProvincia] : (int?)null
                };
            }).Where(l => l.IdProvincia.HasValue).ToList();

            context.Localidades.AddRange(todasLasLocalidades);
            await context.SaveChangesAsync();
            Console.WriteLine("Localidades sembradas.");
        }
    }

    private static async Task SeedTiposProductoAsync(BuyJugadorContext context)
    {
        if (context.TiposProductos.Any()) return;
        Console.WriteLine("Seeding Tipos de Producto...");
        context.TiposProductos.AddRange(GetTiposProducto());
        await context.SaveChangesAsync();
    }

    private static async Task SeedPersonasAsync(BuyJugadorContext context)
    {
        if (context.Personas.IgnoreQueryFilters().Any()) return;
        Console.WriteLine("Seeding Personas...");
        var locs = await context.Localidades.ToListAsync();
        if (locs.Any())
        {
            context.Personas.AddRange(GetPersonas(locs));
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedProveedoresAsync(BuyJugadorContext context)
    {
        if (context.Proveedores.IgnoreQueryFilters().Any()) return;
        Console.WriteLine("Seeding Proveedores...");
        var localidades = await context.Localidades.ToListAsync();
        var proveedores = GetProveedores(localidades);
        if (proveedores.Any())
        {
            context.Proveedores.AddRange(proveedores);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedProductosConPreciosVentaAsync(BuyJugadorContext context)
    {
        if (context.Productos.IgnoreQueryFilters().Any()) return;
        Console.WriteLine("Seeding Productos y Precios de Venta...");
        var tipos = await context.TiposProductos.ToListAsync();
        context.Productos.AddRange(GetProductosConPreciosVenta(tipos));
        await context.SaveChangesAsync();
    }

    private static async Task SeedRelacionesYPreciosCompraAsync(BuyJugadorContext context)
    {
        if (context.ProductoProveedores.Any()) return;
        Console.WriteLine("Seeding Relaciones Producto-Proveedor y Precios de Compra...");

        var proveedores = await context.Proveedores.ToListAsync();
        var productos = await context.Productos.Include(p => p.PreciosVenta).ToListAsync();
        if (!proveedores.Any() || !productos.Any()) return;

        var random = new Random();

        using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            foreach (var proveedor in proveedores)
            {
                var productosAsignados = proveedor.RazonSocial.Contains("Softy Systems", StringComparison.OrdinalIgnoreCase)
                    ? productos
                    : productos.OrderBy(p => random.Next()).Take(random.Next(5, 10)).ToList();

                foreach (var producto in productosAsignados)
                {
                    context.ProductoProveedores.Add(new ProductoProveedor
                    {
                        IdProveedor = proveedor.IdProveedor,
                        IdProducto = producto.IdProducto
                    });

                    var precioVenta = producto.PreciosVenta.FirstOrDefault()?.Monto ?? 10000m;
                    var precioCompraFinal = CalcularPrecioCompra(producto, proveedor, precioVenta);
                    context.PreciosCompra.Add(new PrecioCompra
                    {
                        IdProveedor = proveedor.IdProveedor,
                        IdProducto = producto.IdProducto,
                        Monto = precioCompraFinal
                    });
                }
            }
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            Console.WriteLine("Relaciones y Precios de Compra generados correctamente.");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"Error al sembrar relaciones y precios de compra: {ex.Message}");
        }
    }

    private static async Task SeedVentasAsync(BuyJugadorContext context)
    {
        if (context.Ventas.Any()) return;
        Console.WriteLine("Seeding Ventas...");

        var personas = await context.Personas.Where(p => p.Estado).ToListAsync();
        var productos = await context.Productos.Include(p => p.PreciosVenta).ToListAsync();
        if (!personas.Any() || !productos.Any()) return;

        var random = new Random();
        var ventas = new List<Venta>();
        ventas.Add(new Venta { Fecha = DateTime.UtcNow.AddDays(-10), Estado = "Finalizada", IdPersona = personas[0].IdPersona });
        if (personas.Count > 1)
        {
            ventas.Add(new Venta { Fecha = DateTime.UtcNow.AddDays(-5), Estado = "Pendiente", IdPersona = personas[1].IdPersona });
        }
        context.Ventas.AddRange(ventas);
        await context.SaveChangesAsync();

        var lineasVenta = new List<LineaVenta>();
        int nroLineaCounter = 1;
        foreach (var venta in ventas)
        {
            var productosParaVenta = productos.OrderBy(x => random.Next()).Take(random.Next(2, 4));
            foreach (var producto in productosParaVenta)
            {
                var precioVigente = producto.PreciosVenta.Where(pv => pv.FechaDesde <= venta.Fecha).OrderByDescending(pv => pv.FechaDesde).FirstOrDefault();
                if (precioVigente != null)
                {
                    lineasVenta.Add(new LineaVenta
                    {
                        IdVenta = venta.IdVenta,
                        NroLineaVenta = nroLineaCounter++,
                        IdProducto = producto.IdProducto,
                        Cantidad = random.Next(1, 4),
                        PrecioUnitario = precioVigente.Monto
                    });
                }
            }
        }
        context.LineaVentas.AddRange(lineasVenta);
        await context.SaveChangesAsync();
    }

    private static async Task SeedPedidosAsync(BuyJugadorContext context)
    {
        if (context.Pedidos.Any()) return;
        Console.WriteLine("Seeding Pedidos...");

        var proveedores = await context.Proveedores.Where(p => p.Activo).OrderBy(p => p.IdProveedor).ToListAsync();
        var productosConPrecio = await context.PreciosCompra.ToListAsync();
        if (!proveedores.Any() || !productosConPrecio.Any()) return;

        var random = new Random();
        var pedidos = new List<Pedido>();
        for (int i = 0; i < Math.Min(proveedores.Count, 3); i++)
        {
            pedidos.Add(new Pedido { Fecha = DateTime.UtcNow.AddDays(-i * 7), Estado = i % 2 == 0 ? "Recibido" : "Pendiente", IdProveedor = proveedores[i].IdProveedor });
        }
        context.Pedidos.AddRange(pedidos);
        await context.SaveChangesAsync();

        var lineasPedido = new List<LineaPedido>();
        int nroLineaPedido = 1;
        foreach (var pedido in pedidos)
        {
            var productosDelProveedor = productosConPrecio.Where(pc => pc.IdProveedor == pedido.IdProveedor).OrderBy(x => random.Next()).Take(5);
            foreach (var productoPrecio in productosDelProveedor)
            {
                decimal precioDiferente = Math.Round(productoPrecio.Monto * (1 + (nroLineaPedido % 4) * 0.03m), 2);
                lineasPedido.Add(new LineaPedido
                {
                    IdPedido = pedido.IdPedido,
                    NroLineaPedido = nroLineaPedido,
                    IdProducto = productoPrecio.IdProducto,
                    Cantidad = nroLineaPedido % 2 == 0 ? 10 : 5,
                    PrecioUnitario = precioDiferente
                });
                nroLineaPedido++;
            }
        }
        context.LineaPedidos.AddRange(lineasPedido);
        await context.SaveChangesAsync();
    }

    private static IEnumerable<TipoProducto> GetTiposProducto() => new List<TipoProducto>
    {
        // # RESTAURADO: Se eliminó la propiedad 'Activo' ya que no existe en el modelo.
        new TipoProducto { Descripcion = "Componentes" }, new TipoProducto { Descripcion = "Monitores" }, new TipoProducto { Descripcion = "Parlantes" },
        new TipoProducto { Descripcion = "Teclados" }, new TipoProducto { Descripcion = "Mouse" }, new TipoProducto { Descripcion = "Impresoras" },
        new TipoProducto { Descripcion = "Scanners" }, new TipoProducto { Descripcion = "Tabletas" }, new TipoProducto { Descripcion = "Laptops" },
        new TipoProducto { Descripcion = "Desktop" }, new TipoProducto { Descripcion = "Servidores" }, new TipoProducto { Descripcion = "Redes" },
        new TipoProducto { Descripcion = "Almacenamiento" }, new TipoProducto { Descripcion = "Software" }, new TipoProducto { Descripcion = "Accesorios" },
        new TipoProducto { Descripcion = "Cámaras" }, new TipoProducto { Descripcion = "Proyectores" }, new TipoProducto { Descripcion = "Audio Profesional" },
        new TipoProducto { Descripcion = "Gaming" }, new TipoProducto { Descripcion = "Smartphones" }
    };

    private static IEnumerable<Persona> GetPersonas(List<Localidad> locs)
    {
        var rosario = locs.FirstOrDefault(l => l.Nombre == "Rosario");
        var cordoba = locs.FirstOrDefault(l => l.Nombre == "Córdoba");

        return new List<Persona>
        {
            new Persona { NombreCompleto = "Tomas Levrand", Dni = 46191695, Email = "tomylevrand@buyjugador.com", Password = BCrypt.Net.BCrypt.HashPassword("tomy"), Telefono = "3416668877", Direccion = "Mendoza 2138", IdLocalidad = rosario?.IdLocalidad, FechaIngreso = new DateOnly(2025, 9, 23), Estado = true },
            new Persona { NombreCompleto = "Martin Ratti", Dni = 12345678, Email = "marto@buyjugador.com", Password = BCrypt.Net.BCrypt.HashPassword("admin"), Telefono = "34115559101", Direccion = "Falsa 123", IdLocalidad = rosario?.IdLocalidad, Estado = true },
            new Persona { NombreCompleto = "Frank Fabra", Dni = 41111111, Email = "fabra@email.com", Password = BCrypt.Net.BCrypt.HashPassword("boca123"), Telefono = "3411111111", Direccion = "Verdadera 456", IdLocalidad = cordoba?.IdLocalidad, Estado = true },
            new Persona { NombreCompleto = "Joaquin Peralta", Dni = 44444444, Email = "joaquin@buyjugador.com", Password = BCrypt.Net.BCrypt.HashPassword("empleado1"), Telefono = "115550202", Direccion = "Avenida Imaginaria 2", IdLocalidad = cordoba?.IdLocalidad, FechaIngreso = new DateOnly(2022, 5, 10), Estado = true },
            new Persona { NombreCompleto = "Ayrton Costa", Dni = 42333444, Email = "ayrton@email.com", Password = BCrypt.Net.BCrypt.HashPassword("empleado2"), Telefono = "3415552222", Direccion = "Calle Demo 4", IdLocalidad = rosario?.IdLocalidad, FechaIngreso = new DateOnly(2023, 2, 15), Estado = true },
            new Persona { NombreCompleto = "Luka Doncic", Dni = 42553400, Email = "luka@email.com", Password = BCrypt.Net.BCrypt.HashPassword("empleado3"), Telefono = "3415882922", Direccion = "Calle Prueba 5", IdLocalidad = rosario?.IdLocalidad, FechaIngreso = new DateOnly(2022, 8, 30), Estado = true },
            new Persona { NombreCompleto = "Stephen Curry", Dni = 32393404, Email = "curry@email.com", Password = BCrypt.Net.BCrypt.HashPassword("empleado4"), Telefono = "3415559202", Direccion = "Calle Test 6", IdLocalidad = rosario?.IdLocalidad, FechaIngreso = new DateOnly(2021, 11, 5), Estado = true },
            new Persona { NombreCompleto = "Agustin Santinelli", Dni = 46294992, Email = "agustinsantinelli@buyjugador.com", Password = BCrypt.Net.BCrypt.HashPassword("agustin"), Telefono = "3416667777", Direccion = "Molina 2022", IdLocalidad = rosario?.IdLocalidad, FechaIngreso = new DateOnly(2025, 9, 23), Estado = true },
            new Persona { NombreCompleto = "Carlos Lopez", Dni = 28765432, Email = "carlos.l@email.com", Password = BCrypt.Net.BCrypt.HashPassword("empleado6"), Telefono = "3417778888", Direccion = "Calle Secundaria 321", IdLocalidad = rosario?.IdLocalidad, FechaIngreso = new DateOnly(2022, 12, 10), Estado = true },
            new Persona { NombreCompleto = "Ana Martinez", Dni = 39876543, Email = "ana.m@email.com", Password = BCrypt.Net.BCrypt.HashPassword("empleado7"), Telefono = "3418889999", Direccion = "Pasaje Privado 654", IdLocalidad = cordoba?.IdLocalidad, FechaIngreso = new DateOnly(2023, 3, 25), Estado = true },
            new Persona { NombreCompleto = "Pedro Rodriguez", Dni = 40987654, Email = "pedro.r@email.com", Password = BCrypt.Net.BCrypt.HashPassword("empleado8"), Telefono = "3419990000", Direccion = "Boulevard Principal 987", IdLocalidad = cordoba?.IdLocalidad, FechaIngreso = new DateOnly(2022, 7, 15), Estado = true }
        };
    }

    private static IEnumerable<Proveedor> GetProveedores(List<Localidad> localidades)
    {
        var locRosario = localidades.FirstOrDefault(l => l.Nombre.Contains("Rosario", StringComparison.OrdinalIgnoreCase));
        var locCordoba = localidades.FirstOrDefault(l => l.Nombre.Contains("Córdoba", StringComparison.OrdinalIgnoreCase));
        var locCaba = localidades.FirstOrDefault(l => l.Nombre.Contains("Comuna", StringComparison.OrdinalIgnoreCase) || l.Nombre.Contains("CABA", StringComparison.OrdinalIgnoreCase));

        if (locRosario == null || locCordoba == null || locCaba == null)
        {
            Console.WriteLine("⚠️ No se encontraron las localidades esperadas (Rosario, Córdoba, CABA). No se crearon proveedores.");
            return new List<Proveedor>();
        }

        return new List<Proveedor>
        {
            new Proveedor { RazonSocial = "Distrito Digital S.A.", Cuit = "30-12345678-9", Telefono = "3416667788", Email = "compras@distritodigital.com", Direccion = "Calle Falsa 123", IdLocalidad = locRosario.IdLocalidad, Activo = true },
            new Proveedor { RazonSocial = "Logística Computacional S.R.L.", Cuit = "30-98765432-1", Telefono = "116665544", Email = "ventas@logisticacompsrl.com", Direccion = "Avenida Siempre Viva 742", IdLocalidad = locCordoba.IdLocalidad, Activo = true },
            new Proveedor { RazonSocial = "TecnoImport Argentina", Cuit = "30-55555555-5", Telefono = "114445566", Email = "ventas@tecnoimport.com", Direccion = "Av. Corrientes 1234", IdLocalidad = locCaba.IdLocalidad, Activo = true },
            new Proveedor { RazonSocial = "ElectroRed S.A.", Cuit = "30-44556677-8", Telefono = "1133445566", Email = "contacto@electrored.com", Direccion = "San Martín 321", IdLocalidad = locRosario.IdLocalidad, Activo = true },
            new Proveedor { RazonSocial = "Softy Systems", Cuit = "30-11223344-7", Telefono = "1133557799", Email = "info@softysystems.com", Direccion = "Rivadavia 234", IdLocalidad = locCordoba.IdLocalidad, Activo = true },
            new Proveedor { RazonSocial = "GigaNet Solutions", Cuit = "30-99887766-2", Telefono = "3412233444", Email = "soporte@giganet.com", Direccion = "Alsina 432", IdLocalidad = locRosario.IdLocalidad, Activo = true },
            new Proveedor { RazonSocial = "Comercial Andina SRL", Cuit = "30-33445566-3", Telefono = "2614455667", Email = "ventas@comercialandina.com", Direccion = "San Juan 100", IdLocalidad = locCaba.IdLocalidad, Activo = true },
            new Proveedor { RazonSocial = "Delta Peripherals", Cuit = "30-77665544-0", Telefono = "3813322110", Email = "compras@deltaperipherals.com", Direccion = "Av. Belgrano 654", IdLocalidad = locRosario.IdLocalidad, Activo = true },
            new Proveedor { RazonSocial = "BioTecnica Group", Cuit = "30-12121212-1", Telefono = "3794556677", Email = "contacto@biotecnica.com", Direccion = "Mitre 789", IdLocalidad = locCordoba.IdLocalidad, Activo = true }
        };
    }

    private static IEnumerable<Producto> GetProductosConPreciosVenta(List<TipoProducto> tipos)
    {
        var random = new Random();
        return new List<Producto>
        {
            new Producto { Nombre = "MotherBoard Ryzen 5.0", Descripcion = "Mother Asus", Stock = 150, Activo = true, IdTipoProducto = tipos.First(t => t.Descripcion == "Componentes").IdTipoProducto, PreciosVenta = new List<PrecioVenta> { new PrecioVenta { FechaDesde = DateTime.Today, Monto = random.Next(1000, 15001) * 10 } } },
            new Producto { Nombre = "Monitor Curvo TLC", Descripcion = "Monitor Curvo 20°", Stock = 200, Activo = true, IdTipoProducto = tipos.First(t => t.Descripcion == "Monitores").IdTipoProducto, PreciosVenta = new List<PrecioVenta> { new PrecioVenta { FechaDesde = DateTime.Today, Monto = random.Next(1000, 15001) * 10 } } },
            new Producto { Nombre = "Parlante Huge HBL", Descripcion = "Sonido Envolvente", Stock = 100, Activo = true, IdTipoProducto = tipos.First(t => t.Descripcion == "Parlantes").IdTipoProducto, PreciosVenta = new List<PrecioVenta> { new PrecioVenta { FechaDesde = DateTime.Today, Monto = random.Next(1000, 15001) * 10 } } },
            new Producto { Nombre = "Teclado Mecánico RGB", Descripcion = "Teclado gaming mecánico", Stock = 80, Activo = true, IdTipoProducto = tipos.First(t => t.Descripcion == "Teclados").IdTipoProducto, PreciosVenta = new List<PrecioVenta> { new PrecioVenta { FechaDesde = DateTime.Today, Monto = random.Next(1000, 15001) * 10 } } },
            new Producto { Nombre = "Mouse Inalámbrico", Descripcion = "Mouse ergonómico inalámbrico", Stock = 120, Activo = true, IdTipoProducto = tipos.First(t => t.Descripcion == "Mouse").IdTipoProducto, PreciosVenta = new List<PrecioVenta> { new PrecioVenta { FechaDesde = DateTime.Today, Monto = random.Next(1000, 15001) * 10 } } },
            new Producto { Nombre = "Laptop Gamer Xtreme", Descripcion = "Laptop con GPU RTX 4060 y 32GB RAM", Stock = 50, Activo = true, IdTipoProducto = tipos.First(t => t.Descripcion == "Laptops").IdTipoProducto, PreciosVenta = new List<PrecioVenta> { new PrecioVenta { FechaDesde = DateTime.Today, Monto = random.Next(3000, 5001) * 10 } } },
            new Producto { Nombre = "Router Wi-Fi 6 Mesh", Descripcion = "Sistema de red inalámbrica de alto rendimiento", Stock = 90, Activo = true, IdTipoProducto = tipos.First(t => t.Descripcion == "Redes").IdTipoProducto, PreciosVenta = new List<PrecioVenta> { new PrecioVenta { FechaDesde = DateTime.Today, Monto = random.Next(200, 1001) * 10 } } },
            new Producto { Nombre = "Tablet Android 10\"", Descripcion = "Pantalla FHD y batería de larga duración", Stock = 75, Activo = true, IdTipoProducto = tipos.First(t => t.Descripcion == "Tabletas").IdTipoProducto, PreciosVenta = new List<PrecioVenta> { new PrecioVenta { FechaDesde = DateTime.Today, Monto = random.Next(1000, 2501) * 10 } } },
            new Producto { Nombre = "Impresora Láser HP", Descripcion = "Impresora monocromática rápida", Stock = 60, Activo = true, IdTipoProducto = tipos.First(t => t.Descripcion == "Impresoras").IdTipoProducto, PreciosVenta = new List<PrecioVenta> { new PrecioVenta { FechaDesde = DateTime.Today, Monto = random.Next(1500, 3001) * 10 } } },
            new Producto { Nombre = "Disco SSD 1TB", Descripcion = "Almacenamiento rápido NVMe", Stock = 200, Activo = true, IdTipoProducto = tipos.First(t => t.Descripcion == "Almacenamiento").IdTipoProducto, PreciosVenta = new List<PrecioVenta> { new PrecioVenta { FechaDesde = DateTime.Today, Monto = random.Next(500, 1201) * 10 } } },
            new Producto { Nombre = "Cámara Web Full HD", Descripcion = "Con micrófono incorporado y autofoco", Stock = 150, Activo = true, IdTipoProducto = tipos.First(t => t.Descripcion == "Cámaras").IdTipoProducto, PreciosVenta = new List<PrecioVenta> { new PrecioVenta { FechaDesde = DateTime.Today, Monto = random.Next(300, 701) * 10 } } },
            new Producto { Nombre = "Auriculares Pro Studio", Descripcion = "Audio profesional para edición y mezcla", Stock = 40, Activo = true, IdTipoProducto = tipos.First(t => t.Descripcion == "Audio Profesional").IdTipoProducto, PreciosVenta = new List<PrecioVenta> { new PrecioVenta { FechaDesde = DateTime.Today, Monto = random.Next(1000, 2501) * 10 } } },
            new Producto { Nombre = "Proyector HD LED", Descripcion = "Ideal para presentaciones y cine en casa", Stock = 30, Activo = true, IdTipoProducto = tipos.First(t => t.Descripcion == "Proyectores").IdTipoProducto, PreciosVenta = new List<PrecioVenta> { new PrecioVenta { FechaDesde = DateTime.Today, Monto = random.Next(2000, 4001) * 10 } } },
            new Producto { Nombre = "Scanner Documental Pro", Descripcion = "Scanner de alta velocidad para documentos", Stock = 25, Activo = true, IdTipoProducto = tipos.First(t => t.Descripcion == "Scanners").IdTipoProducto, PreciosVenta = new List<PrecioVenta> { new PrecioVenta { FechaDesde = DateTime.Today, Monto = random.Next(800, 2001) * 10 } } },
            new Producto { Nombre = "Desktop Workstation", Descripcion = "Computadora de escritorio para trabajo intensivo", Stock = 35, Activo = true, IdTipoProducto = tipos.First(t => t.Descripcion == "Desktop").IdTipoProducto, PreciosVenta = new List<PrecioVenta> { new PrecioVenta { FechaDesde = DateTime.Today, Monto = random.Next(2500, 6001) * 10 } } },
            new Producto { Nombre = "Servidor Rack 2U", Descripcion = "Servidor empresarial para centro de datos", Stock = 15, Activo = true, IdTipoProducto = tipos.First(t => t.Descripcion == "Servidores").IdTipoProducto, PreciosVenta = new List<PrecioVenta> { new PrecioVenta { FechaDesde = DateTime.Today, Monto = random.Next(5000, 12001) * 10 } } },
            new Producto { Nombre = "Software Suite Office", Descripcion = "Suite de oficina profesional", Stock = 500, Activo = true, IdTipoProducto = tipos.First(t => t.Descripcion == "Software").IdTipoProducto, PreciosVenta = new List<PrecioVenta> { new PrecioVenta { FechaDesde = DateTime.Today, Monto = random.Next(100, 501) * 10 } } },
            new Producto { Nombre = "Funda Laptop Universal", Descripcion = "Funda protectora para laptops", Stock = 300, Activo = true, IdTipoProducto = tipos.First(t => t.Descripcion == "Accesorios").IdTipoProducto, PreciosVenta = new List<PrecioVenta> { new PrecioVenta { FechaDesde = DateTime.Today, Monto = random.Next(50, 201) * 10 } } },
            new Producto { Nombre = "Kit Gaming RGB", Descripcion = "Kit completo para gaming con iluminación RGB", Stock = 45, Activo = true, IdTipoProducto = tipos.First(t => t.Descripcion == "Gaming").IdTipoProducto, PreciosVenta = new List<PrecioVenta> { new PrecioVenta { FechaDesde = DateTime.Today, Monto = random.Next(1500, 3501) * 10 } } },
            new Producto { Nombre = "Smartphone Android 5G", Descripcion = "Teléfono inteligente con conectividad 5G", Stock = 180, Activo = true, IdTipoProducto = tipos.First(t => t.Descripcion == "Smartphones").IdTipoProducto, PreciosVenta = new List<PrecioVenta> { new PrecioVenta { FechaDesde = DateTime.Today, Monto = random.Next(800, 2001) * 10 } } }
        };
    }

    private static decimal CalcularPrecioCompra(Producto producto, Proveedor proveedor, decimal precioVenta)
    {
        decimal multiplicadorCompra = 0.70m;
        if (producto.Nombre.Contains("Software", StringComparison.OrdinalIgnoreCase)) multiplicadorCompra = 0.85m;
        else if (producto.Nombre.Contains("Servidor", StringComparison.OrdinalIgnoreCase)) multiplicadorCompra = 0.75m;
        else if (producto.Nombre.Contains("Accesorio", StringComparison.OrdinalIgnoreCase) || producto.Nombre.Contains("Kit", StringComparison.OrdinalIgnoreCase)) multiplicadorCompra = 0.60m;
        else if (producto.Nombre.Contains("Laptop", StringComparison.OrdinalIgnoreCase)) multiplicadorCompra = 0.65m;

        var ajusteProveedor = proveedor.RazonSocial switch
        {
            var s when s.Contains("TecnoImport", StringComparison.OrdinalIgnoreCase) => 1.05m,
            var s when s.Contains("ElectroRed", StringComparison.OrdinalIgnoreCase) => 0.97m,
            var s when s.Contains("Softy", StringComparison.OrdinalIgnoreCase) => 0.90m,
            var s when s.Contains("GigaNet", StringComparison.OrdinalIgnoreCase) => 0.95m,
            var s when s.Contains("Comercial Andina", StringComparison.OrdinalIgnoreCase) => 1.08m,
            _ => 1.00m
        };

        return Math.Round(precioVenta * multiplicadorCompra * ajusteProveedor, 2);
    }

    private static async Task<string> GetWithRetryAsync(HttpClient client, string url, int maxRetries = 5)
    {
        for (int i = 0; i < maxRetries; i++)
        {
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Console.WriteLine($"API limit reached, waiting {3 * (i + 1)}s...");
                await Task.Delay(3000 * (i + 1));
            }
            else
            {
                response.EnsureSuccessStatusCode();
            }
        }
        throw new HttpRequestException("Max retries reached for: " + url);
    }

    private class ApiResponseProvincias { public List<ProvinciaAPI> Provincias { get; set; } }
    private class ProvinciaAPI { public string Id { get; set; } public string Nombre { get; set; } }
    private class ApiResponseMunicipios { public List<MunicipioAPI> Municipios { get; set; } }
    private class MunicipioAPI { public string Nombre { get; set; } public ProvinciaAPI Provincia { get; set; } }
}

