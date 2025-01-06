using UpKeep.Data.DTO.Core;

namespace UpKepp.Services.Contracts;

public interface IUsuarioService
{
    public Task<UsuarioDTO> AutenticarUsuario(AuthUsuario usuarioAutenticar, string[] credenciales);
}