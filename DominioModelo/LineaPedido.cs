namespace DominioModelo
{
    public class LineaPedido
    {
        public int IdLineaPedido { get; set; }
        public int Cantidad { get; set; }
        public int IdPedido { get; set; }
        public int IdProducto { get; set; }

        public LineaPedido(int idLineaPedido, int cantidad, int idPedido, int idProducto)
        {
            IdLineaPedido = idLineaPedido;
            Cantidad = cantidad;
            IdPedido = idPedido;
            IdProducto = idProducto;
        }
    }
}
