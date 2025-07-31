using DominioModelo;
using Data;
using System.Collections.Generic;
using System.Linq;

namespace DominioServicios
{
    public class VentaService
    {
        public bool Add(Venta venta)
        {
            venta.SetIdVenta(GetNextId());
            VentaInMemory.Ventas.Add(venta);
            return true;
        }

        public bool Delete(int idVenta)
        {
            var v = VentaInMemory.Ventas.FirstOrDefault(x => x.IdVenta == idVenta);
            if (v != null)
            {
                VentaInMemory.Ventas.Remove(v);
                return true;
            }
            return false;
        }

        public Venta? Get(int idVenta)
        {
            return VentaInMemory.Ventas.FirstOrDefault(x => x.IdVenta == idVenta);
        }

        public IEnumerable<Venta> GetAll()
        {
            return VentaInMemory.Ventas.ToList();
        }

        public bool Update(Venta venta)
        {
            var existing = VentaInMemory.Ventas.FirstOrDefault(x => x.IdVenta == venta.IdVenta);

            if (existing != null)
            {
                existing.SetFechaVenta(venta.FechaVenta);
                existing.SetEstadoVenta(venta.EstadoVenta);
                existing.SetMontoTotalVenta(venta.MontoTotalVenta);
                existing.SetDniVendedor(venta.DniVendedor);
                return true;
            }

            return false;
        }

        private static int GetNextId()
        {
            return VentaInMemory.Ventas.Any() ? VentaInMemory.Ventas.Max(x => x.IdVenta) + 1 : 1;
        }
    }
}


