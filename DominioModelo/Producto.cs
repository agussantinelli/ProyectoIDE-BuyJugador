namespace DominioModelo
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public int IdTipoProducto { get; set; }

        public Producto(int idProducto, string nombre, string descripcion, int stock, int idTipoProducto)
        {
            IdProducto = idProducto;
            Nombre = nombre;
            Descripcion = descripcion;
            Stock = stock;
            IdTipoProducto = idTipoProducto;
        }
    }
}
