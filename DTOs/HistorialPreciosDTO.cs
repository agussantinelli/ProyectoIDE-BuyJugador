using System;
using System.Collections.Generic;

namespace DTOs
{
    // # Representa un único punto en el gráfico (Fecha, Monto).
    public class PrecioPuntoDTO
    {
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
    }

    // # Contiene la información de un producto y su lista de puntos de precios.
    public class HistorialPrecioProductoDTO
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public List<PrecioPuntoDTO> Puntos { get; set; } = new();
    }
}