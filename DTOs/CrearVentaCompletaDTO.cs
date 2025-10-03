using System.Collections.Generic;

namespace DTOs
{
    public class CrearVentaCompletaDTO
    {
        public int? IdPersona { get; set; }
        public List<LineaVentaDTO> Lineas { get; set; } = new List<LineaVentaDTO>();

        public bool Finalizada { get; set; }

    }
}
