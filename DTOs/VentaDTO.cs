namespace DTOs
{
    public class VentaDTO
    {
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public int? IdPersona { get; set; }

        // Propiedad para la UI, no viene de la API.
        public string NombreVendedor { get; set; } = string.Empty;
        public decimal Total { get; set; } // Se calculará en el cliente

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


