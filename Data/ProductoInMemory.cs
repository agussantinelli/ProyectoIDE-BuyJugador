using System;
using System.Collections.Generic;
using Dominio_Modelo;

namespace Data
{
    public class ProductoInMemory
    {
        public static List<Producto> Productos;

        static ProductoInMemory()
        {
            Productos = new List<Producto>
            {
                new Producto(100, "MotherBoard Ryzen 5.0", "MotherBoard Ryzen 5.0 Mother Asus", 150, 1),
                new Producto(200, "Monitor Curvo TLC", "Monitor Curvo 20°", 200, 2),
                new Producto(201, "Monitor Samsung", "Monitor HD Open-View", 180, 2),
                new Producto(300, "Parlante Huge HBL", "Parlante Sonido Envolvente", 120, 3)
            };
        }
    }
}