using System;
using System.Collections.Generic;

namespace UpKeep.Data.Models;

public partial class Prioridad
{
    public int PrioridadId { get; set; }

    public string? Descripcion { get; set; }

    public string? NombrePrioridad { get; set; }

    public virtual ICollection<Solicitud> Solicituds { get; set; } = new List<Solicitud>();
}
