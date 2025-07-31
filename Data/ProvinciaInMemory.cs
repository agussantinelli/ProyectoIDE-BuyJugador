using System;
using System.Collections.Generic;
using DominioModelo;

namespace Data
{
    public class ProvinciaInMemory
    {
        public static List<Provincia> Provincias;

        static ProvinciaInMemory()
        {
            Provincias = new List<Provincia>
            {
                new Provincia(1, "Santa Fe"),
                new Provincia(2, "Córdoba"),
                new Provincia(3, "Buenos Aires")
            };
        }
    }
}
