using System;
using System.Collections.Generic;

namespace UpKeep.Data.Models;

public partial class Cliente
{
    public int ClienteId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string NombreContacto { get; set; } = null!;

    public int UsuarioId { get; set; }

    public virtual ICollection<Edificio> Edificios { get; set; } = new List<Edificio>();

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Usuario Usuario { get; set; } = null!;
}
