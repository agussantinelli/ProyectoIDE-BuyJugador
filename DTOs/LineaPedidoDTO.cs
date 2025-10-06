namespace DTOs
{
    public class LineaPedidoDTO
    {
        public int Cantidad { get; set; }
        public int IdPedido { get; set; }
        public int? IdProducto { get; set; }
        public int NroLineaPedido { get; set; }
        public string NombreProducto { get; set; } = "N/A";
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;

        public static LineaPedidoDTO FromDominio(DominioModelo.LineaPedido entidad)
        {
            if (entidad == null) return null;

            return new LineaPedidoDTO
            {
                Cantidad = entidad.Cantidad,
                IdPedido = entidad.IdPedido,
                IdProducto = entidad.IdProducto,
                NroLineaPedido = entidad.NroLineaPedido,
                NombreProducto = entidad.IdProductoNavigation?.Nombre ?? "N/A"
            };
        }

        public DominioModelo.LineaPedido ToDominio()
        {
            return new DominioModelo.LineaPedido
            {
                Cantidad = this.Cantidad,
                IdPedido = this.IdPedido,
                IdProducto = this.IdProducto,
                NroLineaPedido = this.NroLineaPedido
            };
        }
    }
}

