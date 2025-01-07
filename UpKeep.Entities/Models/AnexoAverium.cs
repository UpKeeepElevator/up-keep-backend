using System;
using System.Collections.Generic;

namespace UpKeep.Data.Models;

public partial class AnexoAverium
{
    public Guid AnexoId { get; set; }

    public string AnexoNombre { get; set; } = null!;

    public string? AnexoRuta { get; set; }

    public int AveriaId { get; set; }

    public string? AnexoPeso { get; set; }

    public virtual Averium Averia { get; set; } = null!;
}
