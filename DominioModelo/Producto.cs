using System;
using System.Collections.Generic;

namespace DominioModelo;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int Stock { get; set; }

    public int? IdTipoProducto { get; set; }

    public bool Activo { get; set; } = true;


    public virtual TipoProducto? IdTipoProductoNavigation { get; set; }

    public virtual ICollection<LineaPedido> LineaPedidos { get; set; } = new List<LineaPedido>();

    public virtual ICollection<LineaVenta> LineaVenta { get; set; } = new List<LineaVenta>();

    public virtual ICollection<Precio> Precios { get; set; } = new List<Precio>();
}
