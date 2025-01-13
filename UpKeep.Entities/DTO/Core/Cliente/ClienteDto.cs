using UpKeep.Data.DTO.Core.Usuarios;

namespace UpKeep.Data.DTO.Core.Cliente;

public class ClienteDto
{
    public int ClienteId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string NombreContacto { get; set; } = null!;

    public int UsuarioId { get; set; }
    public UsuarioShort Usuario { get; set; } = null!;

    public IEnumerable<EdificioDto> Edificios { get; set; } = new List<EdificioDto>();
}