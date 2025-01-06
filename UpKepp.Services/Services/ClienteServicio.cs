using Mapster;
using RazorLight.Extensions;
using UpKeep.Data;
using UpKeep.Data.Contracts;
using UpKeep.Data.DTO.Core;
using UpKeep.Data.DTO.Core.Usuarios;
using UpKeep.Data.Exceptions.Conflict;
using UpKeep.Data.Exceptions.NotFound;
using UpKepp.Services.Contracts;

namespace UpKepp.Services.Services;

public class ClienteServicio : ServicioBase, IClienteService
{
    public ClienteServicio(IRepositorioManager repositorioManager) : base(repositorioManager)
    {
    }

    public async Task<ClienteDto> RegistrarCliente(ClienteRequest request)
    {
        //Validar request 
        try
        {
            await _repositorioManager.clienteRepositorio.GetCliente(request.Nombre);
            throw new GenericConflict("Cliente ya existe");
        }
        catch (ClienteNotFound e)
        {
        }

        bool validUser = true;
        validUser = !string.IsNullOrEmpty(request.Usuario.cuenta);
        validUser = !string.IsNullOrEmpty(request.Usuario.contrasena);

        if (!validUser)
            throw new Exception("Usuario invalido");

        //Crear cliente
        var nuevoCliente = await _repositorioManager.clienteRepositorio.AgregarCliente(request);


        return nuevoCliente;
    }

    public async Task<ClienteDto> GetCliente(int clienteId)
    {
        var cliente = await _repositorioManager.clienteRepositorio.GetCliente(clienteId);

        return cliente;
    }
}