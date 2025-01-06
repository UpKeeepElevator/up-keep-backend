using System;
using System.Collections.Generic;

namespace UpKeep.Data.Models;

public partial class Cliente
{
    public int ClienteId { get; set; }

    public string? Nombre { get; set; }

    public string? Telefono { get; set; }

    public string? NombreContacto { get; set; }

    public virtual ICollection<Edificio> Edificios { get; set; } = new List<Edificio>();

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}
