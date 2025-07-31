using System;
using System.Collections.Generic;
using DominioModelo;

namespace Data
{
    public class ProveedorInMemory
    {
        public static List<Proveedor> Proveedores;

        static ProveedorInMemory()
        {
            Proveedores = new List<Proveedor>
            {
                new Proveedor("30-12345678-9", "Distrito Digital S.A.", "3416667788", "compras@distritodigital.com"),
                new Proveedor("30-98765432-1", "Logística Computacional S.R.L.", "116665544", "ventas@logisticacompsrl.com")
            };
        }
    }
}