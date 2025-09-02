using System;
using System.Collections.Generic;

namespace DominioModelo;

public partial class Persona
{
    public int IdPersona { get; set; }

    public string? NombreCompleto { get; set; }

    public int Dni { get; set; }

    public string Email { get; set; } = null!;

    public byte[] Password { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public int? IdLocalidad { get; set; }

    public DateOnly? FechaIngreso { get; set; }

    public virtual Localidad? IdLocalidadNavigation { get; set; }

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
