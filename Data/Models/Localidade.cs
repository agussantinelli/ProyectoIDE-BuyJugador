using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Localidade
{
    public int IdLocalidad { get; set; }

    public string Nombre { get; set; } = null!;

    public int? IdProvincia { get; set; }

    public virtual Provincia? IdProvinciaNavigation { get; set; }

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();

    public virtual ICollection<Proveedore> Proveedores { get; set; } = new List<Proveedore>();
}
