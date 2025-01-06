using UpKeep.Data.DTO.Core.Usuarios;

namespace UpKeep.Data.DTO.Core;

public class ClienteRequest
{
    public string Nombre { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string NombreContacto { get; set; } = null!;

    public int UsuarioId { get; set; } = 0;


    public AuthUsuario Usuario { get; set; } = null!;
}