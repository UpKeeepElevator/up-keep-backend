using System;
using System.Collections.Generic;

namespace UpKeep.Data.Models;

public partial class AnexoChequeo
{
    public Guid AnexoId { get; set; }

    public string? AnexoNombre { get; set; }

    public string? AnexoTipo { get; set; }

    public string? AnexoRuta { get; set; }

    public int? ChequeoId { get; set; }

    public string? AnexoPeso { get; set; }

    public virtual Chequeo? Chequeo { get; set; }
}
