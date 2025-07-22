using System;
using System.Collections.Generic;
using Dominio_Modelo;

namespace Data
{
    public class PrecioInMemory
    {
        public static List<Precio> Precios;

        static PrecioInMemory()
        {
            Precios = new List<Precio>
            {
                new Precio(100, 1500.00m, new DateTime(2024, 1, 1)),
                new Precio(100, 1750.50m, new DateTime(2024, 6, 15)),
                new Precio(200, 800.00m, new DateTime(2024, 3, 1)),
                new Precio(201, 1200.75m, new DateTime(2024, 5, 20)),
                new Precio(300, 950.00m, new DateTime(2024, 2, 10))
            };
        }
    }

}