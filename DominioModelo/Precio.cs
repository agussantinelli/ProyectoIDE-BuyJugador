using System;
using System.Collections.Generic;

namespace DominioModelo;

public partial class Precio
{
    public decimal Monto { get; set; }

    public DateTime FechaDesde { get; set; }

    public int IdProducto { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
