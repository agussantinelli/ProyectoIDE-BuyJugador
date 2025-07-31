namespace DTOs
{
    public class TipoProducto
    {
        public int IdTipoProducto { get; private set; }
        public string NombreTipoProducto { get; private set; }

        public TipoProducto(int idTipoProducto, string nombreTipoProducto)
        {
            IdTipoProducto = idTipoProducto;
            NombreTipoProducto = nombreTipoProducto;
        }
    }
}