using UpKeep.Data.Contracts;
using UpKeep.Data.DTO.Core;
using UpKepp.Services.Contracts;

namespace UpKepp.Services.Services;

public class ClienteServicio : ServicioBase, IClienteService
{
    public ClienteServicio(IRepositorioManager repositorioManager) : base(repositorioManager)
    {
    }

    public Task<ClienteDto> RegistrarCliente(ClienteRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<ClienteDto> GetCliente(int clienteId)
    {
        throw new NotImplementedException();
    }
}