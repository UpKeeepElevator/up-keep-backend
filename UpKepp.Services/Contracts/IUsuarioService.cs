using UpKeep.Data.DTO.Core;
using UpKeep.Data.DTO.Core.Usuarios;

namespace UpKepp.Services.Contracts;

public interface IUsuarioService
{
    public Task<UsuarioLogin> AutenticarUsuario(AuthUsuario usuarioAutenticar, string[] credenciales);

    public Task<UsuarioDTO> CrearUsuario(UsuarioRequest usuarioRequest, bool esCliente = false);
    Task<IEnumerable<RolDto>> GetRoles();
}