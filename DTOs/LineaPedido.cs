namespace DTOs
{
    internal class LineaPedidoDto
    {
        public int IdPedido { get; private set; }
        public decimal NroLineaPedido { get; private set; }
        public int IdProducto { get; private set; }
        public int CantidadPedido { get; private set; }
    }
}