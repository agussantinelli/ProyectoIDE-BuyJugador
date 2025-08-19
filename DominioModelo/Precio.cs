namespace DominioModelo
{
    public class Precio
    {
        public int IdPrecio { get; set; }
        public double Monto { get; set; }
        public int IdProducto { get; set; }

        public Precio(int idPrecio, double monto, int idProducto)
        {
            IdPrecio = idPrecio;
            Monto = monto;
            IdProducto = idProducto;
        }
    }
}
