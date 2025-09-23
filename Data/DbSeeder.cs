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

        // 2. Localidades 
        if (!context.Localidades.Any())
        {
            var provincias = context.Provincias.ToList();
            context.Localidades.AddRange(
                // Santa Fe (10 localidades)
                new Localidad { IdLocalidad = 1, Nombre = "Rosario", IdProvincia = provincias.First(p => p.Nombre == "Santa Fe").IdProvincia },
                new Localidad { IdLocalidad = 2, Nombre = "Santa Fe", IdProvincia = provincias.First(p => p.Nombre == "Santa Fe").IdProvincia },
                new Localidad { IdLocalidad = 3, Nombre = "Venado Tuerto", IdProvincia = provincias.First(p => p.Nombre == "Santa Fe").IdProvincia },
                new Localidad { IdLocalidad = 4, Nombre = "Rafaela", IdProvincia = provincias.First(p => p.Nombre == "Santa Fe").IdProvincia },
                new Localidad { IdLocalidad = 5, Nombre = "Reconquista", IdProvincia = provincias.First(p => p.Nombre == "Santa Fe").IdProvincia },
                new Localidad { IdLocalidad = 26, Nombre = "San Lorenzo", IdProvincia = provincias.First(p => p.Nombre == "Santa Fe").IdProvincia },
                new Localidad { IdLocalidad = 27, Nombre = "Sunchales", IdProvincia = provincias.First(p => p.Nombre == "Santa Fe").IdProvincia },
                new Localidad { IdLocalidad = 28, Nombre = "Villa Gobernador Gálvez", IdProvincia = provincias.First(p => p.Nombre == "Santa Fe").IdProvincia },
                new Localidad { IdLocalidad = 29, Nombre = "Esperanza", IdProvincia = provincias.First(p => p.Nombre == "Santa Fe").IdProvincia },
                new Localidad { IdLocalidad = 30, Nombre = "Funes", IdProvincia = provincias.First(p => p.Nombre == "Santa Fe").IdProvincia },

                // Buenos Aires (15 localidades)
                new Localidad { IdLocalidad = 6, Nombre = "La Plata", IdProvincia = provincias.First(p => p.Nombre == "Buenos Aires").IdProvincia },
                new Localidad { IdLocalidad = 7, Nombre = "Mar del Plata", IdProvincia = provincias.First(p => p.Nombre == "Buenos Aires").IdProvincia },
                new Localidad { IdLocalidad = 8, Nombre = "Bahía Blanca", IdProvincia = provincias.First(p => p.Nombre == "Buenos Aires").IdProvincia },
                new Localidad { IdLocalidad = 9, Nombre = "Quilmes", IdProvincia = provincias.First(p => p.Nombre == "Buenos Aires").IdProvincia },
                new Localidad { IdLocalidad = 10, Nombre = "Lanús", IdProvincia = provincias.First(p => p.Nombre == "Buenos Aires").IdProvincia },
                new Localidad { IdLocalidad = 31, Nombre = "Morón", IdProvincia = provincias.First(p => p.Nombre == "Buenos Aires").IdProvincia },
                new Localidad { IdLocalidad = 32, Nombre = "San Isidro", IdProvincia = provincias.First(p => p.Nombre == "Buenos Aires").IdProvincia },
                new Localidad { IdLocalidad = 33, Nombre = "Tigre", IdProvincia = provincias.First(p => p.Nombre == "Buenos Aires").IdProvincia },
                new Localidad { IdLocalidad = 34, Nombre = "Merlo", IdProvincia = provincias.First(p => p.Nombre == "Buenos Aires").IdProvincia },
                new Localidad { IdLocalidad = 35, Nombre = "Moreno", IdProvincia = provincias.First(p => p.Nombre == "Buenos Aires").IdProvincia },
                new Localidad { IdLocalidad = 36, Nombre = "Luján", IdProvincia = provincias.First(p => p.Nombre == "Buenos Aires").IdProvincia },
                new Localidad { IdLocalidad = 37, Nombre = "Zárate", IdProvincia = provincias.First(p => p.Nombre == "Buenos Aires").IdProvincia },
                new Localidad { IdLocalidad = 38, Nombre = "Campana", IdProvincia = provincias.First(p => p.Nombre == "Buenos Aires").IdProvincia },
                new Localidad { IdLocalidad = 39, Nombre = "Pergamino", IdProvincia = provincias.First(p => p.Nombre == "Buenos Aires").IdProvincia },
                new Localidad { IdLocalidad = 40, Nombre = "Necochea", IdProvincia = provincias.First(p => p.Nombre == "Buenos Aires").IdProvincia },

                // Córdoba (10 localidades)
                new Localidad { IdLocalidad = 11, Nombre = "Córdoba Capital", IdProvincia = provincias.First(p => p.Nombre == "Córdoba").IdProvincia },
                new Localidad { IdLocalidad = 12, Nombre = "Villa María", IdProvincia = provincias.First(p => p.Nombre == "Córdoba").IdProvincia },
                new Localidad { IdLocalidad = 13, Nombre = "Río Cuarto", IdProvincia = provincias.First(p => p.Nombre == "Córdoba").IdProvincia },
                new Localidad { IdLocalidad = 14, Nombre = "Alta Gracia", IdProvincia = provincias.First(p => p.Nombre == "Córdoba").IdProvincia },
                new Localidad { IdLocalidad = 15, Nombre = "Villa Carlos Paz", IdProvincia = provincias.First(p => p.Nombre == "Córdoba").IdProvincia },
                new Localidad { IdLocalidad = 41, Nombre = "Jesús María", IdProvincia = provincias.First(p => p.Nombre == "Córdoba").IdProvincia },
                new Localidad { IdLocalidad = 42, Nombre = "Cosquín", IdProvincia = provincias.First(p => p.Nombre == "Córdoba").IdProvincia },
                new Localidad { IdLocalidad = 43, Nombre = "La Falda", IdProvincia = provincias.First(p => p.Nombre == "Córdoba").IdProvincia },
                new Localidad { IdLocalidad = 44, Nombre = "Río Tercero", IdProvincia = provincias.First(p => p.Nombre == "Córdoba").IdProvincia },
                new Localidad { IdLocalidad = 45, Nombre = "Bell Ville", IdProvincia = provincias.First(p => p.Nombre == "Córdoba").IdProvincia },

                // Mendoza (8 localidades)
                new Localidad { IdLocalidad = 16, Nombre = "Mendoza", IdProvincia = provincias.First(p => p.Nombre == "Mendoza").IdProvincia },
                new Localidad { IdLocalidad = 17, Nombre = "San Rafael", IdProvincia = provincias.First(p => p.Nombre == "Mendoza").IdProvincia },
                new Localidad { IdLocalidad = 18, Nombre = "Godoy Cruz", IdProvincia = provincias.First(p => p.Nombre == "Mendoza").IdProvincia },
                new Localidad { IdLocalidad = 46, Nombre = "Guaymallén", IdProvincia = provincias.First(p => p.Nombre == "Mendoza").IdProvincia },
                new Localidad { IdLocalidad = 47, Nombre = "Las Heras", IdProvincia = provincias.First(p => p.Nombre == "Mendoza").IdProvincia },
                new Localidad { IdLocalidad = 48, Nombre = "Luján de Cuyo", IdProvincia = provincias.First(p => p.Nombre == "Mendoza").IdProvincia },
                new Localidad { IdLocalidad = 49, Nombre = "Maipú", IdProvincia = provincias.First(p => p.Nombre == "Mendoza").IdProvincia },
                new Localidad { IdLocalidad = 50, Nombre = "Tunuyán", IdProvincia = provincias.First(p => p.Nombre == "Mendoza").IdProvincia },

                // Salta (7 localidades)
                new Localidad { IdLocalidad = 19, Nombre = "Salta", IdProvincia = provincias.First(p => p.Nombre == "Salta").IdProvincia },
                new Localidad { IdLocalidad = 20, Nombre = "San Ramón de la Nueva Orán", IdProvincia = provincias.First(p => p.Nombre == "Salta").IdProvincia },
                new Localidad { IdLocalidad = 51, Nombre = "Tartagal", IdProvincia = provincias.First(p => p.Nombre == "Salta").IdProvincia },
                new Localidad { IdLocalidad = 52, Nombre = "Metán", IdProvincia = provincias.First(p => p.Nombre == "Salta").IdProvincia },
                new Localidad { IdLocalidad = 53, Nombre = "Cafayate", IdProvincia = provincias.First(p => p.Nombre == "Salta").IdProvincia },
                new Localidad { IdLocalidad = 54, Nombre = "Rosario de la Frontera", IdProvincia = provincias.First(p => p.Nombre == "Salta").IdProvincia },
                new Localidad { IdLocalidad = 55, Nombre = "General Güemes", IdProvincia = provincias.First(p => p.Nombre == "Salta").IdProvincia },

                // Tucumán (6 localidades)
                new Localidad { IdLocalidad = 21, Nombre = "San Miguel de Tucumán", IdProvincia = provincias.First(p => p.Nombre == "Tucumán").IdProvincia },
                new Localidad { IdLocalidad = 56, Nombre = "Yerba Buena", IdProvincia = provincias.First(p => p.Nombre == "Tucumán").IdProvincia },
                new Localidad { IdLocalidad = 57, Nombre = "Tafí Viejo", IdProvincia = provincias.First(p => p.Nombre == "Tucumán").IdProvincia },
                new Localidad { IdLocalidad = 58, Nombre = "Aguilares", IdProvincia = provincias.First(p => p.Nombre == "Tucumán").IdProvincia },
                new Localidad { IdLocalidad = 59, Nombre = "Concepción", IdProvincia = provincias.First(p => p.Nombre == "Tucumán").IdProvincia },
                new Localidad { IdLocalidad = 60, Nombre = "Monteros", IdProvincia = provincias.First(p => p.Nombre == "Tucumán").IdProvincia },

                // Chaco (6 localidades)
                new Localidad { IdLocalidad = 22, Nombre = "Resistencia", IdProvincia = provincias.First(p => p.Nombre == "Chaco").IdProvincia },
                new Localidad { IdLocalidad = 61, Nombre = "Barranqueras", IdProvincia = provincias.First(p => p.Nombre == "Chaco").IdProvincia },
                new Localidad { IdLocalidad = 62, Nombre = "Presidencia Roque Sáenz Peña", IdProvincia = provincias.First(p => p.Nombre == "Chaco").IdProvincia },
                new Localidad { IdLocalidad = 63, Nombre = "Villa Ángela", IdProvincia = provincias.First(p => p.Nombre == "Chaco").IdProvincia },
                new Localidad { IdLocalidad = 64, Nombre = "Charata", IdProvincia = provincias.First(p => p.Nombre == "Chaco").IdProvincia },
                new Localidad { IdLocalidad = 65, Nombre = "General San Martín", IdProvincia = provincias.First(p => p.Nombre == "Chaco").IdProvincia },

                // Río Negro (6 localidades)
                new Localidad { IdLocalidad = 23, Nombre = "Bariloche", IdProvincia = provincias.First(p => p.Nombre == "Río Negro").IdProvincia },
                new Localidad { IdLocalidad = 66, Nombre = "Viedma", IdProvincia = provincias.First(p => p.Nombre == "Río Negro").IdProvincia },
                new Localidad { IdLocalidad = 67, Nombre = "General Roca", IdProvincia = provincias.First(p => p.Nombre == "Río Negro").IdProvincia },
                new Localidad { IdLocalidad = 68, Nombre = "Cipolletti", IdProvincia = provincias.First(p => p.Nombre == "Río Negro").IdProvincia },
                new Localidad { IdLocalidad = 69, Nombre = "El Bolsón", IdProvincia = provincias.First(p => p.Nombre == "Río Negro").IdProvincia },
                new Localidad { IdLocalidad = 70, Nombre = "Allen", IdProvincia = provincias.First(p => p.Nombre == "Río Negro").IdProvincia },

                // Ciudad Autónoma de Buenos Aires (7 localidades)
                new Localidad { IdLocalidad = 24, Nombre = "Palermo", IdProvincia = provincias.First(p => p.Nombre == "Ciudad Autónoma de Buenos Aires").IdProvincia },
                new Localidad { IdLocalidad = 25, Nombre = "Recoleta", IdProvincia = provincias.First(p => p.Nombre == "Ciudad Autónoma de Buenos Aires").IdProvincia },
                new Localidad { IdLocalidad = 71, Nombre = "Belgrano", IdProvincia = provincias.First(p => p.Nombre == "Ciudad Autónoma de Buenos Aires").IdProvincia },
                new Localidad { IdLocalidad = 72, Nombre = "Caballito", IdProvincia = provincias.First(p => p.Nombre == "Ciudad Autónoma de Buenos Aires").IdProvincia },
                new Localidad { IdLocalidad = 73, Nombre = "Almagro", IdProvincia = provincias.First(p => p.Nombre == "Ciudad Autónoma de Buenos Aires").IdProvincia },
                new Localidad { IdLocalidad = 74, Nombre = "Flores", IdProvincia = provincias.First(p => p.Nombre == "Ciudad Autónoma de Buenos Aires").IdProvincia },
                new Localidad { IdLocalidad = 75, Nombre = "Boedo", IdProvincia = provincias.First(p => p.Nombre == "Ciudad Autónoma de Buenos Aires").IdProvincia },

                // Otras provincias (5 localidades cada una)
                // Catamarca
                new Localidad { IdLocalidad = 76, Nombre = "San Fernando del Valle de Catamarca", IdProvincia = provincias.First(p => p.Nombre == "Catamarca").IdProvincia },
                new Localidad { IdLocalidad = 77, Nombre = "Valle Viejo", IdProvincia = provincias.First(p => p.Nombre == "Catamarca").IdProvincia },
                new Localidad { IdLocalidad = 78, Nombre = "Andalgalá", IdProvincia = provincias.First(p => p.Nombre == "Catamarca").IdProvincia },
                new Localidad { IdLocalidad = 79, Nombre = "Belén", IdProvincia = provincias.First(p => p.Nombre == "Catamarca").IdProvincia },
                new Localidad { IdLocalidad = 80, Nombre = "Tinogasta", IdProvincia = provincias.First(p => p.Nombre == "Catamarca").IdProvincia },

                // Chubut
                new Localidad { IdLocalidad = 81, Nombre = "Rawson", IdProvincia = provincias.First(p => p.Nombre == "Chubut").IdProvincia },
                new Localidad { IdLocalidad = 82, Nombre = "Trelew", IdProvincia = provincias.First(p => p.Nombre == "Chubut").IdProvincia },
                new Localidad { IdLocalidad = 83, Nombre = "Puerto Madryn", IdProvincia = provincias.First(p => p.Nombre == "Chubut").IdProvincia },
                new Localidad { IdLocalidad = 84, Nombre = "Comodoro Rivadavia", IdProvincia = provincias.First(p => p.Nombre == "Chubut").IdProvincia },
                new Localidad { IdLocalidad = 85, Nombre = "Esquel", IdProvincia = provincias.First(p => p.Nombre == "Chubut").IdProvincia },

                // Corrientes
                new Localidad { IdLocalidad = 86, Nombre = "Corrientes", IdProvincia = provincias.First(p => p.Nombre == "Corrientes").IdProvincia },
                new Localidad { IdLocalidad = 87, Nombre = "Goya", IdProvincia = provincias.First(p => p.Nombre == "Corrientes").IdProvincia },
                new Localidad { IdLocalidad = 88, Nombre = "Mercedes", IdProvincia = provincias.First(p => p.Nombre == "Corrientes").IdProvincia },
                new Localidad { IdLocalidad = 89, Nombre = "Curuzú Cuatiá", IdProvincia = provincias.First(p => p.Nombre == "Corrientes").IdProvincia },
                new Localidad { IdLocalidad = 90, Nombre = "Paso de los Libres", IdProvincia = provincias.First(p => p.Nombre == "Corrientes").IdProvincia },

                // Entre Ríos
                new Localidad { IdLocalidad = 91, Nombre = "Paraná", IdProvincia = provincias.First(p => p.Nombre == "Entre Ríos").IdProvincia },
                new Localidad { IdLocalidad = 92, Nombre = "Concordia", IdProvincia = provincias.First(p => p.Nombre == "Entre Ríos").IdProvincia },
                new Localidad { IdLocalidad = 93, Nombre = "Gualeguaychú", IdProvincia = provincias.First(p => p.Nombre == "Entre Ríos").IdProvincia },
                new Localidad { IdLocalidad = 94, Nombre = "Concepción del Uruguay", IdProvincia = provincias.First(p => p.Nombre == "Entre Ríos").IdProvincia },
                new Localidad { IdLocalidad = 95, Nombre = "Victoria", IdProvincia = provincias.First(p => p.Nombre == "Entre Ríos").IdProvincia },

                // Formosa
                new Localidad { IdLocalidad = 96, Nombre = "Formosa", IdProvincia = provincias.First(p => p.Nombre == "Formosa").IdProvincia },
                new Localidad { IdLocalidad = 97, Nombre = "Clorinda", IdProvincia = provincias.First(p => p.Nombre == "Formosa").IdProvincia },
                new Localidad { IdLocalidad = 98, Nombre = "Pirané", IdProvincia = provincias.First(p => p.Nombre == "Formosa").IdProvincia },
                new Localidad { IdLocalidad = 99, Nombre = "El Colorado", IdProvincia = provincias.First(p => p.Nombre == "Formosa").IdProvincia },
                new Localidad { IdLocalidad = 100, Nombre = "Las Lomitas", IdProvincia = provincias.First(p => p.Nombre == "Formosa").IdProvincia },

                // Jujuy
                new Localidad { IdLocalidad = 101, Nombre = "San Salvador de Jujuy", IdProvincia = provincias.First(p => p.Nombre == "Jujuy").IdProvincia },
                new Localidad { IdLocalidad = 102, Nombre = "Palpalá", IdProvincia = provincias.First(p => p.Nombre == "Jujuy").IdProvincia },
                new Localidad { IdLocalidad = 103, Nombre = "La Quiaca", IdProvincia = provincias.First(p => p.Nombre == "Jujuy").IdProvincia },
                new Localidad { IdLocalidad = 104, Nombre = "San Pedro de Jujuy", IdProvincia = provincias.First(p => p.Nombre == "Jujuy").IdProvincia },
                new Localidad { IdLocalidad = 105, Nombre = "Libertador General San Martín", IdProvincia = provincias.First(p => p.Nombre == "Jujuy").IdProvincia },

                // La Pampa
                new Localidad { IdLocalidad = 106, Nombre = "Santa Rosa", IdProvincia = provincias.First(p => p.Nombre == "La Pampa").IdProvincia },
                new Localidad { IdLocalidad = 107, Nombre = "General Pico", IdProvincia = provincias.First(p => p.Nombre == "La Pampa").IdProvincia },
                new Localidad { IdLocalidad = 108, Nombre = "Toay", IdProvincia = provincias.First(p => p.Nombre == "La Pampa").IdProvincia },
                new Localidad { IdLocalidad = 109, Nombre = "Realicó", IdProvincia = provincias.First(p => p.Nombre == "La Pampa").IdProvincia },
                new Localidad { IdLocalidad = 110, Nombre = "Eduardo Castex", IdProvincia = provincias.First(p => p.Nombre == "La Pampa").IdProvincia },

                // La Rioja
                new Localidad { IdLocalidad = 111, Nombre = "La Rioja", IdProvincia = provincias.First(p => p.Nombre == "La Rioja").IdProvincia },
                new Localidad { IdLocalidad = 112, Nombre = "Chilecito", IdProvincia = provincias.First(p => p.Nombre == "La Rioja").IdProvincia },
                new Localidad { IdLocalidad = 113, Nombre = "Aimogasta", IdProvincia = provincias.First(p => p.Nombre == "La Rioja").IdProvincia },
                new Localidad { IdLocalidad = 114, Nombre = "Chamical", IdProvincia = provincias.First(p => p.Nombre == "La Rioja").IdProvincia },
                new Localidad { IdLocalidad = 115, Nombre = "Chepes", IdProvincia = provincias.First(p => p.Nombre == "La Rioja").IdProvincia },

                // Misiones
                new Localidad { IdLocalidad = 116, Nombre = "Posadas", IdProvincia = provincias.First(p => p.Nombre == "Misiones").IdProvincia },
                new Localidad { IdLocalidad = 117, Nombre = "Oberá", IdProvincia = provincias.First(p => p.Nombre == "Misiones").IdProvincia },
                new Localidad { IdLocalidad = 118, Nombre = "Eldorado", IdProvincia = provincias.First(p => p.Nombre == "Misiones").IdProvincia },
                new Localidad { IdLocalidad = 119, Nombre = "Puerto Iguazú", IdProvincia = provincias.First(p => p.Nombre == "Misiones").IdProvincia },
                new Localidad { IdLocalidad = 120, Nombre = "San Vicente", IdProvincia = provincias.First(p => p.Nombre == "Misiones").IdProvincia },

                // Neuquén
                new Localidad { IdLocalidad = 121, Nombre = "Neuquén", IdProvincia = provincias.First(p => p.Nombre == "Neuquén").IdProvincia },
                new Localidad { IdLocalidad = 122, Nombre = "Cutral Có", IdProvincia = provincias.First(p => p.Nombre == "Neuquén").IdProvincia },
                new Localidad { IdLocalidad = 123, Nombre = "Plottier", IdProvincia = provincias.First(p => p.Nombre == "Neuquén").IdProvincia },
                new Localidad { IdLocalidad = 124, Nombre = "San Martín de los Andes", IdProvincia = provincias.First(p => p.Nombre == "Neuquén").IdProvincia },
                new Localidad { IdLocalidad = 125, Nombre = "Zapala", IdProvincia = provincias.First(p => p.Nombre == "Neuquén").IdProvincia },

                // San Juan
                new Localidad { IdLocalidad = 126, Nombre = "San Juan", IdProvincia = provincias.First(p => p.Nombre == "San Juan").IdProvincia },
                new Localidad { IdLocalidad = 127, Nombre = "Rawson", IdProvincia = provincias.First(p => p.Nombre == "San Juan").IdProvincia },
                new Localidad { IdLocalidad = 128, Nombre = "Rivadavia", IdProvincia = provincias.First(p => p.Nombre == "San Juan").IdProvincia },
                new Localidad { IdLocalidad = 129, Nombre = "Santa Lucía", IdProvincia = provincias.First(p => p.Nombre == "San Juan").IdProvincia },
                new Localidad { IdLocalidad = 130, Nombre = "Pocito", IdProvincia = provincias.First(p => p.Nombre == "San Juan").IdProvincia },

                // San Luis
                new Localidad { IdLocalidad = 131, Nombre = "San Luis", IdProvincia = provincias.First(p => p.Nombre == "San Luis").IdProvincia },
                new Localidad { IdLocalidad = 132, Nombre = "Villa Mercedes", IdProvincia = provincias.First(p => p.Nombre == "San Luis").IdProvincia },
                new Localidad { IdLocalidad = 133, Nombre = "Merlo", IdProvincia = provincias.First(p => p.Nombre == "San Luis").IdProvincia },
                new Localidad { IdLocalidad = 134, Nombre = "Juana Koslay", IdProvincia = provincias.First(p => p.Nombre == "San Luis").IdProvincia },
                new Localidad { IdLocalidad = 135, Nombre = "La Toma", IdProvincia = provincias.First(p => p.Nombre == "San Luis").IdProvincia },

                // Santa Cruz
                new Localidad { IdLocalidad = 136, Nombre = "Río Gallegos", IdProvincia = provincias.First(p => p.Nombre == "Santa Cruz").IdProvincia },
                new Localidad { IdLocalidad = 137, Nombre = "Caleta Olivia", IdProvincia = provincias.First(p => p.Nombre == "Santa Cruz").IdProvincia },
                new Localidad { IdLocalidad = 138, Nombre = "Pico Truncado", IdProvincia = provincias.First(p => p.Nombre == "Santa Cruz").IdProvincia },
                new Localidad { IdLocalidad = 139, Nombre = "Puerto Deseado", IdProvincia = provincias.First(p => p.Nombre == "Santa Cruz").IdProvincia },
                new Localidad { IdLocalidad = 140, Nombre = "Las Heras", IdProvincia = provincias.First(p => p.Nombre == "Santa Cruz").IdProvincia },

                // Santiago del Estero
                new Localidad { IdLocalidad = 141, Nombre = "Santiago del Estero", IdProvincia = provincias.First(p => p.Nombre == "Santiago del Estero").IdProvincia },
                new Localidad { IdLocalidad = 142, Nombre = "La Banda", IdProvincia = provincias.First(p => p.Nombre == "Santiago del Estero").IdProvincia },
                new Localidad { IdLocalidad = 143, Nombre = "Frías", IdProvincia = provincias.First(p => p.Nombre == "Santiago del Estero").IdProvincia },
                new Localidad { IdLocalidad = 144, Nombre = "Añatuya", IdProvincia = provincias.First(p => p.Nombre == "Santiago del Estero").IdProvincia },
                new Localidad { IdLocalidad = 145, Nombre = "Termas de Río Hondo", IdProvincia = provincias.First(p => p.Nombre == "Santiago del Estero").IdProvincia },

                // Tierra del Fuego
                new Localidad { IdLocalidad = 146, Nombre = "Ushuaia", IdProvincia = provincias.First(p => p.Nombre == "Tierra del Fuego, Antártida e Islas del Atlántico Sur").IdProvincia },
                new Localidad { IdLocalidad = 147, Nombre = "Río Grande", IdProvincia = provincias.First(p => p.Nombre == "Tierra del Fuego, Antártida e Islas del Atlántico Sur").IdProvincia },
                new Localidad { IdLocalidad = 148, Nombre = "Tolhuin", IdProvincia = provincias.First(p => p.Nombre == "Tierra del Fuego, Antártida e Islas del Atlántico Sur").IdProvincia },
                new Localidad { IdLocalidad = 149, Nombre = "Puerto Almanza", IdProvincia = provincias.First(p => p.Nombre == "Tierra del Fuego, Antártida e Islas del Atlántico Sur").IdProvincia },
                new Localidad { IdLocalidad = 150, Nombre = "Estancia Harberton", IdProvincia = provincias.First(p => p.Nombre == "Tierra del Fuego, Antártida e Islas del Atlántico Sur").IdProvincia }
            );
            context.SaveChanges();
        }

        // 3. TiposProductos
        if (!context.TiposProductos.Any())
        {
            context.TiposProductos.AddRange(
                new TipoProducto { IdTipoProducto = 1, Descripcion = "Componentes" },
                new TipoProducto { IdTipoProducto = 2, Descripcion = "Monitores" },
                new TipoProducto { IdTipoProducto = 3, Descripcion = "Parlantes" },
                new TipoProducto { IdTipoProducto = 4, Descripcion = "Teclados" },
                new TipoProducto { IdTipoProducto = 5, Descripcion = "Mouse" }
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
                },
                new Proveedor
                {
                    IdProveedor = 3,
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
                new Producto { IdProducto = 1, Nombre = "MotherBoard Ryzen 5.0", Descripcion = "Mother Asus", Stock = 150, IdTipoProducto = tipos.First(t => t.Descripcion == "Componentes").IdTipoProducto },
                new Producto { IdProducto = 2, Nombre = "Monitor Curvo TLC", Descripcion = "Monitor Curvo 20°", Stock = 200, IdTipoProducto = tipos.First(t => t.Descripcion == "Monitores").IdTipoProducto },
                new Producto { IdProducto = 3, Nombre = "Parlante Huge HBL", Descripcion = "Sonido Envolvente", Stock = 100, IdTipoProducto = tipos.First(t => t.Descripcion == "Parlantes").IdTipoProducto },
                new Producto { IdProducto = 4, Nombre = "Teclado Mecánico RGB", Descripcion = "Teclado gaming mecánico", Stock = 80, IdTipoProducto = tipos.First(t => t.Descripcion == "Teclados").IdTipoProducto },
                new Producto { IdProducto = 5, Nombre = "Mouse Inalámbrico", Descripcion = "Mouse ergonómico inalámbrico", Stock = 120, IdTipoProducto = tipos.First(t => t.Descripcion == "Mouse").IdTipoProducto }
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

        // 7. Personas (con contraseña hasheada) - Más usuarios agregados
        if (!context.Personas.Any())
        {
            var locs = context.Localidades.ToList();
            context.Personas.AddRange(
            // Administradores
            new Persona
            {
                IdPersona = 1,
                NombreCompleto = "Martin Ratti",
                Dni = 12345678,
                Email = "marto@empresa.com",
                Password = BCrypt.Net.BCrypt.HashPassword("admin"),
                Telefono = "34115559101",
                Direccion = "Falsa 123",
                IdLocalidad = locs.First(l => l.Nombre == "Rosario").IdLocalidad,
                FechaIngreso = new DateOnly(2020, 1, 15)
            },

            new Persona
            {
                IdPersona = 2,
                NombreCompleto = "Frank Fabra",
                Dni = 41111111,
                Email = "fabra@email.com",
                Password = BCrypt.Net.BCrypt.HashPassword("admin"),
                Telefono = "3411111111",
                Direccion = "Verdadera 456",
                IdLocalidad = locs.First(l => l.Nombre == "Santa Fe").IdLocalidad,
                FechaIngreso = new DateOnly(2021, 3, 20)
            },

                // Empleados
                new Persona
                {
                    IdPersona = 3,
                    NombreCompleto = "Joaquin Peralta",
                    Dni = 30789123,
                    Email = "joaquin@empresa.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("joaquin"),
                    Telefono = "115550202",
                    Direccion = "Avenida Imaginaria 2",
                    IdLocalidad = locs.First(l => l.Nombre == "La Plata").IdLocalidad,
                    FechaIngreso = new DateOnly(2022, 5, 10)
                },
                new Persona
                {
                    IdPersona = 4,
                    NombreCompleto = "Ayrton Costa",
                    Dni = 42333444,
                    Email = "ayrton@email.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("ayrton"),
                    Telefono = "3415552222",
                    Direccion = "Calle Demo 4",
                    IdLocalidad = locs.First(l => l.Nombre == "Córdoba Capital").IdLocalidad,
                    FechaIngreso = new DateOnly(2023, 2, 15)
                },
                new Persona
                {
                    IdPersona = 5,
                    NombreCompleto = "Luka Doncic",
                    Dni = 42553400,
                    Email = "luka@email.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("luka"),
                    Telefono = "3415882922",
                    Direccion = "Calle Prueba 5",
                    IdLocalidad = locs.First(l => l.Nombre == "Mendoza").IdLocalidad,
                    FechaIngreso = new DateOnly(2022, 8, 30)
                },
                new Persona
                {
                    IdPersona = 6,
                    NombreCompleto = "Stephen Curry",
                    Dni = 32393404,
                    Email = "curry@email.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("stephen"),
                    Telefono = "3415559202",
                    Direccion = "Calle Test 6",
                    IdLocalidad = locs.First(l => l.Nombre == "Bariloche").IdLocalidad,
                    FechaIngreso = new DateOnly(2021, 11, 5)
                },

                // Usuarios adicionales
                new Persona
                {
                    IdPersona = 7,
                    NombreCompleto = "Maria Gonzalez",
                    Dni = 35456789,
                    Email = "maria.g@email.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("maria"),
                    Telefono = "3416667777",
                    Direccion = "Av. Central 789",
                    IdLocalidad = locs.First(l => l.Nombre == "Rosario").IdLocalidad,
                    FechaIngreso = new DateOnly(2023, 6, 1)
                },
                new Persona
                {
                    IdPersona = 8,
                    NombreCompleto = "Carlos Lopez",
                    Dni = 28765432,
                    Email = "carlos.l@email.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("carlos"),
                    Telefono = "3417778888",
                    Direccion = "Calle Secundaria 321",
                    IdLocalidad = locs.First(l => l.Nombre == "Santa Fe").IdLocalidad,
                    FechaIngreso = new DateOnly(2022, 12, 10)
                },
                new Persona
                {
                    IdPersona = 9,
                    NombreCompleto = "Ana Martinez",
                    Dni = 39876543,
                    Email = "ana.m@email.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("ana"),
                    Telefono = "3418889999",
                    Direccion = "Pasaje Privado 654",
                    IdLocalidad = locs.First(l => l.Nombre == "Córdoba Capital").IdLocalidad,
                    FechaIngreso = new DateOnly(2023, 3, 25)
                },
                new Persona
                {
                    IdPersona = 10,
                    NombreCompleto = "Pedro Rodriguez",
                    Dni = 40987654,
                    Email = "pedro.r@email.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("pedro"),
                    Telefono = "3419990000",
                    Direccion = "Boulevard Principal 987",
                    IdLocalidad = locs.First(l => l.Nombre == "Mendoza").IdLocalidad,
                    FechaIngreso = new DateOnly(2022, 7, 15)
                }
            );
            context.SaveChanges();
        }

        // 8. Ventas de ejemplo
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