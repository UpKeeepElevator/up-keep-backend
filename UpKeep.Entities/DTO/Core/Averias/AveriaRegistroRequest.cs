namespace UpKeep.Data.DTO.Core.Averias;

public class AveriaRegistroRequest
{
    public int AscensorId { get; set; }
    public TipoAveriaDto TipoAveria { get; set; }

    public DateTime FechaReporte { get; set; }

    public string? Evidencia { get; set; }

    public string ComentarioAveria { get; set; } = null!;
}