
using DominioModelo;
using Data;

namespace DominioServicios
{
    public class LineaPedidoService
    {
        public void Add(LineaPedido lineaPedido)
        {
            decimal nextLinea = GetNextNroLinea(lineaPedido.IdPedido);
            lineaPedido.SetNroLineaPedido(nextLinea);
            LineaPedidoInMemory.LineasPedido.Add(lineaPedido);
        }

        public bool Delete(int idPedido, decimal nroLinea)
        {
            LineaPedido? lineaToDelete = LineaPedidoInMemory.LineasPedido.Find(x => x.IdPedido == idPedido && x.NroLineaPedido == nroLinea);
            if (lineaToDelete != null)
            {
                LineaPedidoInMemory.LineasPedido.Remove(lineaToDelete);
                return true;
            }
            return false;
        }

        public LineaPedido? Get(int idPedido, decimal nroLinea)
        {
            return LineaPedidoInMemory.LineasPedido.Find(x => x.IdPedido == idPedido && x.NroLineaPedido == nroLinea);
        }

        public IEnumerable<LineaPedido> GetAll()
        {
            return LineaPedidoInMemory.LineasPedido.ToList();
        }

        public bool Update(LineaPedido lineaPedido)
        {
            LineaPedido? lineaToUpdate = LineaPedidoInMemory.LineasPedido.Find(x => x.IdPedido == lineaPedido.IdPedido && x.NroLineaPedido == lineaPedido.NroLineaPedido);
            if (lineaToUpdate != null)
            {
                lineaToUpdate.SetIdProducto(lineaPedido.IdProducto);
                lineaToUpdate.SetCantidadPedido(lineaPedido.CantidadPedido);
                return true;
            }
            return false;
        }

        private static decimal GetNextNroLinea(int idPedido)
        {
            var lineas = LineaPedidoInMemory.LineasPedido.Where(x => x.IdPedido == idPedido);
            return lineas.Any() ? lineas.Max(x => x.NroLineaPedido) + 1 : 1;
        }
    }
}