namespace DTOs
{
    public class LineaVentaDTO
    {
        public int Cantidad { get; set; }
        public int IdVenta { get; set; }
        public int? IdProducto { get; set; }
        public int NroLineaVenta { get; set; }
        public string NombreProducto { get; set; } = "N/A";
        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal => Cantidad * PrecioUnitario;
        public bool EsNueva { get; set; } = false;

        public static LineaVentaDTO? FromDominio(DominioModelo.LineaVenta entidad)
        {
            if (entidad == null) return null;

            return new LineaVentaDTO
            {
                Cantidad = entidad.Cantidad,
                IdVenta = entidad.IdVenta,
                IdProducto = entidad.IdProducto,
                NroLineaVenta = entidad.NroLineaVenta,
                PrecioUnitario = entidad.PrecioUnitario,
                NombreProducto = entidad.IdProductoNavigation?.Nombre ?? "Producto no encontrado"
            };
        }

        public DominioModelo.LineaVenta ToDominio()
        {
            return new DominioModelo.LineaVenta
            {
                Cantidad = this.Cantidad,
                IdVenta = this.IdVenta,
                IdProducto = this.IdProducto,
                NroLineaVenta = this.NroLineaVenta,
                PrecioUnitario = this.PrecioUnitario
            };
        }
    }
}

