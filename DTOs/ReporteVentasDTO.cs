using System;

namespace DTOs
{
    // #NUEVO: DTO específico para los datos del reporte.
    // #Intención: Desacoplar la estructura del reporte de los DTOs existentes.
    public class ReporteVentasDTO
    {
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        public string NombreVendedor { get; set; }
        public decimal TotalVenta { get; set; }
        public string Estado { get; set; }
    }
}
