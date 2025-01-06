using System;
using System.Collections.Generic;

namespace UpKeep.Data.Models;

public partial class AscensorRutum
{
    public string RutaId { get; set; } = null!;

    public int AscensorId { get; set; }

    public DateOnly? FechaVisita { get; set; }

    public DateOnly? FechaVisitada { get; set; }

    public int Orden { get; set; }

    public virtual Ascensor Ascensor { get; set; } = null!;

    public virtual Rutum Ruta { get; set; } = null!;
}
