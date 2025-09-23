
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
        //Comentar este comando si se ejecuta por primera vez
        //Luego, descomentarlo para el uso habitual
        //context.Database.Migrate();

        //Esta andando mal el database migrate, por ahora cada ejecucion se borra y se crea de vuelta la BDD.
        
        
        //Descomentar solo para la 1ra ejecucion. Crea la Base de Datos
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        


        // 1. Provincias
        if (!context.Provincias.Any())
        {
            context.Provincias.AddRange(
                new Provincia { Nombre = "Ciudad Autónoma de Buenos Aires" },
                new Provincia { Nombre = "Buenos Aires" },
                new Provincia { Nombre = "Catamarca" },
                new Provincia { Nombre = "Chaco" },
                new Provincia { Nombre = "Chubut" },
                new Provincia { Nombre = "Córdoba" },
                new Provincia { Nombre = "Corrientes" },
                new Provincia { Nombre = "Entre Ríos" },
                new Provincia { Nombre = "Formosa" },
                new Provincia { Nombre = "Jujuy" },
                new Provincia { Nombre = "La Pampa" },
                new Provincia { Nombre = "La Rioja" },
                new Provincia { Nombre = "Mendoza" },
                new Provincia { Nombre = "Misiones" },
                new Provincia { Nombre = "Neuquén" },
                new Provincia { Nombre = "Río Negro" },
                new Provincia { Nombre = "Salta" },
                new Provincia { Nombre = "San Juan" },
                new Provincia { Nombre = "San Luis" },
                new Provincia { Nombre = "Santa Fe" },
                new Provincia { Nombre = "Santa Cruz" },
                new Provincia { Nombre = "Santiago del Estero" },
                new Provincia { Nombre = "Tierra del Fuego, Antártida e Islas del Atlántico Sur" },
                new Provincia { Nombre = "Tucumán" }
            );
            context.SaveChanges();

        }

        // 2. Localidades 
            if (!context.Localidades.Any())
            {
                var provincias = context.Provincias.ToList();
                context.Localidades.AddRange(
                    // Santa Fe (10 localidades)
                    new Localidad { Nombre = "Rosario", IdProvincia = provincias.First(p => p.Nombre == "Santa Fe").IdProvincia },
                    new Localidad { Nombre = "Santa Fe", IdProvincia = provincias.First(p => p.Nombre == "Santa Fe").IdProvincia },
                    new Localidad { Nombre = "Venado Tuerto", IdProvincia = provincias.First(p => p.Nombre == "Santa Fe").IdProvincia },
                    new Localidad { Nombre = "Rafaela", IdProvincia = provincias.First(p => p.Nombre == "Santa Fe").IdProvincia },
                    new Localidad { Nombre = "Reconquista", IdProvincia = provincias.First(p => p.Nombre == "Santa Fe").IdProvincia },
                    new Localidad { Nombre = "San Lorenzo", IdProvincia = provincias.First(p => p.Nombre == "Santa Fe").IdProvincia },
                    new Localidad { Nombre = "Sunchales", IdProvincia = provincias.First(p => p.Nombre == "Santa Fe").IdProvincia },
                    new Localidad { Nombre = "Villa Gobernador Gálvez", IdProvincia = provincias.First(p => p.Nombre == "Santa Fe").IdProvincia },
                    new Localidad { Nombre = "Esperanza", IdProvincia = provincias.First(p => p.Nombre == "Santa Fe").IdProvincia },
                    new Localidad { Nombre = "Funes", IdProvincia = provincias.First(p => p.Nombre == "Santa Fe").IdProvincia },

                    // Buenos Aires (15 localidades)
                    new Localidad { Nombre = "La Plata", IdProvincia = provincias.First(p => p.Nombre == "Buenos Aires").IdProvincia },
                    new Localidad { Nombre = "Mar del Plata", IdProvincia = provincias.First(p => p.Nombre == "Buenos Aires").IdProvincia },
                    new Localidad { Nombre = "Bahía Blanca", IdProvincia = provincias.First(p => p.Nombre == "Buenos Aires").IdProvincia },
                    new Localidad { Nombre = "Quilmes", IdProvincia = provincias.First(p => p.Nombre == "Buenos Aires").IdProvincia },
                    new Localidad { Nombre = "Lanús", IdProvincia = provincias.First(p => p.Nombre == "Buenos Aires").IdProvincia },
                    new Localidad { Nombre = "Morón", IdProvincia = provincias.First(p => p.Nombre == "Buenos Aires").IdProvincia },
                    new Localidad { Nombre = "San Isidro", IdProvincia = provincias.First(p => p.Nombre == "Buenos Aires").IdProvincia },
                    new Localidad { Nombre = "Tigre", IdProvincia = provincias.First(p => p.Nombre == "Buenos Aires").IdProvincia },
                    new Localidad { Nombre = "Merlo", IdProvincia = provincias.First(p => p.Nombre == "Buenos Aires").IdProvincia },
                    new Localidad { Nombre = "Moreno", IdProvincia = provincias.First(p => p.Nombre == "Buenos Aires").IdProvincia },
                    new Localidad { Nombre = "Luján", IdProvincia = provincias.First(p => p.Nombre == "Buenos Aires").IdProvincia },
                    new Localidad { Nombre = "Zárate", IdProvincia = provincias.First(p => p.Nombre == "Buenos Aires").IdProvincia },
                    new Localidad { Nombre = "Campana", IdProvincia = provincias.First(p => p.Nombre == "Buenos Aires").IdProvincia },
                    new Localidad { Nombre = "Pergamino", IdProvincia = provincias.First(p => p.Nombre == "Buenos Aires").IdProvincia },
                    new Localidad { Nombre = "Necochea", IdProvincia = provincias.First(p => p.Nombre == "Buenos Aires").IdProvincia },

                    // Córdoba (10 localidades)
                    new Localidad { Nombre = "Córdoba Capital", IdProvincia = provincias.First(p => p.Nombre == "Córdoba").IdProvincia },
                    new Localidad { Nombre = "Villa María", IdProvincia = provincias.First(p => p.Nombre == "Córdoba").IdProvincia },
                    new Localidad { Nombre = "Río Cuarto", IdProvincia = provincias.First(p => p.Nombre == "Córdoba").IdProvincia },
                    new Localidad { Nombre = "Alta Gracia", IdProvincia = provincias.First(p => p.Nombre == "Córdoba").IdProvincia },
                    new Localidad { Nombre = "Villa Carlos Paz", IdProvincia = provincias.First(p => p.Nombre == "Córdoba").IdProvincia },
                    new Localidad { Nombre = "Jesús María", IdProvincia = provincias.First(p => p.Nombre == "Córdoba").IdProvincia },
                    new Localidad { Nombre = "Cosquín", IdProvincia = provincias.First(p => p.Nombre == "Córdoba").IdProvincia },
                    new Localidad { Nombre = "La Falda", IdProvincia = provincias.First(p => p.Nombre == "Córdoba").IdProvincia },
                    new Localidad { Nombre = "Río Tercero", IdProvincia = provincias.First(p => p.Nombre == "Córdoba").IdProvincia },
                    new Localidad { Nombre = "Bell Ville", IdProvincia = provincias.First(p => p.Nombre == "Córdoba").IdProvincia },

                    // Mendoza (8 localidades)
                    new Localidad { Nombre = "Mendoza", IdProvincia = provincias.First(p => p.Nombre == "Mendoza").IdProvincia },
                    new Localidad { Nombre = "San Rafael", IdProvincia = provincias.First(p => p.Nombre == "Mendoza").IdProvincia },
                    new Localidad { Nombre = "Godoy Cruz", IdProvincia = provincias.First(p => p.Nombre == "Mendoza").IdProvincia },
                    new Localidad { Nombre = "Guaymallén", IdProvincia = provincias.First(p => p.Nombre == "Mendoza").IdProvincia },
                    new Localidad { Nombre = "Las Heras", IdProvincia = provincias.First(p => p.Nombre == "Mendoza").IdProvincia },
                    new Localidad { Nombre = "Luján de Cuyo", IdProvincia = provincias.First(p => p.Nombre == "Mendoza").IdProvincia },
                    new Localidad { Nombre = "Maipú", IdProvincia = provincias.First(p => p.Nombre == "Mendoza").IdProvincia },
                    new Localidad { Nombre = "Tunuyán", IdProvincia = provincias.First(p => p.Nombre == "Mendoza").IdProvincia },

                    // Salta (7 localidades)
                    new Localidad { Nombre = "Salta", IdProvincia = provincias.First(p => p.Nombre == "Salta").IdProvincia },
                    new Localidad { Nombre = "San Ramón de la Nueva Orán", IdProvincia = provincias.First(p => p.Nombre == "Salta").IdProvincia },
                    new Localidad { Nombre = "Tartagal", IdProvincia = provincias.First(p => p.Nombre == "Salta").IdProvincia },
                    new Localidad { Nombre = "Metán", IdProvincia = provincias.First(p => p.Nombre == "Salta").IdProvincia },
                    new Localidad { Nombre = "Cafayate", IdProvincia = provincias.First(p => p.Nombre == "Salta").IdProvincia },
                    new Localidad { Nombre = "Rosario de la Frontera", IdProvincia = provincias.First(p => p.Nombre == "Salta").IdProvincia },
                    new Localidad { Nombre = "General Güemes", IdProvincia = provincias.First(p => p.Nombre == "Salta").IdProvincia },

                    // Tucumán (6 localidades)
                    new Localidad { Nombre = "San Miguel de Tucumán", IdProvincia = provincias.First(p => p.Nombre == "Tucumán").IdProvincia },
                    new Localidad { Nombre = "Yerba Buena", IdProvincia = provincias.First(p => p.Nombre == "Tucumán").IdProvincia },
                    new Localidad { Nombre = "Tafí Viejo", IdProvincia = provincias.First(p => p.Nombre == "Tucumán").IdProvincia },
                    new Localidad { Nombre = "Aguilares", IdProvincia = provincias.First(p => p.Nombre == "Tucumán").IdProvincia },
                    new Localidad { Nombre = "Concepción", IdProvincia = provincias.First(p => p.Nombre == "Tucumán").IdProvincia },
                    new Localidad { Nombre = "Monteros", IdProvincia = provincias.First(p => p.Nombre == "Tucumán").IdProvincia },

                    // Chaco (6 localidades)
                    new Localidad { Nombre = "Resistencia", IdProvincia = provincias.First(p => p.Nombre == "Chaco").IdProvincia },
                    new Localidad { Nombre = "Barranqueras", IdProvincia = provincias.First(p => p.Nombre == "Chaco").IdProvincia },
                    new Localidad { Nombre = "Presidencia Roque Sáenz Peña", IdProvincia = provincias.First(p => p.Nombre == "Chaco").IdProvincia },
                    new Localidad { Nombre = "Villa Ángela", IdProvincia = provincias.First(p => p.Nombre == "Chaco").IdProvincia },
                    new Localidad { Nombre = "Charata", IdProvincia = provincias.First(p => p.Nombre == "Chaco").IdProvincia },
                    new Localidad { Nombre = "General San Martín", IdProvincia = provincias.First(p => p.Nombre == "Chaco").IdProvincia },

                    // Río Negro (6 localidades)
                    new Localidad { Nombre = "Bariloche", IdProvincia = provincias.First(p => p.Nombre == "Río Negro").IdProvincia },
                    new Localidad { Nombre = "Viedma", IdProvincia = provincias.First(p => p.Nombre == "Río Negro").IdProvincia },
                    new Localidad { Nombre = "General Roca", IdProvincia = provincias.First(p => p.Nombre == "Río Negro").IdProvincia },
                    new Localidad { Nombre = "Cipolletti", IdProvincia = provincias.First(p => p.Nombre == "Río Negro").IdProvincia },
                    new Localidad { Nombre = "El Bolsón", IdProvincia = provincias.First(p => p.Nombre == "Río Negro").IdProvincia },
                    new Localidad { Nombre = "Allen", IdProvincia = provincias.First(p => p.Nombre == "Río Negro").IdProvincia },

                    // Ciudad Autónoma de Buenos Aires (7 localidades)
                    new Localidad { Nombre = "Palermo", IdProvincia = provincias.First(p => p.Nombre == "Ciudad Autónoma de Buenos Aires").IdProvincia },
                    new Localidad { Nombre = "Recoleta", IdProvincia = provincias.First(p => p.Nombre == "Ciudad Autónoma de Buenos Aires").IdProvincia },
                    new Localidad { Nombre = "Belgrano", IdProvincia = provincias.First(p => p.Nombre == "Ciudad Autónoma de Buenos Aires").IdProvincia },
                    new Localidad { Nombre = "Caballito", IdProvincia = provincias.First(p => p.Nombre == "Ciudad Autónoma de Buenos Aires").IdProvincia },
                    new Localidad { Nombre = "Almagro", IdProvincia = provincias.First(p => p.Nombre == "Ciudad Autónoma de Buenos Aires").IdProvincia },
                    new Localidad { Nombre = "Flores", IdProvincia = provincias.First(p => p.Nombre == "Ciudad Autónoma de Buenos Aires").IdProvincia },
                    new Localidad { Nombre = "Boedo", IdProvincia = provincias.First(p => p.Nombre == "Ciudad Autónoma de Buenos Aires").IdProvincia },

                    // Otras provincias (5 localidades cada una)
                    // Catamarca
                    new Localidad { Nombre = "San Fernando del Valle de Catamarca", IdProvincia = provincias.First(p => p.Nombre == "Catamarca").IdProvincia },
                    new Localidad { Nombre = "Valle Viejo", IdProvincia = provincias.First(p => p.Nombre == "Catamarca").IdProvincia },
                    new Localidad { Nombre = "Andalgalá", IdProvincia = provincias.First(p => p.Nombre == "Catamarca").IdProvincia },
                    new Localidad { Nombre = "Belén", IdProvincia = provincias.First(p => p.Nombre == "Catamarca").IdProvincia },
                    new Localidad { Nombre = "Tinogasta", IdProvincia = provincias.First(p => p.Nombre == "Catamarca").IdProvincia },

                    // Chubut
                    new Localidad { Nombre = "Rawson", IdProvincia = provincias.First(p => p.Nombre == "Chubut").IdProvincia },
                    new Localidad { Nombre = "Trelew", IdProvincia = provincias.First(p => p.Nombre == "Chubut").IdProvincia },
                    new Localidad { Nombre = "Puerto Madryn", IdProvincia = provincias.First(p => p.Nombre == "Chubut").IdProvincia },
                    new Localidad { Nombre = "Comodoro Rivadavia", IdProvincia = provincias.First(p => p.Nombre == "Chubut").IdProvincia },
                    new Localidad { Nombre = "Esquel", IdProvincia = provincias.First(p => p.Nombre == "Chubut").IdProvincia },

                    // Corrientes
                    new Localidad { Nombre = "Corrientes", IdProvincia = provincias.First(p => p.Nombre == "Corrientes").IdProvincia },
                    new Localidad { Nombre = "Goya", IdProvincia = provincias.First(p => p.Nombre == "Corrientes").IdProvincia },
                    new Localidad { Nombre = "Mercedes", IdProvincia = provincias.First(p => p.Nombre == "Corrientes").IdProvincia },
                    new Localidad { Nombre = "Curuzú Cuatiá", IdProvincia = provincias.First(p => p.Nombre == "Corrientes").IdProvincia },
                    new Localidad { Nombre = "Paso de los Libres", IdProvincia = provincias.First(p => p.Nombre == "Corrientes").IdProvincia },

                    // Entre Ríos
                    new Localidad { Nombre = "Paraná", IdProvincia = provincias.First(p => p.Nombre == "Entre Ríos").IdProvincia },
                    new Localidad { Nombre = "Concordia", IdProvincia = provincias.First(p => p.Nombre == "Entre Ríos").IdProvincia },
                    new Localidad { Nombre = "Gualeguaychú", IdProvincia = provincias.First(p => p.Nombre == "Entre Ríos").IdProvincia },
                    new Localidad { Nombre = "Concepción del Uruguay", IdProvincia = provincias.First(p => p.Nombre == "Entre Ríos").IdProvincia },
                    new Localidad { Nombre = "Victoria", IdProvincia = provincias.First(p => p.Nombre == "Entre Ríos").IdProvincia },

                    // Formosa
                    new Localidad { Nombre = "Formosa", IdProvincia = provincias.First(p => p.Nombre == "Formosa").IdProvincia },
                    new Localidad { Nombre = "Clorinda", IdProvincia = provincias.First(p => p.Nombre == "Formosa").IdProvincia },
                    new Localidad { Nombre = "Pirané", IdProvincia = provincias.First(p => p.Nombre == "Formosa").IdProvincia },
                    new Localidad { Nombre = "El Colorado", IdProvincia = provincias.First(p => p.Nombre == "Formosa").IdProvincia },
                    new Localidad { Nombre = "Las Lomitas", IdProvincia = provincias.First(p => p.Nombre == "Formosa").IdProvincia },

                    // Jujuy
                    new Localidad { Nombre = "San Salvador de Jujuy", IdProvincia = provincias.First(p => p.Nombre == "Jujuy").IdProvincia },
                    new Localidad { Nombre = "Palpalá", IdProvincia = provincias.First(p => p.Nombre == "Jujuy").IdProvincia },
                    new Localidad { Nombre = "La Quiaca", IdProvincia = provincias.First(p => p.Nombre == "Jujuy").IdProvincia },
                    new Localidad { Nombre = "San Pedro de Jujuy", IdProvincia = provincias.First(p => p.Nombre == "Jujuy").IdProvincia },
                    new Localidad { Nombre = "Libertador General San Martín", IdProvincia = provincias.First(p => p.Nombre == "Jujuy").IdProvincia },

                    // La Pampa
                    new Localidad { Nombre = "Santa Rosa", IdProvincia = provincias.First(p => p.Nombre == "La Pampa").IdProvincia },
                    new Localidad { Nombre = "General Pico", IdProvincia = provincias.First(p => p.Nombre == "La Pampa").IdProvincia },
                    new Localidad { Nombre = "Toay", IdProvincia = provincias.First(p => p.Nombre == "La Pampa").IdProvincia },
                    new Localidad { Nombre = "Realicó", IdProvincia = provincias.First(p => p.Nombre == "La Pampa").IdProvincia },
                    new Localidad { Nombre = "Eduardo Castex", IdProvincia = provincias.First(p => p.Nombre == "La Pampa").IdProvincia },

                    // La Rioja
                    new Localidad { Nombre = "La Rioja", IdProvincia = provincias.First(p => p.Nombre == "La Rioja").IdProvincia },
                    new Localidad { Nombre = "Chilecito", IdProvincia = provincias.First(p => p.Nombre == "La Rioja").IdProvincia },
                    new Localidad { Nombre = "Aimogasta", IdProvincia = provincias.First(p => p.Nombre == "La Rioja").IdProvincia },
                    new Localidad { Nombre = "Chamical", IdProvincia = provincias.First(p => p.Nombre == "La Rioja").IdProvincia },
                    new Localidad { Nombre = "Chepes", IdProvincia = provincias.First(p => p.Nombre == "La Rioja").IdProvincia },

                    // Misiones
                    new Localidad { Nombre = "Posadas", IdProvincia = provincias.First(p => p.Nombre == "Misiones").IdProvincia },
                    new Localidad { Nombre = "Oberá", IdProvincia = provincias.First(p => p.Nombre == "Misiones").IdProvincia },
                    new Localidad { Nombre = "Eldorado", IdProvincia = provincias.First(p => p.Nombre == "Misiones").IdProvincia },
                    new Localidad { Nombre = "Puerto Iguazú", IdProvincia = provincias.First(p => p.Nombre == "Misiones").IdProvincia },
                    new Localidad { Nombre = "San Vicente", IdProvincia = provincias.First(p => p.Nombre == "Misiones").IdProvincia },

                    // Neuquén
                    new Localidad { Nombre = "Neuquén", IdProvincia = provincias.First(p => p.Nombre == "Neuquén").IdProvincia },
                    new Localidad { Nombre = "Cutral Có", IdProvincia = provincias.First(p => p.Nombre == "Neuquén").IdProvincia },
                    new Localidad { Nombre = "Plottier", IdProvincia = provincias.First(p => p.Nombre == "Neuquén").IdProvincia },
                    new Localidad { Nombre = "San Martín de los Andes", IdProvincia = provincias.First(p => p.Nombre == "Neuquén").IdProvincia },
                    new Localidad { Nombre = "Zapala", IdProvincia = provincias.First(p => p.Nombre == "Neuquén").IdProvincia },

                    // San Juan
                    new Localidad { Nombre = "San Juan", IdProvincia = provincias.First(p => p.Nombre == "San Juan").IdProvincia },
                    new Localidad { Nombre = "Rawson", IdProvincia = provincias.First(p => p.Nombre == "San Juan").IdProvincia },
                    new Localidad { Nombre = "Rivadavia", IdProvincia = provincias.First(p => p.Nombre == "San Juan").IdProvincia },
                    new Localidad { Nombre = "Santa Lucía", IdProvincia = provincias.First(p => p.Nombre == "San Juan").IdProvincia },
                    new Localidad { Nombre = "Pocito", IdProvincia = provincias.First(p => p.Nombre == "San Juan").IdProvincia },

                    // San Luis
                    new Localidad { Nombre = "San Luis", IdProvincia = provincias.First(p => p.Nombre == "San Luis").IdProvincia },
                    new Localidad { Nombre = "Villa Mercedes", IdProvincia = provincias.First(p => p.Nombre == "San Luis").IdProvincia },
                    new Localidad { Nombre = "Merlo", IdProvincia = provincias.First(p => p.Nombre == "San Luis").IdProvincia },
                    new Localidad { Nombre = "Juana Koslay", IdProvincia = provincias.First(p => p.Nombre == "San Luis").IdProvincia },
                    new Localidad { Nombre = "La Toma", IdProvincia = provincias.First(p => p.Nombre == "San Luis").IdProvincia },

                    // Santa Cruz
                    new Localidad { Nombre = "Río Gallegos", IdProvincia = provincias.First(p => p.Nombre == "Santa Cruz").IdProvincia },
                    new Localidad { Nombre = "Caleta Olivia", IdProvincia = provincias.First(p => p.Nombre == "Santa Cruz").IdProvincia },
                    new Localidad { Nombre = "Pico Truncado", IdProvincia = provincias.First(p => p.Nombre == "Santa Cruz").IdProvincia },
                    new Localidad { Nombre = "Puerto Deseado", IdProvincia = provincias.First(p => p.Nombre == "Santa Cruz").IdProvincia },
                    new Localidad { Nombre = "Las Heras", IdProvincia = provincias.First(p => p.Nombre == "Santa Cruz").IdProvincia },

                    // Santiago del Estero
                    new Localidad { Nombre = "Santiago del Estero", IdProvincia = provincias.First(p => p.Nombre == "Santiago del Estero").IdProvincia },
                    new Localidad { Nombre = "La Banda", IdProvincia = provincias.First(p => p.Nombre == "Santiago del Estero").IdProvincia },
                    new Localidad { Nombre = "Frías", IdProvincia = provincias.First(p => p.Nombre == "Santiago del Estero").IdProvincia },
                    new Localidad { Nombre = "Añatuya", IdProvincia = provincias.First(p => p.Nombre == "Santiago del Estero").IdProvincia },
                    new Localidad { Nombre = "Termas de Río Hondo", IdProvincia = provincias.First(p => p.Nombre == "Santiago del Estero").IdProvincia },

                    // Tierra del Fuego
                    new Localidad { Nombre = "Ushuaia", IdProvincia = provincias.First(p => p.Nombre == "Tierra del Fuego, Antártida e Islas del Atlántico Sur").IdProvincia },
                    new Localidad { Nombre = "Río Grande", IdProvincia = provincias.First(p => p.Nombre == "Tierra del Fuego, Antártida e Islas del Atlántico Sur").IdProvincia },
                    new Localidad { Nombre = "Tolhuin", IdProvincia = provincias.First(p => p.Nombre == "Tierra del Fuego, Antártida e Islas del Atlántico Sur").IdProvincia },
                    new Localidad { Nombre = "Puerto Almanza", IdProvincia = provincias.First(p => p.Nombre == "Tierra del Fuego, Antártida e Islas del Atlántico Sur").IdProvincia },
                    new Localidad { Nombre = "Estancia Harberton", IdProvincia = provincias.First(p => p.Nombre == "Tierra del Fuego, Antártida e Islas del Atlántico Sur").IdProvincia }
                );
                context.SaveChanges();
            }

        // 3. TiposProductos
        if (!context.TiposProductos.Any())
        {
            context.TiposProductos.AddRange(
                new TipoProducto { Descripcion = "Componentes" },
                new TipoProducto { Descripcion = "Monitores" },
                new TipoProducto { Descripcion = "Parlantes" },
                new TipoProducto { Descripcion = "Teclados" },
                new TipoProducto { Descripcion = "Mouse" }
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
                    RazonSocial = "Distrito Digital S.A.",
                    Cuit = "30-12345678-9",
                    Telefono = "3416667788",
                    Email = "compras@distritodigital.com",
                    Direccion = "Calle Falsa 123",
                    IdLocalidad = localidades.First(l => l.Nombre == "Rosario").IdLocalidad
                },
                new Proveedor
                {
                    RazonSocial = "Logística Computacional S.R.L.",
                    Cuit = "30-98765432-1",
                    Telefono = "116665544",
                    Email = "ventas@logisticacompsrl.com",
                    Direccion = "Avenida Siempre Viva 742",
                    IdLocalidad = localidades.First(l => l.Nombre == "Córdoba Capital").IdLocalidad
                },
                new Proveedor
                {
                    RazonSocial = "TecnoImport Argentina",
                    Cuit = "30-55555555-5",
                    Telefono = "114445566",
                    Email = "ventas@tecnoimport.com",
                    Direccion = "Av. Corrientes 1234",
                    IdLocalidad = localidades.First(l => l.Nombre == "Palermo").IdLocalidad
                }
            );
            context.SaveChanges();
        }

        // 5. Productos
        if (!context.Productos.Any())
        {
            var tipos = context.TiposProductos.ToList();
            context.Productos.AddRange(
                new Producto { Nombre = "MotherBoard Ryzen 5.0", Descripcion = "Mother Asus", Stock = 150, IdTipoProducto = tipos.First(t => t.Descripcion == "Componentes").IdTipoProducto },
                new Producto { Nombre = "Monitor Curvo TLC", Descripcion = "Monitor Curvo 20°", Stock = 200, IdTipoProducto = tipos.First(t => t.Descripcion == "Monitores").IdTipoProducto },
                new Producto { Nombre = "Parlante Huge HBL", Descripcion = "Sonido Envolvente", Stock = 100, IdTipoProducto = tipos.First(t => t.Descripcion == "Parlantes").IdTipoProducto },
                new Producto { Nombre = "Teclado Mecánico RGB", Descripcion = "Teclado gaming mecánico", Stock = 80, IdTipoProducto = tipos.First(t => t.Descripcion == "Teclados").IdTipoProducto },
                new Producto { Nombre = "Mouse Inalámbrico", Descripcion = "Mouse ergonómico inalámbrico", Stock = 120, IdTipoProducto = tipos.First(t => t.Descripcion == "Mouse").IdTipoProducto }
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
                new Precio { IdProducto = 3, FechaDesde = DateTime.UtcNow.AddMonths(-1), Monto = 500 },
                new Precio { IdProducto = 4, FechaDesde = DateTime.UtcNow.AddMonths(-2), Monto = 250 },
                new Precio { IdProducto = 5, FechaDesde = DateTime.UtcNow.AddMonths(-1), Monto = 150 }
            );
            context.SaveChanges();
        }

        // 7. Personas (con contraseña hasheada)
        if (!context.Personas.Any())
        {
            var locs = context.Localidades.ToList();
            context.Personas.AddRange(
                // Administradores
                new Persona
                {
                    NombreCompleto = "Martin Ratti",
                    Dni = 12345678,
                    Email = "marto@empresa.com",
                    Password = "$2a$11$685W/fQbRiFtx1K8lSS8AuuYOj07/ygRo8Fr08y/qXD5r4tVrWyjm",
                    Telefono = "34115559101",
                    Direccion = "Falsa 123",
                    IdLocalidad = locs.First(l => l.Nombre == "Rosario").IdLocalidad,
                    FechaIngreso = new DateOnly(2020, 1, 15)
                },
                new Persona
                {
                    NombreCompleto = "Frank Fabra",
                    Dni = 41111111,
                    Email = "fabra@email.com",
                    Password = "$2a$11$ibah0XV1qx.0TMSQ6hh.wOHxDdeYMm1E7raaeg3MzDIiPyhbKQOgK",
                    Telefono = "3411111111",
                    Direccion = "Verdadera 456",
                    IdLocalidad = locs.First(l => l.Nombre == "Santa Fe").IdLocalidad,
                    FechaIngreso = new DateOnly(2021, 3, 20)
                },

                // Empleados
                new Persona
                {
                    NombreCompleto = "Joaquin Peralta",
                    Dni = 30789123,
                    Email = "joaquin@empresa.com",
                    Password = "$2a$11$BbVT9kjYoeQWiAq3jCYdje2O9nVkX4Gq76ja.cMrwfzvrngzowUpm",
                    Telefono = "115550202",
                    Direccion = "Avenida Imaginaria 2",
                    IdLocalidad = locs.First(l => l.Nombre == "La Plata").IdLocalidad,
                    FechaIngreso = new DateOnly(2022, 5, 10)
                },
                new Persona
                {
                    NombreCompleto = "Ayrton Costa",
                    Dni = 42333444,
                    Email = "ayrton@email.com",
                    Password = "$2a$11$h/0tUM5e3qLbrT6m2b8WyOEGeZXaATBpyD/L/pQoacWMVJvnTpGXe",
                    Telefono = "3415552222",
                    Direccion = "Calle Demo 4",
                    IdLocalidad = locs.First(l => l.Nombre == "Córdoba Capital").IdLocalidad,
                    FechaIngreso = new DateOnly(2023, 2, 15)
                },
                new Persona
                {
                    NombreCompleto = "Luka Doncic",
                    Dni = 42553400,
                    Email = "luka@email.com",
                    Password = "$2a$11$MsG.VnnowVSZCMqgrOSkj.EMqh2i/PfJ643xRLk76jA4Yvo9G7fgS",
                    Telefono = "3415882922",
                    Direccion = "Calle Prueba 5",
                    IdLocalidad = locs.First(l => l.Nombre == "Mendoza").IdLocalidad,
                    FechaIngreso = new DateOnly(2022, 8, 30)
                },
                new Persona
                {
                    NombreCompleto = "Stephen Curry",
                    Dni = 32393404,
                    Email = "curry@email.com",
                    Password = "$2a$11$ChfD2UiugA.ZuH1X/w2kLOlcIvwbeq4TpSy6gM1P0VhldnFQDsLVe",
                    Telefono = "3415559202",
                    Direccion = "Calle Test 6",
                    IdLocalidad = locs.First(l => l.Nombre == "Bariloche").IdLocalidad,
                    FechaIngreso = new DateOnly(2021, 11, 5)
                },
                new Persona
                {
                    NombreCompleto = "Maria Gonzalez",
                    Dni = 35456789,
                    Email = "maria.g@email.com",
                    Password = "$2a$11$aDmAJVe1pHTTRiAzAf6m7ek/rsSDM8RZ7aJJPaNjTcvogX0EHCRSm",
                    Telefono = "3416667777",
                    Direccion = "Av. Central 789",
                    IdLocalidad = locs.First(l => l.Nombre == "Rosario").IdLocalidad,
                    FechaIngreso = new DateOnly(2023, 6, 1)
                },
                new Persona
                {
                    NombreCompleto = "Carlos Lopez",
                    Dni = 28765432,
                    Email = "carlos.l@email.com",
                    Password = "$2a$11$uWvxb3fI1P/ElZkImtW4FeYkhitKgX/nEWT6QCfzHdVrtlLBFYF.q",
                    Telefono = "3417778888",
                    Direccion = "Calle Secundaria 321",
                    IdLocalidad = locs.First(l => l.Nombre == "Santa Fe").IdLocalidad,
                    FechaIngreso = new DateOnly(2022, 12, 10)
                },
                new Persona
                {
                    NombreCompleto = "Ana Martinez",
                    Dni = 39876543,
                    Email = "ana.m@email.com",
                    Password = "$2a$11$xLNEyeWBTFpnrkyhFizEAOiszUnX5RyKyuxRNslL9KdBXn4HOAWIC",
                    Telefono = "3418889999",
                    Direccion = "Pasaje Privado 654",
                    IdLocalidad = locs.First(l => l.Nombre == "Córdoba Capital").IdLocalidad,
                    FechaIngreso = new DateOnly(2023, 3, 25)
                },
                new Persona
                {
                    NombreCompleto = "Pedro Rodriguez",
                    Dni = 40987654,
                    Email = "pedro.r@email.com",
                    Password = "$2a$11$VqfW6nNbrTw3U6uiICnTwuw0KQ17RjJHoY97FQaInrTJr8nh3pJJe",
                    Telefono = "3419990000",
                    Direccion = "Boulevard Principal 987",
                    IdLocalidad = locs.First(l => l.Nombre == "Mendoza").IdLocalidad,
                    FechaIngreso = new DateOnly(2022, 7, 15)
                }
            );
            context.SaveChanges();
        }


        if (!context.Ventas.Any())
        {
            var personas = context.Personas.ToList();
            var productos = context.Productos.ToList();

            context.Ventas.AddRange(
                new Venta
                {
                    Fecha = DateTime.UtcNow.AddDays(-10),
                    Estado = "Pagada",
                    IdPersona = personas[2].IdPersona
                },
                new Venta
                {
                    Fecha = DateTime.UtcNow.AddDays(-5),
                    Estado = "Pagada",
                    IdPersona = personas[3].IdPersona
                },
                new Venta
                {
                    Fecha = DateTime.UtcNow.AddDays(-2),
                    Estado = "Pendiente",
                    IdPersona = personas[4].IdPersona
                },
                new Venta
                {
                    Fecha = DateTime.UtcNow.AddDays(-1),
                    Estado = "Entregada",
                    IdPersona = personas[5].IdPersona
                }
            );
            context.SaveChanges();
        }

        Console.WriteLine("Database seeded successfully!");
    }
}