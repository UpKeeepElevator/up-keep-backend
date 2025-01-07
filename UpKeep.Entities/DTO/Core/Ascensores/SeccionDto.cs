using UpKeep.Data.Models;

namespace UpKeep.Data.DTO.Core.Ascensores;

public class SeccionDto
{
    public int SeccionId { get; set; }

    public string NombreSeccion { get; set; } = null!;

    public int TipoSeccionId { get; set; }
    public TipoSeccionDto TipoSeccion { get; set; } = null!;

    public DateTime? UltimaRevision { get; set; }
}

public class TipoSeccionDto
{
    public int TipoSeccionId { get; set; }

    public string TipoSeccionNombre { get; set; } = null!;

    public string TipoDescripcion { get; set; } = null!;
}