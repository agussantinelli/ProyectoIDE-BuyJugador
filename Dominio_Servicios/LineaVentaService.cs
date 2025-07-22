using Dominio_Modelo;
using Data;

namespace Dominio_Servicios
{
    public class LineaVentaService
    {
        public void Add(LineaVenta lineaVenta)
        {
            lineaVenta.SetNroLineaVenta(GetNextCodigo(lineaVenta.Venta.CodigoVenta));
            LineaVentaInMemory.LineasVenta.Add(lineaVenta);
        }

        public bool Delete(int codigoVenta, int NroLineaVenta)
        {
            var lv = LineaVentaInMemory.LineasVenta
                .Find(x => x.Venta.IdVenta == codigoVenta && x.NroLineaVenta == NroLineaVenta);

            if (lv != null)
            {
                LineaVentaInMemory.LineasVenta.Remove(lv);
                return true;
            }

            return false;
        }

        public LineaVenta? Get(int codigoVenta, int NroLineaVenta)
        {
            return LineaVentaInMemory.LineasVenta
                .Find(x => x.Venta.CodigoVenta == codigoVenta && x.NroLineaVenta == NroLineaVenta);
        }

        public IEnumerable<LineaVenta> GetAll()
        {
            return LineaVentaInMemory.LineasVenta.ToList();
        }

        public bool Update(LineaVenta lineaVenta)
        {
            var existing = LineaVentaInMemory.LineasVenta
                .Find(x => x.Venta.CodigoVenta == lineaVenta.Venta.CodigoVenta &&
                           x.NroLineaVenta == lineaVenta.NroLineaVenta);

            if (existing != null)
            {
                existing.SetProducto(lineaVenta.Producto);
                existing.SetCantidad(lineaVenta.Cantidad);
                return true;
            }

            return false;
        }

        private static int GetNextCodigo(int codigoVenta)
        {
            var lineas = LineaVentaInMemory.LineasVenta
                .Where(x => x.Venta.CodigoVenta == codigoVenta)
                .ToList();

            return lineas.Any() ? lineas.Max(x => x.NroLineaVenta) + 1 : 1;
        }
    }
}
