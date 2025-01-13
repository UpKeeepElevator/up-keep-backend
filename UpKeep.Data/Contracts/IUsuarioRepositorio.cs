using UpKeep.Data.DTO.Core;
using UpKeep.Data.DTO.Core.Usuarios;
using UpKeep.Data.Models;

namespace UpKeep.Data.Contracts;

public interface IUsuarioRepositorio
{
    Task<string> GetUsuarioSalt(AuthUsuario usuarioAutenticar);
    Task<UsuarioLogin> AutenticarUsuario(AuthUsuario usuarioAutenticar);
    Task<UsuarioDTO> GetUsuario(string usuarioRequestCuenta);
    Task<UsuarioDTO> GetUsuario(int usuarioId);
    Task<UsuarioDTO> AgregarUsuario(UsuarioRequest usuarioRequest);
    Task<List<Rol>> GetRoles();
    Task<IEnumerable<UsuarioDTO>> GetTecnicos();
}