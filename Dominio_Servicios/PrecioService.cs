using DominioModelo;
using Data;
using System.Collections.Generic;
using System.Linq;

namespace DominioServicios
{
    public class PrecioService
    {
        public void Add(Precio precio)
        {
            PrecioInMemory.Precios.Add(precio);
        }

        public bool Delete(int idProducto, DateTime fechaDesde)
        {
            var p = PrecioInMemory.Precios
                .Find(x => x.IdProducto == idProducto && x.FechaDesde == fechaDesde);

            if (p != null)
            {
                PrecioInMemory.Precios.Remove(p);
                return true;
            }

            return false;
        }

        public Precio? Get(int idProducto, DateTime fechaDesde)
        {
            return PrecioInMemory.Precios
                .Find(x => x.IdProducto == idProducto && x.FechaDesde == fechaDesde);
        }

        public IEnumerable<Precio> GetAll()
        {
            return PrecioInMemory.Precios.ToList();
        }

        public bool Update(Precio precio)
        {
            var existing = PrecioInMemory.Precios
                .Find(x => x.IdProducto == precio.IdProducto && x.FechaDesde == precio.FechaDesde);

            if (existing != null)
            {
                existing.SetMonto(precio.Monto);
                return true;
            }

            return false;
        }
    }
}
