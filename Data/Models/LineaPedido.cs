using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class LineaPedido
{
    public int Cantidad { get; set; }

    public int IdPedido { get; set; }

    public int? IdProducto { get; set; }

    public int NroLineaPedido { get; set; }

    public virtual Pedido IdPedidoNavigation { get; set; } = null!;

    public virtual Producto? IdProductoNavigation { get; set; }
}
