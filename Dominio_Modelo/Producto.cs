namespace Dominio_Modelo
{
    public class Producto
    {
        public int IdProducto { get; private set; }
        public string NombreProducto { get; private set; }
        public string Caracteristicas { get; private set; }
        public int Stock { get; private set; }
        public int IdTipoProducto { get; private set; }

        public Producto(int idProducto, string nombreProducto, string caracteristicas, int stock, int idTipoProducto)
        {
            SetIdProducto(idProducto);
            SetNombreProducto(nombreProducto);
            SetCaracteristicas(caracteristicas);
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

        public void SetCaracteristicas(string caracteristicas)
        {
            if (string.IsNullOrWhiteSpace(caracteristicas))
                throw new ArgumentException("Las características no pueden ser nulas o vacías.", nameof(caracteristicas));
            Caracteristicas = caracteristicas;
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
