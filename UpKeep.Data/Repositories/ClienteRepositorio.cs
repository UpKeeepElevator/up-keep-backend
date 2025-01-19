using Mapster;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Serilog;
using UpKeep.Data.Context;
using UpKeep.Data.Contracts;
using UpKeep.Data.DTO.Core;
using UpKeep.Data.DTO.Core.Cliente;
using UpKeep.Data.DTO.Core.Usuarios;
using UpKeep.Data.Exceptions.NotFound;
using UpKeep.Data.Models;

namespace UpKeep.Data.Repositories;

public class ClienteRepositorio : RepositorioBase, IClienteRepositorio
{
    public ClienteRepositorio(UpKeepDbContext mySqlContext) : base(mySqlContext)
    {
    }

    public Task<ClienteDto> AgregarCliente(ClienteRequest cliente)
    {
        Cliente objCliente = new()
        {
            Nombre = cliente.Nombre,
            Telefono = cliente.Telefono,
            NombreContacto = cliente.NombreContacto,
            UsuarioId = cliente.UsuarioId,
        };

        try
        {
            dbContext.Clientes.Add(objCliente);
            Log.Information("Cliente agregado {P1}", objCliente.Nombre);

            SavesChanges();
        }
        catch (Exception e)
        {
            Log.Error("Error agregando cliente: {P1}", e.Message);
            Log.Error("=>:{P1}", e.InnerException);
            throw new Exception($"Error agregando cliente");
        }

        return GetCliente(cliente.Nombre);
    }

    public async Task<ClienteDto> GetCliente(int clienteId)
    {
        Cliente? cliente = await dbContext.Clientes
            .Include(x => x.Edificios)
            .FirstOrDefaultAsync(x => x.ClienteId == clienteId);
        if (cliente == null)
            throw new ClienteNotFound(clienteId);

        ClienteDto clienteEncontrado = cliente.Adapt<ClienteDto>();
        clienteEncontrado.Usuario = dbContext.Usuarios.First(x => x.UsuarioId == clienteEncontrado.UsuarioId)
            .Adapt<UsuarioShort>();

        clienteEncontrado.Edificios = cliente.Edificios
            .Select(x => new EdificioDto()
            {
                ClienteId = x.ClienteId,
                Edificio1 = x.Edificio1,
                EdificioUbicacion = x.EdificioUbicacion,
                Geolocalizacion = x.Geolocalizacion,
                EdificioId = x.EdificioId
            })
            .AsQueryable().ProjectToType<EdificioDto>();

        return clienteEncontrado;
    }

    public async Task<ClienteDto> GetCliente(string clienteNombre)
    {
        Cliente? cliente = await dbContext.Clientes.FirstOrDefaultAsync(x => x.Nombre == clienteNombre);
        if (cliente == null)
            throw new ClienteNotFound(clienteNombre);

        ClienteDto clienteEncontrado = cliente.Adapt<ClienteDto>();
        clienteEncontrado.Usuario = dbContext.Usuarios.First(x => x.UsuarioId == clienteEncontrado.UsuarioId)
            .Adapt<UsuarioShort>();
        clienteEncontrado.Edificios = cliente.Edificios
            .Select(x => new EdificioDto()
            {
                ClienteId = x.ClienteId,
                Edificio1 = x.Edificio1,
                EdificioUbicacion = x.EdificioUbicacion,
                Geolocalizacion = x.Geolocalizacion,
                EdificioId = x.EdificioId
            })
            .AsQueryable().ProjectToType<EdificioDto>();

        return clienteEncontrado;
    }

    public async Task<ClienteDto> GetClienteConUsuario(int usuarioId)
    {
        Cliente? cliente = await dbContext.Clientes.FirstOrDefaultAsync(x => x.UsuarioId == usuarioId);
        if (cliente == null)
            throw new ClienteNotFound(0);

        ClienteDto clienteEncontrado = cliente.Adapt<ClienteDto>();
        clienteEncontrado.Usuario = dbContext.Usuarios.First(x => x.UsuarioId == clienteEncontrado.UsuarioId)
            .Adapt<UsuarioShort>();
        clienteEncontrado.Edificios = cliente.Edificios
            .Select(x => new EdificioDto()
            {
                ClienteId = x.ClienteId,
                Edificio1 = x.Edificio1,
                EdificioUbicacion = x.EdificioUbicacion,
                Geolocalizacion = x.Geolocalizacion,
                EdificioId = x.EdificioId
            })
            .AsQueryable().ProjectToType<EdificioDto>();

        return clienteEncontrado;
    }

    public Task<IEnumerable<EdificioDto>> GetEdificiosCliente(int clienteId)
    {
        var edificios = dbContext.Edificios
            .Where(x => x.ClienteId == clienteId)
            .Select(x => new EdificioDto()
            {
                ClienteId = x.ClienteId,
                Edificio1 = x.Edificio1,
                EdificioUbicacion = x.EdificioUbicacion,
                Geolocalizacion = x.Geolocalizacion,
                EdificioId = x.EdificioId
            });

        var edificiosDto = edificios.ProjectToType<EdificioDto>();

        return Task.FromResult<IEnumerable<EdificioDto>>(edificiosDto);
    }

    public Task<bool> AgregarEdificio(EdificioRequest request)
    {
        try
        {
            Edificio edificio = new()
            {
                Edificio1 = request.Edificio1,
                EdificioUbicacion = request.EdificioUbicacion,
                Geolocalizacion = request.Geolocalizacion,
                ClienteId = request.ClienteId,
            };

            dbContext.Edificios.Add(edificio);
            SavesChanges();
            Log.Information("Edificio {P1} de cliente-{P2} agregado", request.Edificio1, request.ClienteId);
            return Task.FromResult(true);
        }
        catch (Exception e)
        {
            Log.Error("Error agregando edificio: {P1} {P2}", e.Message, e.InnerException);
            throw new Exception($"Error agregando edificio");
        }
    }

    public Task<EdificioDto> GetEdificio(string requestEdificio1)
    {
        Edificio? edificio = dbContext.Edificios.FirstOrDefault(x => x.Edificio1 == requestEdificio1);
        if (edificio == null) throw new EdificioNotFound(requestEdificio1);

        return Task.FromResult(edificio.Adapt<EdificioDto>());
    }

    public Task<EdificioDto> GetEdificio(int edificioId)
    {
        Edificio? edificio = dbContext.Edificios.FirstOrDefault(x => x.EdificioId == edificioId);
        if (edificio == null) throw new EdificioNotFound(edificioId);

        return Task.FromResult(edificio.Adapt<EdificioDto>());
    }

    public Task<IEnumerable<ClienteDto>> GetClientes()
    {
        var clientes = dbContext.Clientes
            .Select(x => new ClienteDto()
            {
                ClienteId = x.ClienteId,
                Nombre = x.Nombre,
                NombreContacto = x.NombreContacto,
                Telefono = x.Telefono,
                UsuarioId = x.UsuarioId ?? 0
            });

        var clientesDto = clientes.ProjectToType<ClienteDto>().AsEnumerable();

        return Task.FromResult(clientesDto);
    }
}