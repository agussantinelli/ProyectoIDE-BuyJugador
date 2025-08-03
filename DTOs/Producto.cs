namespace DTOs
{
    public class ProductoDto
    {
        public int IdProducto { get; private set; }
        public string NombreProducto { get; private set; }
        public string DescripcionProducto { get; private set; }
        public int Stock { get; private set; }
        public int IdTipoProducto { get; private set; }
    }
}
