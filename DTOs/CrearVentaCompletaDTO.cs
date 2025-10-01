using System.Collections.Generic;

namespace DTOs
{
    /// <summary>
    /// DTO para encapsular toda la información necesaria para crear una venta completa en una sola llamada.
    /// </summary>
    public class CrearVentaCompletaDTO
    {
        public int? IdPersona { get; set; }
        public List<LineaVentaDTO> Lineas { get; set; } = new List<LineaVentaDTO>();
    }
}
