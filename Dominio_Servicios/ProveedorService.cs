
using Dominio_Modelo;
using Data;

namespace Dominio_Servicios
{
    public class ProveedorService
    {
        public void Add(Proveedor proveedor)
        { 
            ProveedorInMemory.Proveedores.Add(proveedor);
        }

        public bool Delete(string cuil)
        {
            var obj = ProveedorInMemory.Proveedores.Find(x => x.Cuil == cuil);
            if (obj != null)
            {
                ProveedorInMemory.Proveedores.Remove(obj);
                return true;
            }
            return false;
        }

        public Proveedor? Get(string cuil)
        {
            return ProveedorInMemory.Proveedores.Find(x => x.Cuil == cuil);
        }

        public IEnumerable<Proveedor> GetAll()
        {
            return ProveedorInMemory.Proveedores.ToList();
        }

        public bool Update(Proveedor proveedor)
        {
            var obj = ProveedorInMemory.Proveedores.Find(x => x.Cuil == proveedor.Cuil);
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
