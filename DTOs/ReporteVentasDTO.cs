using System;

namespace DTOs
{

    public class ReporteVentasDTO
    {
        public int IdVenta { get; set; }

        public DateTime Fecha { get; set; }

        public string NombreVendedor { get; set; }

        public decimal TotalVenta { get; set; }

        public string Estado { get; set; }
    }
}
