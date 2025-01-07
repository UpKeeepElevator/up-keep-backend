using System;
using System.Collections.Generic;

namespace UpKeep.Data.Models;

public partial class Solicitud
{
    public int SolicitudId { get; set; }

    public int TecnicoId { get; set; }

    public int AscensorId { get; set; }

    public DateTime FechaSolicitud { get; set; }

    public DateTime? FechaRespuesta { get; set; }

    public int PrioridadId { get; set; }

    public string? DescripcionSolicitud { get; set; }

    public int ServicioId { get; set; }

    public virtual Ascensor Ascensor { get; set; } = null!;

    public virtual Prioridad Prioridad { get; set; } = null!;

    public virtual Servicio Servicio { get; set; } = null!;

    public virtual Usuario Tecnico { get; set; } = null!;
}
