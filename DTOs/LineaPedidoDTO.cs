namespace DTOs
{
    public class LineaPedidoDTO
    {
        public int Cantidad { get; set; }
        public int IdPedido { get; set; }
        public int? IdProducto { get; set; }
        public int NroLineaPedido { get; set; }

        public static LineaPedidoDTO FromDominio(DominioModelo.LineaPedido entidad)
        {
            if (entidad == null) return null;

            return new LineaPedidoDTO
            {
                Cantidad = entidad.Cantidad,
                IdPedido = entidad.IdPedido,
                IdProducto = entidad.IdProducto,
                NroLineaPedido = entidad.NroLineaPedido
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

