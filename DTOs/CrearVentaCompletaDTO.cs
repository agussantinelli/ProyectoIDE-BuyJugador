using System.Collections.Generic;

namespace DTOs
{
    public class CrearVentaCompletaDTO
    {
        public int IdVenta { get; set; }
        public int IdPersona { get; set; }
        public List<LineaVentaDTO> Lineas { get; set; }

        /// <summary>
        /// Indica si la venta debe crearse o marcarse como 'Finalizada'.
        /// </summary>
        public bool Finalizada { get; set; } // Propiedad añadida

        public CrearVentaCompletaDTO()
        {
            Lineas = new List<LineaVentaDTO>();
        }
    }
}

