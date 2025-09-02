namespace DTOs
{
    public class PrecioDTO
    {
        public decimal Monto { get; set; }
        public DateTime FechaDesde { get; set; }
        public int IdProducto { get; set; }

        public static PrecioDTO FromDominio(DominioModelo.Precio entidad)
        {
            if (entidad == null) return null;

            return new PrecioDTO
            {
                Monto = entidad.Monto,
                FechaDesde = entidad.FechaDesde,
                IdProducto = entidad.IdProducto
            };
        }

        public DominioModelo.Precio ToDominio()
        {
            return new DominioModelo.Precio
            {
                Monto = this.Monto,
                FechaDesde = this.FechaDesde,
                IdProducto = this.IdProducto
            };
        }
    }
}
