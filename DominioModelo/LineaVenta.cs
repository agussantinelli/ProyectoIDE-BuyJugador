using System;
using System.Collections.Generic;


namespace DominioModelo;


public partial class LineaVenta
{
    public int Cantidad { get; set; }


    public int IdVenta { get; set; }


    public int? IdProducto { get; set; }


    public int NroLineaVenta { get; set; }

    public decimal PrecioUnitario { get; set; }


    public virtual Producto? IdProductoNavigation { get; set; }


    public virtual Venta IdVentaNavigation { get; set; } = null!;
}
