namespace DTOs
{
    public class PedidoDTO
    {
        public int IdPedido { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public int? IdProveedor { get; set; }

        public static PedidoDTO FromDominio(DominioModelo.Pedido entidad)
        {
            if (entidad == null) return null;

            return new PedidoDTO
            {
                IdPedido = entidad.IdPedido,
                Fecha = entidad.Fecha,
                Estado = entidad.Estado,
                IdProveedor = entidad.IdProveedor
            };
        }

        public DominioModelo.Pedido ToDominio()
        {
            return new DominioModelo.Pedido
            {
                IdPedido = this.IdPedido,
                Fecha = this.Fecha,
                Estado = this.Estado,
                IdProveedor = this.IdProveedor
            };
        }
    }
}
