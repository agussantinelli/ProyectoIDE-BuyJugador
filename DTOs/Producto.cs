namespace DTOs
{
    public class ProductoDTO
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public int? IdTipoProducto { get; set; }

        public static ProductoDTO FromDominio(DominioModelo.Producto entidad)
        {
            if (entidad == null) return null;

            return new ProductoDTO
            {
                IdProducto = entidad.IdProducto,
                Nombre = entidad.Nombre,
                Descripcion = entidad.Descripcion,
                Stock = entidad.Stock,
                IdTipoProducto = entidad.IdTipoProducto
            };
        }

        public DominioModelo.Producto ToDominio()
        {
            return new DominioModelo.Producto
            {
                IdProducto = this.IdProducto,
                Nombre = this.Nombre,
                Descripcion = this.Descripcion,
                Stock = this.Stock,
                IdTipoProducto = this.IdTipoProducto
            };
        }
    }
}
