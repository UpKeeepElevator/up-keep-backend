using Microsoft.AspNetCore.Http;

namespace UpKeep.Data.DTO.Core.Averias;

public class AveriaRegistroRequest
{
    public int AscensorId { get; set; }
    public int TipoAveriaId { get; set; }

    public DateTime FechaReporte { get; set; }

    public IFormFile Evidencia { get; set; }

    public string ComentarioAveria { get; set; } = null!;
}

public class AveriaCierreRequest
{
    public int AveriaId { get; set; }
    public int TecnicoId { get; set; }
    public DateTime FechaRespuesta { get; set; }
    public string ErrorEncontrado { get; set; }
    public string ReparacionRealizada { get; set; }
    public int SeccionAveria { get; set; }
    public int TiempoRespuesta { get; set; }
    public int TiempoReparacion { get; set; }
    public string Geolocalizacion { get; set; }

    public List<IFormFile> Anexos { get; set; }
}

public class AveriaAsignacionRequest
{
    public int AveriaId { get; set; }
    public int TecnicoId { get; set; }
}