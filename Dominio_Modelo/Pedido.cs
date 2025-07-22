using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio_Modelo
{
    public class Pedido
    {
        public int IdPedido { get; private set; }
        public DateTime FechaPedido { get; private set; }
        public string EstadoPedido { get; private set; }
        public decimal MontoTotalPedido { get; private set; }
        public string CuilProveedor { get; private set; }


        public Pedido(int idPedido, DateTime fechaPedido, string estadoPedido, decimal montoTotalPedido, string cuilProveedor)
        {
            SetIdPedido(idPedido);
            SetFechaPedido(fechaPedido);
            SetEstadoPedido(estadoPedido);
            SetMontoTotalPedido(montoTotalPedido);
            SetCuilProveedor(cuilProveedor);

        }

        public void SetIdPedido(int idPedido)
        {
            if (idPedido <= 0)
                throw new ArgumentException("El IdPedido debe ser mayor a cero.", nameof(idPedido));
            IdPedido = idPedido;
        }

        public void SetFechaPedido(DateTime fechaPedido)
        {
            if (fechaPedido == default)
                throw new ArgumentException("La fecha de pedido no puede ser vacía.", nameof(fechaPedido));
            FechaPedido = fechaPedido;
        }

        public void SetEstadoPedido(string estadoPedido)
        {
            if (string.IsNullOrWhiteSpace(estadoPedido))
                throw new ArgumentException("El estado del pedido no puede ser nulo o vacío.", nameof(estadoPedido));
            EstadoPedido = estadoPedido;
        }

        public void SetMontoTotalPedido(decimal montoTotalPedido)
        {
            if (montoTotalPedido < 0)
                throw new ArgumentException("El monto total del pedido no puede ser negativo.", nameof(montoTotalPedido));
            MontoTotalPedido = montoTotalPedido;
        }

        public void SetCuilProveedor(string cuil)
        {
            if (string.IsNullOrWhiteSpace(cuil))
                throw new ArgumentException("El CUIT no puede ser nulo o vacío.", nameof(cuil));
            CuilProveedor = cuil;
        }

    }
}
