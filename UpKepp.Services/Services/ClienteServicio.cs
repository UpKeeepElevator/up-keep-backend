using Mapster;
using RazorLight.Extensions;
using UpKeep.Data;
using UpKeep.Data.Contracts;
using UpKeep.Data.DTO.Core;
using UpKeep.Data.DTO.Core.Cliente;
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
        //
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

    public async Task<ClienteDto> GetClienteConUsuario(int usuarioId)
    {
        var cliente = await _repositorioManager.clienteRepositorio.GetClienteConUsuario(usuarioId);

        return cliente;
    }

    public async Task<EdificioDto> GetEdificio(string edificioNombre)
    {
        EdificioDto edificio = await _repositorioManager.clienteRepositorio.GetEdificio(edificioNombre);

        edificio.Cliente = await _repositorioManager.clienteRepositorio.GetCliente(edificio.ClienteId);

        return edificio;
    }

    public async Task<IEnumerable<EdificioDto>> AgregarEdificio(EdificioRequest request)
    {
        //Buscar cliente
        await _repositorioManager.clienteRepositorio.GetCliente(request.ClienteId);
        //Buscar Edificio
        try
        {
            EdificioDto obj = await GetEdificio(request.Edificio1);
            throw new GenericConflict("Edificio ya existe");
        }
        catch (EdificioNotFound e)
        {
        }

        //Agregar edificio

        bool exito = await _repositorioManager.clienteRepositorio.AgregarEdificio(request);

        IEnumerable<EdificioDto> edificios =
            await _repositorioManager.clienteRepositorio.GetEdificiosCliente(request.ClienteId);


        return edificios;
    }

    public async Task<IEnumerable<ClienteDto>> GetClientes()
    {
        IEnumerable<ClienteDto> clientes = await _repositorioManager.clienteRepositorio.GetClientes();


        return clientes;
    }
}