namespace DTOs
{
    public class VentaDTO
    {
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public int? IdPersona { get; set; }

        public static VentaDTO FromDominio(DominioModelo.Venta entidad)
        {
            if (entidad == null) return null;

            return new VentaDTO
            {
                IdVenta = entidad.IdVenta,
                Fecha = entidad.Fecha,
                Estado = entidad.Estado,
                IdPersona = entidad.IdPersona
            };
        }

        public DominioModelo.Venta ToDominio()
        {
            return new DominioModelo.Venta
            {
                IdVenta = this.IdVenta,
                Fecha = this.Fecha,
                Estado = this.Estado,
                IdPersona = this.IdPersona
            };
        }
    }
}
