using System.Collections.Generic;

namespace DTOs
{
    public class CrearVentaCompletaDTO
    {
        public int IdVenta { get; set; }
        public int IdPersona { get; set; }
        public List<LineaVentaDTO> Lineas { get; set; }

        public bool Finalizada { get; set; } 

        public CrearVentaCompletaDTO()
        {
            Lineas = new List<LineaVentaDTO>();
        }
    }
}

