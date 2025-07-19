namespace Dominio_Modelo
{
    public class Producto
    {
        public int IdProducto { get; private set; }
        public string Nombre { get; private set; }
        public string Caracteristicas { get; private set; }
        public int Stock { get; private set; }
        public int CodTipoProducto { get; private set; }

        public Producto(int idProducto, string nombre, string caracteristicas, int stock, int codTipoProducto)
        {
            SetIdProducto(idProducto);
            SetNombre(nombre);
            SetCaracteristicas(caracteristicas);
            SetStock(stock);
            SetCodTipoProducto(codTipoProducto);
        }

        public void SetIdProducto(int idProducto)
        {
            if (idProducto <= 0)
                throw new ArgumentException("El ID de producto debe ser positivo.", nameof(idProducto));
            IdProducto = idProducto;
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede ser nulo o vacío.", nameof(nombre));
            Nombre = nombre;
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

        public void SetCodTipoProducto(int codTipoProducto)
        {
            if (codTipoProducto <= 0)
                throw new ArgumentException("El código de tipo de producto debe ser positivo.", nameof(codTipoProducto));
            CodTipoProducto = codTipoProducto;
        }
    }
}
