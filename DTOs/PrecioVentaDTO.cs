using System;

namespace DTOs
{
    public class PrecioVentaDTO
    {
        public int IdProducto { get; set; }
        public DateTime FechaDesde { get; set; }
        public decimal Monto { get; set; }

        public static PrecioVentaDTO FromDominio(DominioModelo.PrecioVenta e)
        {
            if (e == null) return null;
            return new PrecioVentaDTO
            {
                IdProducto = e.IdProducto,
                FechaDesde = e.FechaDesde,
                Monto = e.Monto
            };
        }

        public DominioModelo.PrecioVenta ToDominio()
        {
            return new DominioModelo.PrecioVenta
            {
                IdProducto = IdProducto,
                FechaDesde = FechaDesde,
                Monto = Monto
            };
        }
    }
}
