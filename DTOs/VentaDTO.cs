using System;
using System.Collections.Generic;

namespace DTOs
{
    public class VentaDTO
    {
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public int? IdPersona { get; set; }
        public string NombreVendedor { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public List<LineaVentaDTO> Lineas { get; set; } = new List<LineaVentaDTO>();

        public static VentaDTO FromDominio(DominioModelo.Venta entidad)
        {
            if (entidad == null) return null;

            return new VentaDTO
            {
                IdVenta = entidad.IdVenta,
                Fecha = entidad.Fecha,
                Estado = entidad.Estado,
                IdPersona = entidad.IdPersona,
                NombreVendedor = entidad.IdPersonaNavigation?.NombreCompleto ?? "N/A"
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
