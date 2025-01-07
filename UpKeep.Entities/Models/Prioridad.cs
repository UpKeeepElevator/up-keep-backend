using System;
using System.Collections.Generic;

namespace UpKeep.Data.Models;

public partial class Prioridad
{
    public int PrioridadId { get; set; }

    public string Descripcion { get; set; } = null!;

    public string NombrePrioridad { get; set; } = null!;

    public virtual ICollection<Solicitud> Solicituds { get; set; } = new List<Solicitud>();
}
