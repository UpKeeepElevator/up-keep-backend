using UpKeep.Data.DTO.Core;
using UpKeep.Data.DTO.Core.Cliente;
using UpKeep.Data.Models;

namespace UpKeep.Data.Contracts;

public interface IClienteRepositorio
{
    Task<ClienteDto> AgregarCliente(ClienteRequest cliente);
    Task<ClienteDto> GetCliente(int clienteId);
    Task<ClienteDto> GetCliente(string clienteNombre);
    Task<IEnumerable<EdificioDto>> GetEdificiosCliente(int clienteId);
    Task<bool> AgregarEdificio(EdificioRequest request);
    Task<EdificioDto> GetEdificio(string requestEdificio1);
}