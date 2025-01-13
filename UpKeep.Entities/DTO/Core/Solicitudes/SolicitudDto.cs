namespace UpKeep.Data.DTO.Core.Solicitudes;

public class SolicitudDto
{
    public int SolicitudId { get; set; }

    public int TecnicoId { get; set; }

    public int AscensorId { get; set; }

    public DateTime FechaSolicitud { get; set; }

    public DateTime? FechaRespuesta { get; set; }

    public string Estado { get; set; }

    public int PrioridadId { get; set; }
    public PrioridadDto Prioridad { get; set; }

    public string? DescripcionSolicitud { get; set; }

    public int ServicioId { get; set; }
}

public class SolicitudRequest
{
    public int TecnicoId { get; set; }

    public int AscensorId { get; set; }

    public DateTime FechaSolicitud { get; set; }

    public int PrioridadId { get; set; }

    public string? DescripcionSolicitud { get; set; }

    public int ServicioId { get; set; }
}

public class PrioridadDto
{
    public int PrioridadId { get; set; }

    public string Descripcion { get; set; }

    public string NombrePrioridad { get; set; }
}