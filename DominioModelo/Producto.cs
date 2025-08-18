namespace DominioModelo
{
    public class Producto
    {
        public int IdProducto { get; private set; }
        public string NombreProducto { get; private set; }
        public string DescripcionProducto { get; private set; }
        public int Stock { get; private set; }
        public int IdTipoProducto { get; private set; }

        public Producto(int idProducto, string nombreProducto, string descripcionProducto, int stock, int idTipoProducto)
        {
            SetIdProducto(idProducto);
            SetNombreProducto(nombreProducto);
            SetDescripcionProducto(descripcionProducto);
            SetStock(stock);
            SetIdTipoProducto(idTipoProducto);
        }

        public void SetIdProducto(int idProducto)
        {
            if (idProducto <= 0)
                throw new ArgumentException("El ID de producto debe ser positivo.", nameof(idProducto));
            IdProducto = idProducto;
        }

        public void SetNombreProducto(string nombreProducto)
        {
            if (string.IsNullOrWhiteSpace(nombreProducto))
                throw new ArgumentException("El nombre del producto no puede ser nulo o vacío.", nameof(nombreProducto));
            NombreProducto = nombreProducto;
        }

        public void SetDescripcionProducto(string descripcionProducto)
        {
            if (string.IsNullOrWhiteSpace(descripcionProducto))
                throw new ArgumentException("Las características no pueden ser nulas o vacías.", nameof(descripcionProducto));
            DescripcionProducto = descripcionProducto;
        }

        public void SetStock(int stock)
        {
            if (stock < 0)
                throw new ArgumentException("El stock no puede ser negativo.", nameof(stock));
            Stock = stock;
        }

        public void SetIdTipoProducto(int idTipoProducto)
        {
            if (idTipoProducto <= 0)
                throw new ArgumentException("El ID de tipo de producto debe ser positivo.", nameof(idTipoProducto));
            IdTipoProducto = idTipoProducto;
        }
    }
}
