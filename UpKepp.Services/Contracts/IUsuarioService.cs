using UpKeep.Data.DTO.Core;
using UpKeep.Data.DTO.Core.Averias;
using UpKeep.Data.DTO.Core.Cliente;
using UpKeep.Data.DTO.Core.Usuarios;

namespace UpKepp.Services.Contracts;

public interface IUsuarioService
{
    public Task<UsuarioLogin> AutenticarUsuario(AuthUsuario usuarioAutenticar, string[] credenciales);
    Task<IEnumerable<TrabajoAveria>> BuscarTrabajoAverias(int clienteId);
    Task<IEnumerable<TrabajoHecho>> BuscarTrabajosHechosTecnico(int tecnicoId);
    public Task<UsuarioDTO> CrearUsuario(UsuarioRequest usuarioRequest, bool esCliente = false);
    Task<bool> DeleteUsuario(int usuarioId);
    Task<UsuarioDTO> EditarUsuario(int usuarioId, EditarUsuario usuario);
    Task<bool> ForgotPassword(RecuperarPassword cuenta);
    Task<IEnumerable<RolDto>> GetRoles();
    Task<IEnumerable<UsuarioDTO>> GetTecnicos();

    /// <summary>
    /// Restablecer constraseña del usuario indicado 
    /// </summary>
    /// <param name="cuenta"></param>
    /// <param name="usuario"></param>
    /// <returns></returns>
    Task<bool> ResetPassword(ResetPassword cuenta, string? usuario);

    Task<IEnumerable<UsuarioDTO>> GetUsuarios();
}