using UpKeep.Data.DTO.Core;
using UpKeep.Data.Models;

namespace UpKeep.Data.Contracts;

public interface IClienteRepositorio
{
    Task<ClienteDto> AgregarCliente(ClienteRequest cliente);
    Task<ClienteDto> GetCliente(int clienteId);
    Task<ClienteDto> GetCliente(string clienteNombre);
}