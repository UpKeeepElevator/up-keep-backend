namespace UpKeep.Data.DTO.Core.Averias;

public class AveriaDto
{
    public int AveriaId { get; set; }

    public int AscensorId { get; set; }

    public int TipoAveriaId { get; set; }

    public DateTime FechaReporte { get; set; }

    public string? Evidencia { get; set; }

    public string ComentarioAveria { get; set; } = null!;

    public DateTime? FechaRespuesta { get; set; }

    public string? ErrorEncontrado { get; set; }

    public string? ReparacionRealizada { get; set; }

    public int? SeccionAveria { get; set; }

    public int? TecnicoId { get; set; }

    public int? TiempoReparacion { get; set; }

    public int? TiempoRespuesta { get; set; }

    public string? Firma { get; set; }

    public string? Geolocalizacion { get; set; }
}

public class AveriaRegistroRequest
{
    public int AscensorId { get; set; }
    public TipoAveriaDto TipoAveria { get; set; }

    public DateTime FechaReporte { get; set; }

    public string? Evidencia { get; set; }

    public string ComentarioAveria { get; set; } = null!;
}

public class TipoAveriaDto
{
    public int TipoAveriaId { get; set; }

    public string TipoNombre { get; set; }

    public string TipoDescripcion { get; set; }
}