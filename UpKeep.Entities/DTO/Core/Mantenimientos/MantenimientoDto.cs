using Microsoft.AspNetCore.Http;

namespace UpKeep.Data.DTO.Core.Cliente;

public class MantenimientoDto
{
    public int MantenimientoId { get; set; }

    public int TecnicoId { get; set; }

    public int AscensorId { get; set; }

    public DateOnly Fecha { get; set; }

    public DateTime Hora { get; set; }

    public string Firma { get; set; } = null!;

    public int Duracion { get; set; }

    public string Geolocalizacion { get; set; } = null!;

    public IEnumerable<ChequeoDto> Chequeos { get; set; } = new List<ChequeoDto>();
}

public class MantenimientoRequest
{
    public int TecnicoId { get; set; }

    public int AscensorId { get; set; }

    public DateOnly Fecha { get; set; }

    public DateTime Hora { get; set; }

    public string Firma { get; set; }

    public int Duracion { get; set; }

    public string Geolocalizacion { get; set; }
}

public class ChequeoRequest
{
    public int MantenimientoId { get; set; }
    public int EstadoSeccionId { get; set; }
    public int SeccionId { get; set; }

    public string ChequeoComentarios { get; set; } = null!;

    public DateOnly ChequeoFecha { get; set; }

    public DateTime ChequeoHora { get; set; }

    public List<IFormFile> Anexos { get; set; }
}

public class ChequeoDto
{
    public int ChequeoId { get; set; }
    public int EstadoSeccionId { get; set; }

    public string ChequeoComentarios { get; set; } = null!;


    public DateOnly ChequeoFecha { get; set; }

    public DateTime ChequeoHora { get; set; }

    public int MantenimientoId { get; set; }

    public int SeccionId { get; set; }

    public IEnumerable<AnexoChequeoDto> Anexos { get; set; } = new List<AnexoChequeoDto>();
}

public class AnexoChequeoDto
{
    public Guid AnexoId { get; set; }

    public string AnexoNombre { get; set; } = null!;

    public string AnexoTipo { get; set; } = null!;

    public string AnexoRuta { get; set; } = null!;

    public int ChequeoId { get; set; }

    public string AnexoPeso { get; set; } = null!;
}