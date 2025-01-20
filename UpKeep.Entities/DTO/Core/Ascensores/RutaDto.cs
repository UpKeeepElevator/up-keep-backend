namespace UpKeep.Data.DTO.Core.Ascensores;

public class RutaDto
{
    public string RutaId { get; set; }

    public string NombreRuta { get; set; }

    public int CantidadAscensores { get; set; }

    public int CantidadVisitas { get; set; }

    public IEnumerable<AscensorRutaDto> Ascensores { get; set; } = new List<AscensorRutaDto>();
}

public class RutaRequest
{
    public string RutaId { get; set; }

    public string NombreRuta { get; set; }
}

public class AscensorRutaDto
{
    public string RutaId { get; set; }

    public int AscensorId { get; set; }

    public DateOnly? FechaVisita { get; set; }

    public int Orden { get; set; } = 0;
}