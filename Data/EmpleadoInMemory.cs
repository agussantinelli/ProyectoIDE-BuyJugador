using System;
using System.Collections.Generic;
using Dominio_Modelo;


namespace Data
{
    public class EmpleadoInMemory
    {
        public static List<Empleado> Empleados;

        static EmpleadoInMemory()
        {
            Empleados = new List<Empleado>
            {
                new Empleado(40111222, "Frank Fabra", "frank@email.com", "empleado1", "3415551111", new DateTime(2023, 5, 15)),
                new Empleado(42333444, "Ayrton Costa", "ayrton@email.com", "empleado2", "3415552222", new DateTime(2024, 1, 20)),
                new Empleado(42553400, "Luka Doncic", "doncic@email.com", "empleado3", "3415882922", new DateTime(2014, 3, 6)),
                new Empleado(32393404, "Stephen Curry", "curry@email.com", "empleado4", "3415559202", new DateTime(2009, 1, 7))

            };
        }
    }
}