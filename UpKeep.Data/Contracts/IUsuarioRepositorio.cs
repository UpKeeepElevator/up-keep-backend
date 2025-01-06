using UpKeep.Data.DTO.Core;

namespace UpKeep.Data.Contracts;

public interface IUsuarioRepositorio
{
    Task<string> GetUsuarioSalt(AuthUsuario usuarioAutenticar);
    Task<UsuarioLogin> AutenticarUsuario(AuthUsuario usuarioAutenticar);
    Task<UsuarioDTO> GetUsuario(string usuarioRequestCuenta);
    Task<UsuarioDTO> AgregarUsuario(UsuarioRequest usuarioRequest);
}