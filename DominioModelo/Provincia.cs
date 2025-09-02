using System;
using System.Collections.Generic;

namespace DominioModelo;

public partial class Provincia
{
    public int IdProvincia { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Localidad> Localidades { get; set; } = new List<Localidad>();
}
