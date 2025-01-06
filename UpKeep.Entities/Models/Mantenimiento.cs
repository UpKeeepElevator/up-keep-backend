using System;
using System.Collections.Generic;

namespace UpKeep.Data.Models;

public partial class Mantenimiento
{
    public int MantenimientoId { get; set; }

    public int? TecnicoId { get; set; }

    public int? AscensorId { get; set; }

    public DateOnly? Fecha { get; set; }

    public DateTime? Hora { get; set; }

    public string? Firma { get; set; }

    public int? Duracion { get; set; }

    public string? Geolocalizacion { get; set; }

    public virtual Ascensor? Ascensor { get; set; }

    public virtual ICollection<Chequeo> Chequeos { get; set; } = new List<Chequeo>();

    public virtual Usuario? Tecnico { get; set; }
}
