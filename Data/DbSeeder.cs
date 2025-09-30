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
    // El método ahora es asíncrono para poder hacer llamadas a la API
    public static async Task SeedAsync(BuyJugadorContext context)
    {
        context.Database.EnsureCreated();

        // --- SEED DE PROVINCIAS Y LOCALIDADES DESDE API ---
        if (!context.Provincias.Any())
        {
            await SeedProvinciasYLocalidadesAsync(context);
        }

        // --- SEED DE DATOS DE NEGOCIO (Tipos de Producto, Proveedores, etc.) ---

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
            context.SaveChanges();
        }

        if (!context.Proveedores.Any())
        {
            var localidades = context.Localidades.ToList();
            if (localidades.Any(l => l.Nombre == "Rosario") &&
               localidades.Any(l => l.Nombre == "Córdoba") &&
               localidades.Any(l => l.Nombre.Contains("Comuna 1"))) // CABA se divide en comunas
            {
                context.Proveedores.AddRange(
                    new Proveedor { RazonSocial = "Distrito Digital S.A.", Cuit = "30-12345678-9", Telefono = "3416667788", Email = "compras@distritodigital.com", Direccion = "Calle Falsa 123", IdLocalidad = localidades.First(l => l.Nombre == "Rosario").IdLocalidad },
                    new Proveedor { RazonSocial = "Logística Computacional S.R.L.", Cuit = "30-98765432-1", Telefono = "116665544", Email = "ventas@logisticacompsrl.com", Direccion = "Avenida Siempre Viva 742", IdLocalidad = localidades.First(l => l.Nombre == "Córdoba").IdLocalidad },
                    new Proveedor { RazonSocial = "TecnoImport Argentina", Cuit = "30-55555555-5", Telefono = "114445566", Email = "ventas@tecnoimport.com", Direccion = "Av. Corrientes 1234", IdLocalidad = localidades.First(l => l.Nombre.Contains("Comuna 1")).IdLocalidad }
                );
                context.SaveChanges();
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
            context.SaveChanges();
        }

        if (!context.Personas.IgnoreQueryFilters().Any())
        {
            var locs = context.Localidades.ToList();
            if (locs.Any())
            {
                context.Personas.AddRange(
                    new Persona { NombreCompleto = "Martin Ratti", Dni = 12345678, Email = "marto@buyjugador.com", Password = BCrypt.Net.BCrypt.HashPassword("admin"), Telefono = "34115559101", Direccion = "Falsa 123", IdLocalidad = locs.First(l => l.Nombre == "Rosario").IdLocalidad, Estado = true },
                    new Persona { NombreCompleto = "Frank Fabra", Dni = 41111111, Email = "fabra@email.com", Password = BCrypt.Net.BCrypt.HashPassword("boca123"), Telefono = "3411111111", Direccion = "Verdadera 456", IdLocalidad = locs.First(l => l.Nombre == "Santa Fe").IdLocalidad, Estado = true },
                    new Persona { NombreCompleto = "Joaquin Peralta", Dni = 44444444, Email = "joaquin@buyjugador.com", Password = BCrypt.Net.BCrypt.HashPassword("empleado1"), Telefono = "115550202", Direccion = "Avenida Imaginaria 2", IdLocalidad = locs.First(l => l.Nombre == "La Plata").IdLocalidad, FechaIngreso = new DateOnly(2022, 5, 10), Estado = true },
                    new Persona { NombreCompleto = "Ayrton Costa", Dni = 42333444, Email = "ayrton@email.com", Password = BCrypt.Net.BCrypt.HashPassword("empleado2"), Telefono = "3415552222", Direccion = "Calle Demo 4", IdLocalidad = locs.First(l => l.Nombre == "Córdoba").IdLocalidad, FechaIngreso = new DateOnly(2023, 2, 15), Estado = true },
                    new Persona { NombreCompleto = "Luka Doncic", Dni = 42553400, Email = "luka@email.com", Password = BCrypt.Net.BCrypt.HashPassword("empleado3"), Telefono = "3415882922", Direccion = "Calle Prueba 5", IdLocalidad = locs.First(l => l.Nombre == "Mendoza").IdLocalidad, FechaIngreso = new DateOnly(2022, 8, 30), Estado = true },
                    new Persona { NombreCompleto = "Stephen Curry", Dni = 32393404, Email = "curry@email.com", Password = BCrypt.Net.BCrypt.HashPassword("empleado4"), Telefono = "3415559202", Direccion = "Calle Test 6", IdLocalidad = locs.First(l => l.Nombre == "San Carlos de Bariloche").IdLocalidad, FechaIngreso = new DateOnly(2021, 11, 5), Estado = true },
                    new Persona { NombreCompleto = "Agustin Santinelli", Dni = 46294992, Email = "agustinsantinelli@buyjugador.com", Password = BCrypt.Net.BCrypt.HashPassword("agustin"), Telefono = "3416667777", Direccion = "Molina 2022", IdLocalidad = locs.First(l => l.Nombre == "Rosario").IdLocalidad, FechaIngreso = new DateOnly(2025, 9, 23), Estado = true },
                    new Persona { NombreCompleto = "Carlos Lopez", Dni = 28765432, Email = "carlos.l@email.com", Password = BCrypt.Net.BCrypt.HashPassword("empleado6"), Telefono = "3417778888", Direccion = "Calle Secundaria 321", IdLocalidad = locs.First(l => l.Nombre == "Santa Fe").IdLocalidad, FechaIngreso = new DateOnly(2022, 12, 10), Estado = true },
                    new Persona { NombreCompleto = "Ana Martinez", Dni = 39876543, Email = "ana.m@email.com", Password = BCrypt.Net.BCrypt.HashPassword("empleado7"), Telefono = "3418889999", Direccion = "Pasaje Privado 654", IdLocalidad = locs.First(l => l.Nombre == "Córdoba").IdLocalidad, FechaIngreso = new DateOnly(2023, 3, 25), Estado = true },
                    new Persona { NombreCompleto = "Pedro Rodriguez", Dni = 40987654, Email = "pedro.r@email.com", Password = BCrypt.Net.BCrypt.HashPassword("empleado8"), Telefono = "3419990000", Direccion = "Boulevard Principal 987", IdLocalidad = locs.First(l => l.Nombre == "Mendoza").IdLocalidad, FechaIngreso = new DateOnly(2022, 7, 15), Estado = true }
                );
                context.SaveChanges();
            }
        }

        if (!context.Ventas.Any())
        {
            var personasActivas = context.Personas.ToList();
            if (personasActivas.Count > 5)
            {
                context.Ventas.AddRange(
                    new Venta { Fecha = DateTime.UtcNow.AddDays(-10), Estado = "Pagada", IdPersona = personasActivas[2].IdPersona },
                    new Venta { Fecha = DateTime.UtcNow.AddDays(-5), Estado = "Pagada", IdPersona = personasActivas[3].IdPersona },
                    new Venta { Fecha = DateTime.UtcNow.AddDays(-2), Estado = "Pendiente", IdPersona = personasActivas[4].IdPersona },
                    new Venta { Fecha = DateTime.UtcNow.AddDays(-1), Estado = "Entregada", IdPersona = personasActivas[5].IdPersona }
                );
                context.SaveChanges();
            }
        }

        Console.WriteLine("Database seeded successfully!");
    }

    private static async Task SeedProvinciasYLocalidadesAsync(BuyJugadorContext context)
    {
        using var httpClient = new HttpClient();

        // 1. Obtener y guardar Provincias
        var responseProvincias = await httpClient.GetStringAsync("https://apis.datos.gob.ar/georef/api/provincias?campos=nombre");
        var apiResponseProvincias = JsonSerializer.Deserialize<ApiResponseProvincias>(responseProvincias, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        var provincias = apiResponseProvincias.Provincias.Select(p => new Provincia { Nombre = p.Nombre }).ToList();

        // Ajustamos CABA para que coincida con el resto del seeder
        var caba = provincias.FirstOrDefault(p => p.Nombre == "Ciudad Autónoma de Buenos Aires");
        if (caba != null) caba.Nombre = "CABA";

        context.Provincias.AddRange(provincias);
        await context.SaveChangesAsync();
        Console.WriteLine("Provincias seeded.");

        // 2. Obtener y guardar Localidades (Municipios en la API)
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

    // Clases auxiliares para deserializar la respuesta de la API
    private class ApiResponseProvincias { public List<ProvinciaAPI> Provincias { get; set; } }
    private class ProvinciaAPI { public string Nombre { get; set; } }
    private class ApiResponseMunicipios { public List<MunicipioAPI> Municipios { get; set; } }
    private class MunicipioAPI { public string Nombre { get; set; } }
}

