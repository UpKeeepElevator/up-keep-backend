using Mapster;
using Microsoft.EntityFrameworkCore;
using Serilog;
using UpKeep.Data.Context;
using UpKeep.Data.Contracts;
using UpKeep.Data.DTO.Core;
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
            .FirstOrDefaultAsync(
                x => x.Correo == usuarioAutenticar.cuenta && x.Password == usuarioAutenticar.contrasena);

        if (user == null)
            throw new UsuarioNotFound(usuarioAutenticar.cuenta);

        var roles = user.Rols.ToList().AsQueryable();
        usuarioLogin = user.Adapt<UsuarioLogin>();
        usuarioLogin.Roles = roles.ProjectToType<RolDto>();

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
}