using System;
using System.Collections.Generic;
using Dominio_Modelo;


namespace Data
{

    public class DuenioInMemory
    {
        public static List<Duenio> Duenios;

        static DuenioInMemory()
        {
            Duenios = new List<Duenio>
            {
                new Duenio(25123456, "Martin Ratti", "marto666@empresa.com", "pass123", "3415550101"),
                new Duenio(30789123, "Joaquin Peralta", "rojopasion@empresa.com", "pass456", "115550202")
            };
        }
    }
}