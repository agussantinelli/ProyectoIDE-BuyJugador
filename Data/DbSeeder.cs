using Data;
using DominioModelo;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

public static class DbSeeder
{
    public static async Task SeedAsync(BuyJugadorContext context)
    {
        context.Database.EnsureCreated();

        if (!context.Provincias.Any())
        {
            await SeedProvinciasYLocalidadesAsync(context);
        }

        if (!context.TiposProductos.Any())
        {
            context.TiposProductos.AddRange(
                new TipoProducto { Descripcion = "Componentes" },
                new TipoProducto { Descripcion = "Monitores" },
                new TipoProducto { Descripcion = "Parlantes" },
                new TipoProducto { Descripcion = "Teclados" },
                new TipoProducto { Descripcion = "Mouse" },
                new TipoProducto { Descripcion = "Impresoras" },
                new TipoProducto { Descripcion = "Scanners" },
                new TipoProducto { Descripcion = "Tabletas" },
                new TipoProducto { Descripcion = "Laptops" },
                new TipoProducto { Descripcion = "Desktop" },
                new TipoProducto { Descripcion = "Servidores" },
                new TipoProducto { Descripcion = "Redes" },
                new TipoProducto { Descripcion = "Almacenamiento" },
                new TipoProducto { Descripcion = "Software" },
                new TipoProducto { Descripcion = "Accesorios" },
                new TipoProducto { Descripcion = "Cámaras" },
                new TipoProducto { Descripcion = "Proyectores" },
                new TipoProducto { Descripcion = "Audio Profesional" },
                new TipoProducto { Descripcion = "Gaming" },
                new TipoProducto { Descripcion = "Smartphones" }
            );
            await context.SaveChangesAsync();
        }

        if (!context.Proveedores.Any())
        {
            var localidades = context.Localidades.ToList();
            if (localidades.Any(l => l.Nombre == "Rosario") &&
                localidades.Any(l => l.Nombre == "Córdoba") &&
                localidades.Any(l => l.Nombre.Contains("Comuna 1")))
            {
                context.Proveedores.AddRange(
                    new Proveedor { RazonSocial = "Distrito Digital S.A.", Cuit = "30-12345678-9", Telefono = "3416667788", Email = "compras@distritodigital.com", Direccion = "Calle Falsa 123", IdLocalidad = localidades.First(l => l.Nombre == "Rosario").IdLocalidad },
                    new Proveedor { RazonSocial = "Logística Computacional S.R.L.", Cuit = "30-98765432-1", Telefono = "116665544", Email = "ventas@logisticacompsrl.com", Direccion = "Avenida Siempre Viva 742", IdLocalidad = localidades.First(l => l.Nombre == "Córdoba").IdLocalidad },
                    new Proveedor { RazonSocial = "TecnoImport Argentina", Cuit = "30-55555555-5", Telefono = "114445566", Email = "ventas@tecnoimport.com", Direccion = "Av. Corrientes 1234", IdLocalidad = localidades.First(l => l.Nombre.Contains("Comuna 1")).IdLocalidad }
                );
                await context.SaveChangesAsync();
            }
        }

        if (!context.Productos.Any())
        {
            var tipos = context.TiposProductos.ToList();
            var random = new Random();

            var productosConPrecios = new List<Producto>
            {
                new Producto
                {
                    Nombre = "MotherBoard Ryzen 5.0", Descripcion = "Mother Asus", Stock = 150, IdTipoProducto = tipos.First(t => t.Descripcion == "Componentes").IdTipoProducto,
                    Activo = true,
                    Precios = new List<Precio> { new Precio { FechaDesde = DateTime.Today, Monto = random.Next(1000, 15001) * 10 } }
                },
                new Producto
                {
                    Nombre = "Monitor Curvo TLC", Descripcion = "Monitor Curvo 20°", Stock = 200, IdTipoProducto = tipos.First(t => t.Descripcion == "Monitores").IdTipoProducto,
                    Activo = true,
                    Precios = new List<Precio> { new Precio { FechaDesde = DateTime.Today, Monto = random.Next(1000, 15001) * 10 } }
                },
                new Producto
                {
                    Nombre = "Parlante Huge HBL", Descripcion = "Sonido Envolvente", Stock = 100, IdTipoProducto = tipos.First(t => t.Descripcion == "Parlantes").IdTipoProducto,
                    Activo = true,
                    Precios = new List<Precio> { new Precio { FechaDesde = DateTime.Today, Monto = random.Next(1000, 15001) * 10 } }
                },
                new Producto
                {
                    Nombre = "Teclado Mecánico RGB", Descripcion = "Teclado gaming mecánico", Stock = 80, IdTipoProducto = tipos.First(t => t.Descripcion == "Teclados").IdTipoProducto,
                    Activo = true,
                    Precios = new List<Precio> { new Precio { FechaDesde = DateTime.Today, Monto = random.Next(1000, 15001) * 10 } }
                },
                new Producto
                {
                    Nombre = "Mouse Inalámbrico", Descripcion = "Mouse ergonómico inalámbrico", Stock = 120, IdTipoProducto = tipos.First(t => t.Descripcion == "Mouse").IdTipoProducto,
                    Activo = true,
                    Precios = new List<Precio> { new Precio { FechaDesde = DateTime.Today, Monto = random.Next(1000, 15001) * 10 } }
                }
            };
            context.Productos.AddRange(productosConPrecios);
            await context.SaveChangesAsync();
        }

        if (!context.Personas.IgnoreQueryFilters().Any())
        {
            var locs = context.Localidades.ToList();
            if (locs.Any())
            {
                context.Personas.AddRange(
                    new Persona { NombreCompleto = "Martin Ratti", Dni = 12345678, Email = "marto@buyjugador.com", Password = BCrypt.Net.BCrypt.HashPassword("admin"), Telefono = "34115559101", Direccion = "Falsa 123", IdLocalidad = locs.First(l => l.Nombre == "Rosario").IdLocalidad, Estado = true, FechaIngreso = null },
                    new Persona { NombreCompleto = "Frank Fabra", Dni = 41111111, Email = "fabra@email.com", Password = BCrypt.Net.BCrypt.HashPassword("boca123"), Telefono = "3411111111", Direccion = "Verdadera 456", IdLocalidad = locs.First(l => l.Nombre == "Santa Fe").IdLocalidad, Estado = true, FechaIngreso = null },
                    new Persona { NombreCompleto = "Joaquin Peralta", Dni = 44444444, Email = "joaquin@buyjugador.com", Password = BCrypt.Net.BCrypt.HashPassword("empleado1"), Telefono = "115550202", Direccion = "Avenida Imaginaria 2", IdLocalidad = locs.First(l => l.Nombre == "La Plata").IdLocalidad, FechaIngreso = new DateOnly(2022, 5, 10), Estado = true },
                    new Persona { NombreCompleto = "Ayrton Costa", Dni = 42333444, Email = "ayrton@email.com", Password = BCrypt.Net.BCrypt.HashPassword("empleado2"), Telefono = "3415552222", Direccion = "Calle Demo 4", IdLocalidad = locs.First(l => l.Nombre == "Córdoba").IdLocalidad, FechaIngreso = new DateOnly(2023, 2, 15), Estado = true },
                    new Persona { NombreCompleto = "Luka Doncic", Dni = 42553400, Email = "luka@email.com", Password = BCrypt.Net.BCrypt.HashPassword("empleado3"), Telefono = "3415882922", Direccion = "Calle Prueba 5", IdLocalidad = locs.First(l => l.Nombre == "Mendoza").IdLocalidad, FechaIngreso = new DateOnly(2022, 8, 30), Estado = true },
                    new Persona { NombreCompleto = "Stephen Curry", Dni = 32393404, Email = "curry@email.com", Password = BCrypt.Net.BCrypt.HashPassword("empleado4"), Telefono = "3415559202", Direccion = "Calle Test 6", IdLocalidad = locs.First(l => l.Nombre == "San Carlos de Bariloche").IdLocalidad, FechaIngreso = new DateOnly(2021, 11, 5), Estado = true }
                );
                await context.SaveChangesAsync();
            }
        }

        if (!context.Ventas.Any())
        {
            var personasActivas = context.Personas.ToList();
            if (personasActivas.Count > 2)
            {
                context.Ventas.AddRange(
                    new Venta { Fecha = DateTime.UtcNow.AddDays(-10), Estado = "Pagada", IdPersona = personasActivas[2].IdPersona },
                    new Venta { Fecha = DateTime.UtcNow.AddDays(-5), Estado = "Pagada", IdPersona = personasActivas[3].IdPersona },
                    new Venta { Fecha = DateTime.UtcNow.AddDays(-2), Estado = "Pendiente", IdPersona = personasActivas[4].IdPersona },
                    new Venta { Fecha = DateTime.UtcNow.AddDays(-1), Estado = "Entregada", IdPersona = personasActivas[5].IdPersona }
                );
                await context.SaveChangesAsync();

                var ventasInsertadas = context.Ventas.ToList();
                var productosDisponibles = context.Productos.ToList();
                var lineasParaVentas = new List<LineaVenta>();
                var random = new Random();

                foreach (var venta in ventasInsertadas)
                {
                    var cantidadLineas = random.Next(2, 4);
                    var productosElegidos = productosDisponibles.OrderBy(p => random.Next()).Take(cantidadLineas).ToList();

                    for (int i = 0; i < productosElegidos.Count; i++)
                    {
                        lineasParaVentas.Add(new LineaVenta
                        {
                            IdVenta = venta.IdVenta,
                            NroLineaVenta = i + 1,
                            IdProducto = productosElegidos[i].IdProducto,
                            Cantidad = random.Next(1, 6)
                        });
                    }
                }

                context.LineaVentas.AddRange(lineasParaVentas);
                await context.SaveChangesAsync();
            }
        }

        if (!context.Pedidos.Any())
        {
            var proveedores = context.Proveedores.ToList();
            var productos = context.Productos.ToList();
            var random = new Random();

            if (proveedores.Count >= 2 && productos.Any())
            {
                var pedidos = new List<Pedido>
                {
                    new Pedido { Fecha = DateTime.UtcNow.AddDays(-7), Estado = "Pendiente", IdProveedor = proveedores[0].IdProveedor },
                    new Pedido { Fecha = DateTime.UtcNow.AddDays(-3), Estado = "Recibido", IdProveedor = proveedores[1].IdProveedor },
                    new Pedido { Fecha = DateTime.UtcNow.AddDays(-1), Estado = "En tránsito", IdProveedor = proveedores[0].IdProveedor }
                };

                context.Pedidos.AddRange(pedidos);
                await context.SaveChangesAsync();

                var pedidosGuardados = context.Pedidos.ToList();
                var lineas = new List<LineaPedido>();

                foreach (var pedido in pedidosGuardados)
                {
                    var cantidadLineas = random.Next(2, 4);
                    var productosElegidos = productos.OrderBy(p => random.Next()).Take(cantidadLineas).ToList();

                    for (int i = 0; i < productosElegidos.Count; i++)
                    {
                        lineas.Add(new LineaPedido
                        {
                            IdPedido = pedido.IdPedido,
                            NroLineaPedido = i + 1,
                            IdProducto = productosElegidos[i].IdProducto,
                            Cantidad = random.Next(5, 21)
                        });
                    }
                }

                context.LineaPedidos.AddRange(lineas);
                await context.SaveChangesAsync();
            }
        }

        Console.WriteLine("Database seeded successfully!");
    }

    private static async Task SeedProvinciasYLocalidadesAsync(BuyJugadorContext context)
    {
        using var httpClient = new HttpClient();

        var responseProvincias = await httpClient.GetStringAsync("https://apis.datos.gob.ar/georef/api/provincias?campos=nombre");
        var apiResponseProvincias = JsonSerializer.Deserialize<ApiResponseProvincias>(responseProvincias, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        var provincias = apiResponseProvincias.Provincias.Select(p => new Provincia { Nombre = p.Nombre }).ToList();

        var caba = provincias.FirstOrDefault(p => p.Nombre == "Ciudad Autónoma de Buenos Aires");
        if (caba != null) caba.Nombre = "CABA";

        context.Provincias.AddRange(provincias);
        await context.SaveChangesAsync();
        Console.WriteLine("Provincias seeded.");

        var provinciasDB = await context.Provincias.ToListAsync();
        var todasLasLocalidades = new List<Localidad>();

        foreach (var provincia in provinciasDB)
        {
            var nombreProvinciaParaApi = provincia.Nombre == "CABA" ? "Ciudad Autónoma de Buenos Aires" : provincia.Nombre;
            var responseLocalidades = await httpClient.GetStringAsync($"https://apis.datos.gob.ar/georef/api/municipios?provincia={Uri.EscapeDataString(nombreProvinciaParaApi)}&campos=nombre&max=500");
            var apiResponseLocalidades = JsonSerializer.Deserialize<ApiResponseMunicipios>(responseLocalidades, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (apiResponseLocalidades?.Municipios != null)
            {
                var localidadesDeProvincia = apiResponseLocalidades.Municipios.Select(m => new Localidad
                {
                    Nombre = m.Nombre,
                    IdProvincia = provincia.IdProvincia
                });
                todasLasLocalidades.AddRange(localidadesDeProvincia);
            }
        }

        context.Localidades.AddRange(todasLasLocalidades);
        await context.SaveChangesAsync();
        Console.WriteLine("Localidades seeded.");
    }

    private class ApiResponseProvincias { public List<ProvinciaAPI> Provincias { get; set; } }
    private class ProvinciaAPI { public string Nombre { get; set; } }
    private class ApiResponseMunicipios { public List<MunicipioAPI> Municipios { get; set; } }
    private class MunicipioAPI { public string Nombre { get; set; } }
}

