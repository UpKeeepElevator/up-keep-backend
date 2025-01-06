using System;
using System.Collections.Generic;

namespace UpKeep.Data.Models;

public partial class Chequeo
{
    public int EstadoSeccionId { get; set; }

    public string? ChequeoComentarios { get; set; }

    public int ChequeoId { get; set; }

    public DateOnly? ChequeoFecha { get; set; }

    public DateTime? ChequeoHora { get; set; }

    public int MantenimientoId { get; set; }

    public int SeccionId { get; set; }

    public virtual ICollection<AnexoChequeo> AnexoChequeos { get; set; } = new List<AnexoChequeo>();

    public virtual EstadoSeccion EstadoSeccion { get; set; } = null!;

    public virtual Mantenimiento Mantenimiento { get; set; } = null!;

    public virtual Seccion Seccion { get; set; } = null!;
}
