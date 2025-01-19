using UpKeep.Data.DTO.Core;
using UpKeep.Data.DTO.Core.Cliente;
using UpKeep.Data.Models;

namespace UpKepp.Services.Contracts;

public interface IClienteService
{
    Task<ClienteDto> RegistrarCliente(ClienteRequest request);
    Task<ClienteDto> GetCliente(int clienteId);
    Task<ClienteDto> GetClienteConUsuario(int usuarioId);
    Task<EdificioDto> GetEdificio(string edificioNombre);
    Task<IEnumerable<EdificioDto>> AgregarEdificio(EdificioRequest request);
    Task<IEnumerable<ClienteDto>> GetClientes();
}