using Dominio_Modelo;
using Data;
using System.Collections.Generic;
using System.Linq;

namespace Dominio_Servicios
{
    public class LineaVentaService
    {
        public void Add(LineaVenta lineaVenta)
        {
            lineaVenta.SetNroLineaVenta(GetNextCodigo(lineaVenta.IdVenta));
            LineaVentaInMemory.LineasVenta.Add(lineaVenta);
        }

        public bool Delete(int codigoVenta, decimal nroLineaVenta)
        {
            var lv = LineaVentaInMemory.LineasVenta
                .Find(x => x.IdVenta == codigoVenta && x.NroLineaVenta == nroLineaVenta);

            if (lv != null)
            {
                LineaVentaInMemory.LineasVenta.Remove(lv);
                return true;
            }

            return false;
        }

        public LineaVenta? Get(int codigoVenta, decimal nroLineaVenta)
        {
            return LineaVentaInMemory.LineasVenta
                .Find(x => x.IdVenta == codigoVenta && x.NroLineaVenta == nroLineaVenta);
        }

        public IEnumerable<LineaVenta> GetAll()
        {
            return LineaVentaInMemory.LineasVenta.ToList();
        }

        public bool Update(LineaVenta lineaVenta)
        {
            var existing = LineaVentaInMemory.LineasVenta
                .Find(x => x.IdVenta == lineaVenta.IdVenta &&
                           x.NroLineaVenta == lineaVenta.NroLineaVenta);

            if (existing != null)
            {
                existing.SetIdProducto(lineaVenta.IdProducto);
                existing.SetCantidadVenta(lineaVenta.CantidadVenta);
                return true;
            }

            return false;
        }

        private static decimal GetNextCodigo(int codigoVenta)
        {
            var lineas = LineaVentaInMemory.LineasVenta
                .Where(x => x.IdVenta == codigoVenta)
                .ToList();

            return lineas.Any() ? lineas.Max(x => x.NroLineaVenta) + 1 : 1;
        }
    }
}

