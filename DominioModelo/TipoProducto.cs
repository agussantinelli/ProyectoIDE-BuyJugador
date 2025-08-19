namespace DominioModelo
{
    public class TipoProducto
    {
        public int IdTipoProducto { get; set; }
        public string Descripcion { get; set; }

        public TipoProducto(int idTipoProducto, string descripcion)
        {
            IdTipoProducto = idTipoProducto;
            Descripcion = descripcion;
        }
    }
}
