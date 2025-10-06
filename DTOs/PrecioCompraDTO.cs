namespace DTOs
{
    public class PrecioCompraDTO
    {
        public int IdProducto { get; set; }
        public int IdProveedor { get; set; }
        public decimal Monto { get; set; }

        public static PrecioCompraDTO FromDominio(DominioModelo.PrecioCompra e)
        {
            if (e == null) return null;
            return new PrecioCompraDTO
            {
                IdProducto = e.IdProducto,
                IdProveedor = e.IdProveedor,
                Monto = e.Monto
            };
        }

        public DominioModelo.PrecioCompra ToDominio()
        {
            return new DominioModelo.PrecioCompra
            {
                IdProducto = IdProducto,
                IdProveedor = IdProveedor,
                Monto = Monto
            };
        }
    }
}
