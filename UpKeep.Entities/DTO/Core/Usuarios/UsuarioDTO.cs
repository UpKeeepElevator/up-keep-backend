namespace UpKeep.Data.DTO.Core.Usuarios;

public class UsuarioDTO
{
    public int UsuarioId { get; set; }
    public string Correo { get; set; }
    public string Nombres { get; set; }

    public string? RutaId { get; set; }

    public string? Telefono { get; set; }
    public IEnumerable<RolDto> Roles { get; set; } = new List<RolDto>();
}

public class UsuarioLogin : UsuarioDTO
{
    public string Token { get; set; }
}