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

    public IEnumerable<SeccionAscensorDto> Secciones { get; set; } = new List<SeccionAscensorDto>();
}

public class SeccionAscensorDto
{
    public int ParteAscensorId { get; set; }
    public int SeccionId { get; set; }
    public string NombreSeccion { get; set; } = null!;

    public int AscensorId { get; set; }

    public DateTime? UltimaRevision { get; set; }
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