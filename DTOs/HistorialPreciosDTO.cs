using System;
using System.Collections.Generic;

namespace DTOs
{
    public class PrecioPuntoDTO
    {
        public DateTime Fecha { get; set; }

        public decimal Monto { get; set; }
    }

    public class HistorialPrecioProductoDTO
    {
        public int IdProducto { get; set; }

        public string NombreProducto { get; set; } = string.Empty;

        public List<PrecioPuntoDTO> Puntos { get; set; } = new();
    }
}