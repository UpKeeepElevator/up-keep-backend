using System;
using System.Collections.Generic;

namespace UpKeep.Data.Models;

public partial class Averium
{
    public int AveriaId { get; set; }

    public int? AscensorId { get; set; }

    public int? TipoAveriaId { get; set; }

    public DateTime? FechaReporte { get; set; }

    public string? Evidencia { get; set; }

    public string? ComentarioAveria { get; set; }

    public DateTime? FechaRespuesta { get; set; }

    public string? ErrorEncontrado { get; set; }

    public string? ReparacionRealizada { get; set; }

    public int? SeccionAveria { get; set; }

    public int? TecnicoId { get; set; }

    public int? TiempoReparacion { get; set; }

    public int? TiempoRespuesta { get; set; }

    public string? Firma { get; set; }

    public string? Geolocalizacion { get; set; }

    public virtual Ascensor? Ascensor { get; set; }

    public virtual Seccion? SeccionAveriaNavigation { get; set; }

    public virtual Usuario? Tecnico { get; set; }

    public virtual TipoAverium? TipoAveria { get; set; }
}
