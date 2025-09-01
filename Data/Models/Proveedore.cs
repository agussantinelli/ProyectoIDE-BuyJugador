using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Proveedore
{
    public int IdProveedor { get; set; }

    public string RazonSocial { get; set; } = null!;

    public string Cuit { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public int? IdLocalidad { get; set; }

    public virtual Localidade? IdLocalidadNavigation { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
