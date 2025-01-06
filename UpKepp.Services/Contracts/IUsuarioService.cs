using UpKeep.Data.DTO.Core;

namespace UpKepp.Services.Contracts;

public interface IUsuarioService
{
    public Task<UsuarioLogin> AutenticarUsuario(AuthUsuario usuarioAutenticar, string[] credenciales);

    public Task<UsuarioDTO> CrearUsuario(UsuarioRequest usuarioRequest, bool esCliente = false);
}