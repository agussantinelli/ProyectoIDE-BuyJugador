namespace DTOs
{
    public class LineaPedidoDTO
    {
        public int IdPedido { get; set; }
        public int NroLineaPedido { get; set; }
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; } = "N/A";
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;

        public static LineaPedidoDTO FromDominio(DominioModelo.LineaPedido entidad)
        {
            if (entidad == null) return null;


            return new LineaPedidoDTO
            {
                IdPedido = entidad.IdPedido,
                NroLineaPedido = entidad.NroLineaPedido,
                IdProducto = entidad.IdProducto,
                Cantidad = entidad.Cantidad,
                PrecioUnitario = entidad.PrecioUnitario,
                NombreProducto = entidad.IdProductoNavigation?.Nombre ?? "N/A"
            };
        }

        public DominioModelo.LineaPedido ToDominio()
        {
            return new DominioModelo.LineaPedido
            {
                IdPedido = this.IdPedido,
                NroLineaPedido = this.NroLineaPedido,
                IdProducto = this.IdProducto,
                Cantidad = this.Cantidad,
                PrecioUnitario = this.PrecioUnitario
            };
        }

    }
}


