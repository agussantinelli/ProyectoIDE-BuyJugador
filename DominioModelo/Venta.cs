using System;
using System.Collections.Generic;

namespace DominioModelo;

public partial class Venta
{
    public int IdVenta { get; set; }

    public DateTime Fecha { get; set; }

    public string Estado { get; set; } = null!;

    public int? IdPersona { get; set; }

    public virtual Persona? IdPersonaNavigation { get; set; }

    public virtual ICollection<LineaVenta> LineaVenta { get; set; } = new List<LineaVenta>();
}
