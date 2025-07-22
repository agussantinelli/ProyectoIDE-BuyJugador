namespace DTOs
{
    public class Pedido
    {
        public int IdPedido { get; private set; }
        public DateTime FechaPedido { get; private set; }
        public string EstadoPedido { get; private set; }
        public decimal MontoTotalPedido { get; private set; }
        public string CuilProveedor { get; private set; }
    }
}
