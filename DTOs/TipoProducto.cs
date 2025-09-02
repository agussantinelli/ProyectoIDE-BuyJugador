namespace DTOs
{
    public class TipoProductoDTO
    {
        public int IdTipoProducto { get; set; }
        public string Descripcion { get; set; }

        public static TipoProductoDTO FromDominio(DominioModelo.TipoProducto entidad)
        {
            if (entidad == null) return null;

            return new TipoProductoDTO
            {
                IdTipoProducto = entidad.IdTipoProducto,
                Descripcion = entidad.Descripcion
            };
        }

        public DominioModelo.TipoProducto ToDominio()
        {
            return new DominioModelo.TipoProducto
            {
                IdTipoProducto = this.IdTipoProducto,
                Descripcion = this.Descripcion
            };
        }
    }
}
