namespace DominioModelo
{
    public class LineaVenta
    {
        public int IdLineaVenta { get; set; }
        public int Cantidad { get; set; }
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }

        public LineaVenta(int idLineaVenta, int cantidad, int idVenta, int idProducto)
        {
            IdLineaVenta = idLineaVenta;
            Cantidad = cantidad;
            IdVenta = idVenta;
            IdProducto = idProducto;
        }
    }
}
