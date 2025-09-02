using System;
using System.Collections.Generic;

namespace DominioModelo;

public partial class TipoProducto
{
    public int IdTipoProducto { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
