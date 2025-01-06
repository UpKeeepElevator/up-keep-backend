using Mapster;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Serilog;
using UpKeep.Data.Context;
using UpKeep.Data.Contracts;
using UpKeep.Data.DTO.Core;
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
        Cliente? cliente = await dbContext.Clientes.FirstOrDefaultAsync(x => x.ClienteId == clienteId);
        if (cliente == null)
            throw new ClienteNotFound(clienteId);

        ClienteDto clienteEncontrado = cliente.Adapt<ClienteDto>();
        clienteEncontrado.Usuario = dbContext.Usuarios.First(x => x.UsuarioId == clienteEncontrado.UsuarioId)
            .Adapt<UsuarioShort>();

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

        return clienteEncontrado;
    }
}