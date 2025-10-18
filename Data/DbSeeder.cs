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
    private static class EstadosVenta
    {
        public const string Finalizada = "Finalizada";
        public const string Pendiente = "Pendiente";
    }

    private static class EstadosPedido
    {
        public const string Recibido = "Recibido";
        public const string Pendiente = "Pendiente";
    }

    private static class NombresProveedores
    {
        public const string SoftySystems = "Softy Systems";
        public const string TecnoImport = "TecnoImport";
        public const string ElectroRed = "ElectroRed";
        public const string GigaNet = "GigaNet";
        public const string ComercialAndina = "Comercial Andina";
    }

    private static readonly Random _random = new Random();
    private static readonly HttpClient _httpClient = new HttpClient();

    public static async Task SeedAsync(BuyJugadorContext context)
    {
        // # Descomenta esta línea para forzar la recreación de la BD cada vez que inicias
       //await context.Database.EnsureDeletedAsync(); 
        await context.Database.EnsureCreatedAsync();

        if (await context.Productos.AnyAsync())
        {
            Console.WriteLine("✅ La base de datos ya contiene datos. No se ejecutará el seeder.");
            return;
        }

        Console.WriteLine("Iniciando el sembrado de la base de datos...");

        await SeedProvinciasYLocalidadesAsync(context);
        await SeedTiposProductoAsync(context);
        await SeedPersonasAsync(context);
        await SeedProveedoresAsync(context);
        await SeedProductosConPreciosVentaAsync(context);
        await SeedRelacionesYPreciosCompraAsync(context);

        var strategy = context.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                await SeedVentasAsync(context);
                await SeedPedidosAsync(context);

                await context.SaveChangesAsync();

                await transaction.CommitAsync();
                Console.WriteLine("✅ Ventas y Pedidos sembrados correctamente dentro de la transacción.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"Error durante el sembrado transaccional: {ex.Message}");
                throw;
            }
        });

        Console.WriteLine("✅ Database seeded successfully!");
    }

    #region Base Seeding Methods
    private static async Task SeedProvinciasYLocalidadesAsync(BuyJugadorContext context)
    {
        if (await context.Provincias.AnyAsync()) return;
        Console.WriteLine("Seeding Provincias y Localidades...");

        var responseProvincias = await GetWithRetryAsync("https://apis.datos.gob.ar/georef/api/provincias?campos=id,nombre");
        var apiResponseProvincias = JsonSerializer.Deserialize<ApiResponseProvincias>(responseProvincias, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (apiResponseProvincias?.Provincias == null)
        {
            Console.WriteLine("Error: No se pudieron obtener las provincias desde la API.");
            return;
        }

        var provincias = apiResponseProvincias.Provincias.Select(p => new Provincia { Nombre = p.Nombre }).ToList();
        var caba = provincias.FirstOrDefault(p => p.Nombre == "Ciudad Autónoma de Buenos Aires");
        if (caba != null) caba.Nombre = "CABA";

        await context.Provincias.AddRangeAsync(provincias);
        await context.SaveChangesAsync();
        Console.WriteLine("Provincias sembradas.");

        var provinciasDbMap = await context.Provincias.ToDictionaryAsync(p => p.Nombre, p => p.IdProvincia);

        var responseLocalidades = await GetWithRetryAsync("https://apis.datos.gob.ar/georef/api/municipios?campos=nombre,provincia&max=2000");
        var apiResponseLocalidades = JsonSerializer.Deserialize<ApiResponseMunicipios>(responseLocalidades, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (apiResponseLocalidades?.Municipios != null)
        {
            var todasLasLocalidades = apiResponseLocalidades.Municipios
                .Select(m => {
                    var nombreProvincia = m.Provincia.Nombre == "Ciudad Autónoma de Buenos Aires" ? "CABA" : m.Provincia.Nombre;
                    provinciasDbMap.TryGetValue(nombreProvincia, out int idProvincia);
                    return new Localidad
                    {
                        Nombre = m.Nombre,
                        IdProvincia = idProvincia > 0 ? idProvincia : (int?)null
                    };
                })
                .Where(l => l.IdProvincia.HasValue)
                .ToList();

            await context.Localidades.AddRangeAsync(todasLasLocalidades);
            await context.SaveChangesAsync();
            Console.WriteLine("Localidades sembradas.");
        }
    }

    private static async Task SeedTiposProductoAsync(BuyJugadorContext context)
    {
        if (await context.TiposProductos.AnyAsync()) return;
        Console.WriteLine("Seeding Tipos de Producto...");
        await context.TiposProductos.AddRangeAsync(GetTiposProducto());
        await context.SaveChangesAsync();
    }

    private static async Task SeedPersonasAsync(BuyJugadorContext context)
    {
        if (await context.Personas.IgnoreQueryFilters().AnyAsync()) return;
        Console.WriteLine("Seeding Personas...");
        var locs = await context.Localidades.ToListAsync();
        if (locs.Any())
        {
            await context.Personas.AddRangeAsync(GetPersonas(locs));
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedProveedoresAsync(BuyJugadorContext context)
    {
        if (await context.Proveedores.IgnoreQueryFilters().AnyAsync()) return;
        Console.WriteLine("Seeding Proveedores...");
        var localidades = await context.Localidades.ToListAsync();
        var proveedores = GetProveedores(localidades);
        if (proveedores.Any())
        {
            await context.Proveedores.AddRangeAsync(proveedores);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedProductosConPreciosVentaAsync(BuyJugadorContext context)
    {
        if (await context.Productos.IgnoreQueryFilters().AnyAsync()) return;
        Console.WriteLine("Seeding Productos y Precios de Venta...");
        var tipos = await context.TiposProductos.ToListAsync();
        if (tipos.Any())
        {
            await context.Productos.AddRangeAsync(GetProductosConPreciosVenta(tipos));
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedRelacionesYPreciosCompraAsync(BuyJugadorContext context)
    {
        if (await context.ProductoProveedores.AnyAsync()) return;
        Console.WriteLine("Seeding Relaciones Producto-Proveedor y Precios de Compra...");

        var proveedores = await context.Proveedores.ToListAsync();
        var productos = await context.Productos.Include(p => p.PreciosVenta).ToListAsync();
        if (!proveedores.Any() || !productos.Any()) return;

        var relaciones = new List<ProductoProveedor>();
        var preciosCompra = new List<PrecioCompra>();

        foreach (var proveedor in proveedores)
        {
            var productosAsignados = proveedor.RazonSocial.Contains(NombresProveedores.SoftySystems, StringComparison.OrdinalIgnoreCase)
                ? productos
                : productos.OrderBy(p => _random.Next()).Take(_random.Next(5, 10)).ToList();

            foreach (var producto in productosAsignados)
            {
                relaciones.Add(new ProductoProveedor
                {
                    IdProveedor = proveedor.IdProveedor,
                    IdProducto = producto.IdProducto
                });

                var precioVentaReciente = producto.PreciosVenta.OrderByDescending(p => p.FechaDesde).FirstOrDefault()?.Monto ?? 10000m;
                preciosCompra.Add(new PrecioCompra
                {
                    IdProveedor = proveedor.IdProveedor,
                    IdProducto = producto.IdProducto,
                    Monto = CalcularPrecioCompra(producto, proveedor, precioVentaReciente)
                });
            }
        }
        await context.ProductoProveedores.AddRangeAsync(relaciones);
        await context.PreciosCompra.AddRangeAsync(preciosCompra);
        await context.SaveChangesAsync();
        Console.WriteLine("Relaciones y Precios de Compra generados correctamente.");
    }
    #endregion

    #region Transactional Seeding Methods
    private static async Task SeedVentasAsync(BuyJugadorContext context)
    {
        if (await context.Ventas.AnyAsync()) return;
        Console.WriteLine("LOG: Iniciando SeedVentasAsync...");

        var personas = await context.Personas.Where(p => p.Estado).ToListAsync();
        var productosDict = await context.Productos.Include(p => p.PreciosVenta).ToDictionaryAsync(p => p.IdProducto);

        if (personas.Count < 2 || !productosDict.Any())
        {
            Console.WriteLine("LOG: No hay suficientes personas o productos para crear ventas.");
            return;
        }

        var ventas = new List<Venta>
        {
            new Venta { Fecha = DateTime.UtcNow.AddDays(-10), Estado = EstadosVenta.Finalizada, IdPersona = personas[0].IdPersona },
            new Venta { Fecha = DateTime.UtcNow.AddDays(-5), Estado = EstadosVenta.Pendiente, IdPersona = personas[1].IdPersona }
        };

        foreach (var venta in ventas)
        {
            Console.WriteLine($"LOG: Preparando Venta para Persona ID {venta.IdPersona} con Fecha {venta.Fecha:yyyy-MM-dd}");
            var productosParaVenta = productosDict.Values.OrderBy(x => _random.Next()).Take(_random.Next(2, 4)).ToList();
            int nroLineaCounter = 1;

            foreach (var producto in productosParaVenta)
            {
                var precioVigente = producto.PreciosVenta
                    .Where(pv => pv.FechaDesde.Date <= venta.Fecha.Date)
                    .OrderByDescending(pv => pv.FechaDesde)
                    .FirstOrDefault();

                if (precioVigente != null)
                {
                    int cantidad = _random.Next(1, 4);
                    if (producto.Stock >= cantidad)
                    {
                        var linea = new LineaVenta
                        {
                            NroLineaVenta = nroLineaCounter++,
                            IdProducto = producto.IdProducto,
                            Cantidad = cantidad,
                            PrecioUnitario = precioVigente.Monto
                        };
                        venta.LineaVenta.Add(linea);
                        producto.Stock -= cantidad;
                        Console.WriteLine($"   -> Añadiendo línea: Producto ID {linea.IdProducto}, Cantidad: {linea.Cantidad}, Precio: {linea.PrecioUnitario:C}");
                    }
                    else
                    {
                        Console.WriteLine($"   -> LOG: Stock insuficiente para Producto ID {producto.IdProducto}. Stock: {producto.Stock}, Cantidad solicitada: {cantidad}");
                    }
                }
                else
                {
                    Console.WriteLine($"   -> LOG: No se encontró precio vigente para Producto ID {producto.IdProducto} en la fecha {venta.Fecha:yyyy-MM-dd}");
                }
            }
        }
        await context.Ventas.AddRangeAsync(ventas);
    }

    private static async Task SeedPedidosAsync(BuyJugadorContext context)
    {
        if (await context.Pedidos.AnyAsync()) return;
        Console.WriteLine("LOG: Iniciando SeedPedidosAsync...");

        var proveedores = await context.Proveedores.Where(p => p.Activo).OrderBy(p => p.IdProveedor).ToListAsync();
        var productosConPrecio = await context.PreciosCompra.ToListAsync();
        if (!proveedores.Any() || !productosConPrecio.Any())
        {
            Console.WriteLine("LOG: No hay suficientes proveedores o precios de compra para crear pedidos.");
            return;
        }

        var productosDict = await context.Productos.ToDictionaryAsync(p => p.IdProducto);
        var pedidos = new List<Pedido>();

        for (int i = 0; i < Math.Min(proveedores.Count, 3); i++)
        {
            var nuevoPedido = new Pedido
            {
                Fecha = DateTime.UtcNow.AddDays(-i * 7),
                Estado = i % 2 == 0 ? EstadosPedido.Recibido : EstadosPedido.Pendiente,
                IdProveedor = proveedores[i].IdProveedor
            };
            pedidos.Add(nuevoPedido);

            Console.WriteLine($"LOG: Preparando Pedido para Proveedor ID {nuevoPedido.IdProveedor} con Fecha {nuevoPedido.Fecha:yyyy-MM-dd}");

            int nroLineaPedido = 1;
            var productosDelProveedor = productosConPrecio
                .Where(pc => pc.IdProveedor == nuevoPedido.IdProveedor)
                .OrderBy(x => _random.Next()).Take(5);

            foreach (var productoPrecio in productosDelProveedor)
            {
                int cantidad = nroLineaPedido % 2 == 0 ? 10 : 5;
                var linea = new LineaPedido
                {
                    NroLineaPedido = nroLineaPedido,
                    IdProducto = productoPrecio.IdProducto,
                    Cantidad = cantidad,
                    PrecioUnitario = Math.Round(productoPrecio.Monto * (1 + (nroLineaPedido % 4) * 0.03m), 2)
                };
                nuevoPedido.LineasPedido.Add(linea);

                Console.WriteLine($"   -> Añadiendo línea: Producto ID {linea.IdProducto}, Cantidad: {linea.Cantidad}, Precio: {linea.PrecioUnitario:C}");

                if (nuevoPedido.Estado == EstadosPedido.Recibido && productosDict.TryGetValue(productoPrecio.IdProducto, out var producto))
                {
                    producto.Stock += cantidad;
                }
                nroLineaPedido++;
            }
        }
        await context.Pedidos.AddRangeAsync(pedidos);
    }
    #endregion

    #region Data Generation Methods
    private static IEnumerable<TipoProducto> GetTiposProducto() => new List<TipoProducto>
    {
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
            return Enumerable.Empty<Proveedor>();
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
        var tiposDict = tipos.ToDictionary(t => t.Descripcion, t => t.IdTipoProducto);

        var productos = new List<Producto>
        {
            new Producto { Nombre = "MotherBoard Ryzen 5.0", Descripcion = "Mother Asus", Stock = 150, Activo = true, IdTipoProducto = tiposDict["Componentes"] },
            new Producto { Nombre = "Monitor Curvo TLC", Descripcion = "Monitor Curvo 20°", Stock = 200, Activo = true, IdTipoProducto = tiposDict["Monitores"] },
            new Producto { Nombre = "Parlante Huge HBL", Descripcion = "Sonido Envolvente", Stock = 100, Activo = true, IdTipoProducto = tiposDict["Parlantes"] },
            new Producto { Nombre = "Teclado Mecánico RGB", Descripcion = "Teclado gaming mecánico", Stock = 80, Activo = true, IdTipoProducto = tiposDict["Teclados"] },
            new Producto { Nombre = "Mouse Inalámbrico", Descripcion = "Mouse ergonómico inalámbrico", Stock = 120, Activo = true, IdTipoProducto = tiposDict["Mouse"] },
            new Producto { Nombre = "Laptop Gamer Xtreme", Descripcion = "Laptop con GPU RTX 4060 y 32GB RAM", Stock = 50, Activo = true, IdTipoProducto = tiposDict["Laptops"] },
            new Producto { Nombre = "Router Wi-Fi 6 Mesh", Descripcion = "Sistema de red inalámbrica de alto rendimiento", Stock = 90, Activo = true, IdTipoProducto = tiposDict["Redes"] },
            new Producto { Nombre = "Tablet Android 10\"", Descripcion = "Pantalla FHD y batería de larga duración", Stock = 75, Activo = true, IdTipoProducto = tiposDict["Tabletas"] },
            new Producto { Nombre = "Impresora Láser HP", Descripcion = "Impresora monocromática rápida", Stock = 60, Activo = true, IdTipoProducto = tiposDict["Impresoras"] },
            new Producto { Nombre = "Disco SSD 1TB", Descripcion = "Almacenamiento rápido NVMe", Stock = 200, Activo = true, IdTipoProducto = tiposDict["Almacenamiento"] },
            new Producto { Nombre = "Cámara Web Full HD", Descripcion = "Con micrófono incorporado y autofoco", Stock = 150, Activo = true, IdTipoProducto = tiposDict["Cámaras"] },
            new Producto { Nombre = "Auriculares Pro Studio", Descripcion = "Audio profesional para edición y mezcla", Stock = 40, Activo = true, IdTipoProducto = tiposDict["Audio Profesional"] },
            new Producto { Nombre = "Proyector HD LED", Descripcion = "Ideal para presentaciones y cine en casa", Stock = 30, Activo = true, IdTipoProducto = tiposDict["Proyectores"] },
            new Producto { Nombre = "Scanner Documental Pro", Descripcion = "Scanner de alta velocidad para documentos", Stock = 25, Activo = true, IdTipoProducto = tiposDict["Scanners"] },
            new Producto { Nombre = "Desktop Workstation", Descripcion = "Computadora de escritorio para trabajo intensivo", Stock = 35, Activo = true, IdTipoProducto = tiposDict["Desktop"] },
            new Producto { Nombre = "Servidor Rack 2U", Descripcion = "Servidor empresarial para centro de datos", Stock = 15, Activo = true, IdTipoProducto = tiposDict["Servidores"] },
            new Producto { Nombre = "Software Suite Office", Descripcion = "Suite de oficina profesional", Stock = 500, Activo = true, IdTipoProducto = tiposDict["Software"] },
            new Producto { Nombre = "Funda Laptop Universal", Descripcion = "Funda protectora para laptops", Stock = 300, Activo = true, IdTipoProducto = tiposDict["Accesorios"] },
            new Producto { Nombre = "Kit Gaming RGB", Descripcion = "Kit completo para gaming con iluminación RGB", Stock = 45, Activo = true, IdTipoProducto = tiposDict["Gaming"] },
            new Producto { Nombre = "Smartphone Android 5G", Descripcion = "Teléfono inteligente con conectividad 5G", Stock = 180, Activo = true, IdTipoProducto = tiposDict["Smartphones"] }
        };

        foreach (var producto in productos)
        {
            producto.PreciosVenta = new List<PrecioVenta>();
            decimal precioActual = _random.Next(500, 1500) * 100;

            for (int i = 11; i >= 0; i--)
            {
                producto.PreciosVenta.Add(new PrecioVenta
                {
                    FechaDesde = DateTime.UtcNow.AddMonths(-i).Date,
                    Monto = Math.Round(precioActual, 2)
                });

                bool sube = _random.Next(0, 10) > 2;
                if (sube)
                {
                    decimal factor = 1 + (decimal)_random.Next(10, 101) / 1000m; 
                    precioActual *= factor;
                }
                else
                {
                    decimal factor = 1 - (decimal)_random.Next(10, 51) / 1000m; 
                    precioActual *= factor;
                }
            }
        }

        return productos;
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
            var s when s.Contains(NombresProveedores.TecnoImport, StringComparison.OrdinalIgnoreCase) => 1.05m,
            var s when s.Contains(NombresProveedores.ElectroRed, StringComparison.OrdinalIgnoreCase) => 0.97m,
            var s when s.Contains(NombresProveedores.SoftySystems, StringComparison.OrdinalIgnoreCase) => 0.90m,
            var s when s.Contains(NombresProveedores.GigaNet, StringComparison.OrdinalIgnoreCase) => 0.95m,
            var s when s.Contains(NombresProveedores.ComercialAndina, StringComparison.OrdinalIgnoreCase) => 1.08m,
            _ => 1.00m
        };

        return Math.Round(precioVenta * multiplicadorCompra * ajusteProveedor, 2);
    }
    #endregion

    #region Helpers
    private static async Task<string> GetWithRetryAsync(string url, int maxRetries = 5)
    {
        for (int i = 0; i < maxRetries; i++)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    var delay = 3000 * (i + 1);
                    Console.WriteLine($"API limit reached, waiting {delay / 1000}s...");
                    await Task.Delay(delay);
                }
                else
                {
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching {url}: {ex.Message}. Retrying...");
                await Task.Delay(1000);
            }
        }
        throw new HttpRequestException($"Max retries reached for: {url}");
    }

    private class ApiResponseProvincias { public List<ProvinciaAPI> Provincias { get; set; } }
    private class ProvinciaAPI { public string Id { get; set; } public string Nombre { get; set; } }
    private class ApiResponseMunicipios { public List<MunicipioAPI> Municipios { get; set; } }
    private class MunicipioAPI { public string Nombre { get; set; } public ProvinciaAPI Provincia { get; set; } }
    #endregion
}

