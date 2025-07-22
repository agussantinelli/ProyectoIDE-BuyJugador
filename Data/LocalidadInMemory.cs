using System;
using System.Collections.Generic;
using Dominio_Modelo;


namespace Data
{
    public class LocalidadInMemory
    {
        public static List<Localidad> Localidades;

        static LocalidadInMemory()
        {
            Localidades = new List<Localidad>
            {
                new Localidad(101, "Rosario", 1),
                new Localidad(102, "Santa Fe", 1),
                new Localidad(103, "Funes", 1),
                new Localidad(104, "Villa Gobernador Galvez", 1),

                new Localidad(201, "Córdoba Capital", 2),
                new Localidad(202, "Villa Carlos Paz", 2),

                new Localidad(301, "La Plata", 3),
                new Localidad(302, "La Matanza", 3),
                new Localidad(303, "Florencio Varela", 3),
                new Localidad(304, "San Nicolas", 3)


            };
        }
    }

}