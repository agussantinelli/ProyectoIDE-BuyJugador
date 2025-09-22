using Data;
using Microsoft.EntityFrameworkCore;
using DominioModelo;
using System;
using System.Linq;
using BCrypt.Net;

public static class DbSeeder
{
    public static void Seed(BuyJugadorContext context)
    {
        context.Database.Migrate();

        // 1. Provincias
        if (!context.Provincias.Any())
        {
            context.Provincias.AddRange(
                new Provincia { IdProvincia = 1, Nombre = "Ciudad Autónoma de Buenos Aires" },
                new Provincia { IdProvincia = 2, Nombre = "Buenos Aires" },
                new Provincia { IdProvincia = 3, Nombre = "Catamarca" },
                new Provincia { IdProvincia = 4, Nombre = "Chaco" },
                new Provincia { IdProvincia = 5, Nombre = "Chubut" },
                new Provincia { IdProvincia = 6, Nombre = "Córdoba" },
                new Provincia { IdProvincia = 7, Nombre = "Corrientes" },
                new Provincia { IdProvincia = 8, Nombre = "Entre Ríos" },
                new Provincia { IdProvincia = 9, Nombre = "Formosa" },
                new Provincia { IdProvincia = 10, Nombre = "Jujuy" },
                new Provincia { IdProvincia = 11, Nombre = "La Pampa" },
                new Provincia { IdProvincia = 12, Nombre = "La Rioja" },
                new Provincia { IdProvincia = 13, Nombre = "Mendoza" },
                new Provincia { IdProvincia = 14, Nombre = "Misiones" },
                new Provincia { IdProvincia = 15, Nombre = "Neuquén" },
                new Provincia { IdProvincia = 16, Nombre = "Río Negro" },
                new Provincia { IdProvincia = 17, Nombre = "Salta" },
                new Provincia { IdProvincia = 18, Nombre = "San Juan" },
                new Provincia { IdProvincia = 19, Nombre = "San Luis" },
                new Provincia { IdProvincia = 20, Nombre = "Santa Fe" },
                new Provincia { IdProvincia = 21, Nombre = "Santa Cruz" },
                new Provincia { IdProvincia = 22, Nombre = "Santiago del Estero" },
                new Provincia { IdProvincia = 23, Nombre = "Tierra del Fuego, Antártida e Islas del Atlántico Sur" },
                new Provincia { IdProvincia = 24, Nombre = "Tucumán" }
            );
            context.SaveChanges();
        }

        // 2. Localidades extendidas
        if (!context.Localidades.Any())
        {
            var provincias = context.Provincias.ToList();
            context.Localidades.AddRange(
                // Santa Fe
                new Localidad { IdLocalidad = 1, Nombre = "Rosario", IdProvincia = provincias.First(p => p.Nombre == "Santa Fe").IdProvincia },
                new Localidad { IdLocalidad = 2, Nombre = "Santa Fe", IdProvincia = provincias.First(p => p.Nombre == "Santa Fe").IdProvincia },
                new Localidad { IdLocalidad = 3, Nombre = "Venado Tuerto", IdProvincia = provincias.First(p => p.Nombre == "Santa Fe").IdProvincia },

                // Buenos Aires
                new Localidad { IdLocalidad = 4, Nombre = "La Plata", IdProvincia = provincias.First(p => p.Nombre == "Buenos Aires").IdProvincia },
                new Localidad { IdLocalidad = 5, Nombre = "Mar del Plata", IdProvincia = provincias.First(p => p.Nombre == "Buenos Aires").IdProvincia },
                new Localidad { IdLocalidad = 6, Nombre = "Bahía Blanca", IdProvincia = provincias.First(p => p.Nombre == "Buenos Aires").IdProvincia },

                // Córdoba
                new Localidad { IdLocalidad = 7, Nombre = "Córdoba Capital", IdProvincia = provincias.First(p => p.Nombre == "Córdoba").IdProvincia },
                new Localidad { IdLocalidad = 8, Nombre = "Villa María", IdProvincia = provincias.First(p => p.Nombre == "Córdoba").IdProvincia },
                new Localidad { IdLocalidad = 9, Nombre = "Río Cuarto", IdProvincia = provincias.First(p => p.Nombre == "Córdoba").IdProvincia },

                // Mendoza
                new Localidad { IdLocalidad = 10, Nombre = "Mendoza", IdProvincia = provincias.First(p => p.Nombre == "Mendoza").IdProvincia },
                new Localidad { IdLocalidad = 11, Nombre = "San Rafael", IdProvincia = provincias.First(p => p.Nombre == "Mendoza").IdProvincia },

                // Salta
                new Localidad { IdLocalidad = 12, Nombre = "Salta", IdProvincia = provincias.First(p => p.Nombre == "Salta").IdProvincia },

                // Tucumán
                new Localidad { IdLocalidad = 13, Nombre = "San Miguel de Tucumán", IdProvincia = provincias.First(p => p.Nombre == "Tucumán").IdProvincia },

                // Chaco
                new Localidad { IdLocalidad = 14, Nombre = "Resistencia", IdProvincia = provincias.First(p => p.Nombre == "Chaco").IdProvincia },

                // Río Negro
                new Localidad { IdLocalidad = 15, Nombre = "Bariloche", IdProvincia = provincias.First(p => p.Nombre == "Río Negro").IdProvincia }
            );
            context.SaveChanges();
        }

        // 3. TiposProductos
        if (!context.TiposProductos.Any())
        {
            context.TiposProductos.AddRange(
                new TipoProducto { IdTipoProducto = 1, Descripcion = "Componentes" },
                new TipoProducto { IdTipoProducto = 2, Descripcion = "Monitores" },
                new TipoProducto { IdTipoProducto = 3, Descripcion = "Parlantes" }
            );
            context.SaveChanges();
        }

        // 4. Proveedores
        if (!context.Proveedores.Any())
        {
            var localidades = context.Localidades.ToList();
            context.Proveedores.AddRange(
                new Proveedor
                {
                    IdProveedor = 1,
                    RazonSocial = "Distrito Digital S.A.",
                    Cuit = "30-12345678-9",
                    Telefono = "3416667788",
                    Email = "compras@distritodigital.com",
                    Direccion = "Calle Falsa 123",
                    IdLocalidad = localidades.First(l => l.Nombre == "Rosario").IdLocalidad
                },
                new Proveedor
                {
                    IdProveedor = 2,
                    RazonSocial = "Logística Computacional S.R.L.",
                    Cuit = "30-98765432-1",
                    Telefono = "116665544",
                    Email = "ventas@logisticacompsrl.com",
                    Direccion = "Avenida Siempre Viva 742",
                    IdLocalidad = localidades.First(l => l.Nombre == "Córdoba Capital").IdLocalidad
                }
            );
            context.SaveChanges();
        }

        // 5. Productos
        if (!context.Productos.Any())
        {
            var tipos = context.TiposProductos.ToList();
            context.Productos.AddRange(
                new Producto { IdProducto = 1, Nombre = "MotherBoard Ryzen 5.0", Descripcion = "Mother Asus", Stock = 150, IdTipoProducto = tipos.First(t => t.Descripcion == "Componentes").IdTipoProducto },
                new Producto { IdProducto = 2, Nombre = "Monitor Curvo TLC", Descripcion = "Monitor Curvo 20°", Stock = 200, IdTipoProducto = tipos.First(t => t.Descripcion == "Monitores").IdTipoProducto },
                new Producto { IdProducto = 3, Nombre = "Parlante Huge HBL", Descripcion = "Sonido Envolvente", Stock = 100, IdTipoProducto = tipos.First(t => t.Descripcion == "Parlantes").IdTipoProducto }
            );
            context.SaveChanges();
        }

        // 6. Precios
        if (!context.Precios.Any())
        {
            var productos = context.Productos.ToList();
            context.Precios.AddRange(
                new Precio { IdProducto = 1, FechaDesde = DateTime.UtcNow.AddMonths(-3), Monto = 1000 },
                new Precio { IdProducto = 2, FechaDesde = DateTime.UtcNow.AddMonths(-2), Monto = 750 },
                new Precio { IdProducto = 3, FechaDesde = DateTime.UtcNow.AddMonths(-1), Monto = 500 }
            );
            context.SaveChanges();
        }

        // 7. Personas (con contraseña hasheada)
        if (!context.Personas.Any())
        {
            var locs = context.Localidades.ToList();
            context.Personas.AddRange(
                new Persona { IdPersona = 1, NombreCompleto = "Martin Ratti", Dni = 25123456, Email = "marto@empresa.com", Password = BCrypt.Net.BCrypt.HashPassword("admin"), Telefono = "3415550101", Direccion = "Falsa 123", IdLocalidad = locs.First(l => l.Nombre == "Rosario").IdLocalidad },
                new Persona { IdPersona = 2, NombreCompleto = "Frank Fabra", Dni = 40111222, Email = "fabra@email.com", Password = BCrypt.Net.BCrypt.HashPassword("admin"), Telefono = "3411111111", Direccion = "Verdadera 456", IdLocalidad = locs.First(l => l.Nombre == "Santa Fe").IdLocalidad }
            );
            context.SaveChanges();
        }
    }
}
