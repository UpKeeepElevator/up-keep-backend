using System;
using System.Collections.Generic;

namespace UpKeep.Data.Models;

public partial class AnexoChequeo
{
    public Guid AnexoId { get; set; }

    public string AnexoNombre { get; set; } = null!;

    public string AnexoTipo { get; set; } = null!;

    public string AnexoRuta { get; set; } = null!;

    public int ChequeoId { get; set; }

    public string AnexoPeso { get; set; } = null!;

    public virtual Chequeo Chequeo { get; set; } = null!;
}
