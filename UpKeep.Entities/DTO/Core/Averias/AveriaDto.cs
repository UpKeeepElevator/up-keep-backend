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
    public IEnumerable<AnexoAveriaDto> AnexoAveria { get; set; } = new List<AnexoAveriaDto>();
}

public class AnexoAveriaDto
{
    public Guid AnexoId { get; set; }

    public string AnexoNombre { get; set; } = null!;

    public string AnexoTipo { get; set; } = null!;

    public string? AnexoRuta { get; set; }

    public int AveriaId { get; set; }

    public string? AnexoPeso { get; set; }

}
