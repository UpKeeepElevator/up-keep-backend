using Mapster;
using Microsoft.EntityFrameworkCore;
using Serilog;
using UpKeep.Data.Context;
using UpKeep.Data.Contracts;
using UpKeep.Data.DTO.Core;
using UpKeep.Data.DTO.Core.Averias;
using UpKeep.Data.DTO.Core.Cliente;
using UpKeep.Data.DTO.Core.Usuarios;
using UpKeep.Data.Exceptions.NotFound;
using UpKeep.Data.Models;

namespace UpKeep.Data.Repositories;

public class UsuarioRepositorio : RepositorioBase, IUsuarioRepositorio
{
    public UsuarioRepositorio(UpKeepDbContext mySqlContext) : base(mySqlContext)
    {
    }

    public async Task<string> GetUsuarioSalt(AuthUsuario usuarioAutenticar)
    {
        Usuario? user = await dbContext.Usuarios
            .FirstOrDefaultAsync(x => x.Correo == usuarioAutenticar.cuenta);
        if (user == null)
            throw new UsuarioNotFound(usuarioAutenticar.cuenta);

        return user.Salt.Trim();
    }

    public async Task<UsuarioLogin> AutenticarUsuario(AuthUsuario usuarioAutenticar)
    {
        UsuarioLogin usuarioLogin = new UsuarioLogin();

        Usuario? user = await dbContext.Usuarios
            .Include(usuario => usuario.Rols)
            .FirstOrDefaultAsync(
                x => x.Correo == usuarioAutenticar.cuenta && x.Password == usuarioAutenticar.contrasena);

        if (user == null)
            throw new UsuarioNotFound(usuarioAutenticar.cuenta);

        var roles = user.Rols.ToList();
        usuarioLogin = user.Adapt<UsuarioLogin>();
        usuarioLogin.Roles = roles.AsQueryable().ProjectToType<RolDto>().ToList();

        return usuarioLogin;
    }

    public async Task<UsuarioDTO> GetUsuario(string usuarioRequestCuenta)
    {
        Usuario? user = await dbContext.Usuarios
            .FirstOrDefaultAsync(
                x => x.Correo == usuarioRequestCuenta);
        if (user == null)
            throw new UsuarioNotFound(usuarioRequestCuenta);

        UsuarioDTO usuarioDTO = new UsuarioDTO();
        usuarioDTO = user.Adapt<UsuarioDTO>();
        usuarioDTO.Roles = user.Rols.ToList().AsQueryable().ProjectToType<RolDto>();

        return usuarioDTO;
    }

    public async Task<UsuarioDTO> GetUsuario(int usuarioId)
    {
        Usuario? user = await dbContext.Usuarios
            .Include(usuario => usuario.Rols)
            .FirstOrDefaultAsync(
                x => x.UsuarioId == usuarioId);
        if (user == null)
            throw new UsuarioNotFound(usuarioId);

        UsuarioDTO usuarioDTO = new UsuarioDTO();
        usuarioDTO = user.Adapt<UsuarioDTO>();
        usuarioDTO.Roles = user.Rols.ToList().AsQueryable().ProjectToType<RolDto>();

        return usuarioDTO;
    }

    public async Task<UsuarioDTO> AgregarUsuario(UsuarioRequest usuarioRequest)
    {
        Usuario usuario = new Usuario
        {
            Correo = usuarioRequest.cuenta,
            Nombres = usuarioRequest.Nombres,
            Password = usuarioRequest.contrasena,
            Salt = usuarioRequest.salt,
            Telefono = usuarioRequest.Telefono,
        };

        List<Rol> lstRoles = await GetRoles();

        foreach (RolDto usuarioRequestRole in usuarioRequest.Roles)
        {
            var obj = lstRoles.Find(x => x.RolId == usuarioRequestRole.RolId);
            if (obj == null)
                continue;
            usuario.Rols
                .Add(obj);
        }

        //Agregar Usuario
        try
        {
            dbContext.Usuarios.Add(usuario);
            SavesChanges();
        }
        catch (Exception e)
        {
            Log.Error("Crear nuevo usuario: {P1}", e.Message);
            Log.Error("{P1}", e.InnerException);
            throw new Exception("Error creando nuevo usuario");
        }

        return await GetUsuario(usuario.Correo);
    }

    public async Task<List<Rol>> GetRoles()
    {
        return dbContext.Rols.ToList();
    }

    public Task<List<Rol>> GetRoles(int usuarioId)
    {
        var usuario = dbContext.Usuarios
            .Include(x => x.Rols)
            .FirstOrDefault(x => x.UsuarioId == usuarioId);
        if (usuario is null)
            throw new UsuarioNotFound(usuarioId);

        var roles = usuario.Rols.ToList();

        return Task.FromResult(roles);
    }

    public Task<IEnumerable<UsuarioDTO>> GetTecnicos()
    {
        int rolTecnico = 1;
        var tecnicos = dbContext.Usuarios
            .Where(x => x.Rols.Any(y => y.RolId == rolTecnico));

        var projectToType = tecnicos.ProjectToType<UsuarioDTO>().AsEnumerable();

        return Task.FromResult(projectToType);
    }

    public Task<IEnumerable<TrabajoAveria>> BuscarTrabajoAverias(int clienteId)
    {
        IEnumerable<TrabajoAveria> averias = dbContext.Averia
            .Join(dbContext.Ascensors,
                x => x.AscensorId,
                x => x.AscensorId,
                (averia, ascensor) => new { averia, ascensor })
            .Join(dbContext.Edificios,
                x => x.ascensor.EdificioId,
                x => x.EdificioId,
                (averiaAscensor, edificio) => new { averia = averiaAscensor, edificio })
            .Where(x => x.edificio.ClienteId == clienteId)
            .Select(x => new TrabajoAveria()
                {
                    Trabajo = "Averia",
                    FechaReportado = x.averia.averia.FechaReporte,
                    FechaAtendido = x.averia.averia.FechaRespuesta
                }
            );

        return Task.FromResult(averias);
    }

    public Task<IEnumerable<TrabajoHecho>> BuscarTrabajosHechosTecnico(int tecnicoId)
    {
        var averias = dbContext.Averia
            .Where(averia => averia.TecnicoId == tecnicoId)
            .Select(x => new TrabajoHecho()
            {
                Fecha = x.FechaRespuesta,
                Trabajo = "Averia",
                AscensorId = x.AscensorId
            });

        var mantenimientos = new List<TrabajoHecho>();
        // mantenimientos = dbContext.Mantenimientos
        // .Where(mantenimiento => mantenimiento.TecnicoId == tecnicoId)
        //     .Select(x => new TrabajoHecho()
        //     {
        //         Fecha = x.Fecha,
        //         Trabajo = "Averia",
        //         AscensorId = x.AscensorId ?? 0
        //     });

        IEnumerable<TrabajoHecho> trabajos = averias;

        return Task.FromResult(trabajos);
    }

    public Task<IEnumerable<UsuarioDTO>> GetUsuarios()
    {
        var usuarios = dbContext.Usuarios;

        var projectToType = usuarios.ProjectToType<UsuarioDTO>().AsEnumerable();

        return Task.FromResult(projectToType);
    }
}