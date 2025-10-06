using System.Linq;

namespace DTOs
{
    public class ProductoDTO
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int Stock { get; set; }
        public int? IdTipoProducto { get; set; }
        public string? TipoProductoDescripcion { get; set; }
        public decimal? PrecioActual { get; set; }

        public static ProductoDTO? FromDominio(DominioModelo.Producto p)
        {
            if (p == null) return null;

            var precioActual = p.PreciosVenta?
                .OrderByDescending(pr => pr.FechaDesde)
                .FirstOrDefault()?.Monto;

            return new ProductoDTO
            {
                IdProducto = p.IdProducto,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Stock = p.Stock,
                IdTipoProducto = p.IdTipoProducto,
                TipoProductoDescripcion = p.IdTipoProductoNavigation?.Descripcion,
                PrecioActual = precioActual
            };
        }

        public DominioModelo.Producto ToDominio()
        {
            return new DominioModelo.Producto
            {
                IdProducto = IdProducto,
                Nombre = Nombre,
                Descripcion = Descripcion,
                Stock = Stock,
                IdTipoProducto = IdTipoProducto ?? null,
                Activo = true
            };
        }
    }
}

