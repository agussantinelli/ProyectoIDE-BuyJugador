using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Pedido
{
    public int IdPedido { get; set; }

    public DateTime Fecha { get; set; }

    public string Estado { get; set; } = null!;

    public int? IdProveedor { get; set; }

    public virtual Proveedore? IdProveedorNavigation { get; set; }

    public virtual ICollection<LineaPedido> LineaPedidos { get; set; } = new List<LineaPedido>();
}
