using UpKeep.Data.DTO.Core.Cliente;
using UpKeep.Data.Models;

namespace UpKeep.Data.DTO.Core.Ascensores;

public class AscensorDto
{
    public int AscensorId { get; set; }

    public int NumeroPisos { get; set; }

    public string Marca { get; set; } = null!;

    public string Modelo { get; set; } = null!;

    public int Capacidad { get; set; }

    public string TipoAscensor { get; set; } = null!;

    public string TipoDeUso { get; set; } = null!;

    public string? UbicacionEnEdificio { get; set; }

    public string? Geolocalizacion { get; set; }

    public int EdificioId { get; set; }

    public EdificioDto Edificio { get; set; } = null!;

    IEnumerable<SeccionAscensor> Secciones { get; set; } = new List<SeccionAscensor>();
}

public class AscensorRequest
{
    public int NumeroPisos { get; set; }

    public string Marca { get; set; } = null!;

    public string Modelo { get; set; } = null!;

    public int Capacidad { get; set; }

    public string TipoAscensor { get; set; } = null!;

    public string TipoDeUso { get; set; } = null!;

    public string? UbicacionEnEdificio { get; set; }

    public string? Geolocalizacion { get; set; }

    public int EdificioId { get; set; }
}