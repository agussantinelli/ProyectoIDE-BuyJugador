using DominioModelo;
using Data;
using System.Collections.Generic;
using System.Linq;

namespace DominioServicios
{
    public class ProveedorService
    {
        public bool Add(Proveedor proveedor)
        {
            if (ProveedorInMemory.Proveedores.Any(p => p.Cuil == proveedor.Cuil))
                return false;

            ProveedorInMemory.Proveedores.Add(proveedor);
            return true;
        }

        public bool Delete(string cuil)
        {
            var obj = ProveedorInMemory.Proveedores.FirstOrDefault(x => x.Cuil == cuil);
            if (obj != null)
            {
                ProveedorInMemory.Proveedores.Remove(obj);
                return true;
            }
            return false;
        }

        public Proveedor? Get(string cuil)
        {
            return ProveedorInMemory.Proveedores.FirstOrDefault(x => x.Cuil == cuil);
        }

        public IReadOnlyList<Proveedor> GetAll()
        {
            return ProveedorInMemory.Proveedores.AsReadOnly();
        }

        public bool Update(Proveedor proveedor)
        {
            var obj = ProveedorInMemory.Proveedores.FirstOrDefault(x => x.Cuil == proveedor.Cuil);
            if (obj != null)
            {
                obj.SetRazonSocial(proveedor.RazonSocial);
                obj.SetTelefonoProveedor(proveedor.TelefonoProveedor);
                obj.SetMailProveedor(proveedor.MailProveedor);
                return true;
            }
            return false;
        }
    }
}
