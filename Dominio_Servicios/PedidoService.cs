using DominioModelo;
using Data;
using System.Collections.Generic;
using System.Linq;

namespace DominioServicios
{
    public class PedidoService
    {
        public void Add(Pedido pedido)
        {
            pedido.SetIdPedido(GetNextCodigo());
            PedidoInMemory.Pedidos.Add(pedido);
        }

        public bool Delete(int idPedido)
        {
            var p = PedidoInMemory.Pedidos.Find(x => x.IdPedido == idPedido);

            if (p != null)
            {
                PedidoInMemory.Pedidos.Remove(p);
                return true;
            }

            return false;
        }

        public Pedido? Get(int idPedido)
        {
            return PedidoInMemory.Pedidos.Find(x => x.IdPedido == idPedido);
        }

        public IEnumerable<Pedido> GetAll()
        {
            return PedidoInMemory.Pedidos.ToList();
        }

        public bool Update(Pedido pedido)
        {
            var existing = PedidoInMemory.Pedidos.Find(x => x.IdPedido == pedido.IdPedido);

            if (existing != null)
            {
                existing.SetCuilProveedor(pedido.CuilProveedor);
                existing.SetFechaPedido(pedido.FechaPedido);
                existing.SetEstadoPedido(pedido.EstadoPedido);
                existing.SetMontoTotalPedido(pedido.MontoTotalPedido);
                return true;
            }

            return false;
        }

        private static int GetNextCodigo()
        {
            return PedidoInMemory.Pedidos.Any()
                ? PedidoInMemory.Pedidos.Max(x => x.IdPedido) + 1
                : 1;
        }
    }
}