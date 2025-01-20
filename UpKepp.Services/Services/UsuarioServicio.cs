using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Mapster;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using UpKeep.Data.Configuration;
using UpKeep.Data.Contracts;
using UpKeep.Data.DTO.Core;
using UpKeep.Data.DTO.Core.Averias;
using UpKeep.Data.DTO.Core.Cliente;
using UpKeep.Data.DTO.Core.Usuarios;
using UpKeep.Data.Exceptions.Conflict;
using UpKeep.Data.Exceptions.NotFound;
using UpKeep.Data.Models;
using UpKepp.Services.Contracts;

namespace UpKepp.Services.Services;

public class UsuarioServicio : ServicioBase, IUsuarioService
{
    public UsuarioServicio(IRepositorioManager repositorioManager) : base(repositorioManager)
    {
    }


    private string GenerateJSONWebToken(UsuarioDTO userInfo, string[] _config)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config[0]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //Corregir claims
        bool esAdmin = false;
        bool esTecnico = false;
        bool esCliente = false;

        foreach (RolDto objRol in userInfo.Roles)
        {
            if (objRol.RolId == 1)
                esTecnico = true;

            if (objRol.RolId == 2)
                esAdmin = true;

            if (objRol.RolId == 3)
                esCliente = true;
        }


        var claims = new[]
        {
            new Claim(IdentityData.AdminRoleClaimName, esAdmin.ToString()),
            new Claim(IdentityData.ClienteRoleClaimName, esCliente.ToString()),
            new Claim(IdentityData.TecnicoRoleClaimName, esTecnico.ToString()),
            new Claim(ClaimTypes.NameIdentifier, userInfo.UsuarioId.ToString()),
        };

        var token = new JwtSecurityToken(_config[1],
            _config[2],
            claims,
            expires: DateTime.Now.AddMinutes(200),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<UsuarioLogin> AutenticarUsuario(AuthUsuario usuarioAutenticar, string[] credenciales)
    {
        Log.Information($"Solicitud de autenticacion : {usuarioAutenticar.cuenta}");

        UsuarioLogin objUsuario = await ProcesoAutenticacion(usuarioAutenticar);
        if (objUsuario.Nombres == "")
            return null;

        Log.Information($"Usuario: {objUsuario.Correo} autenticado");

        var tokenString = GenerateJSONWebToken(objUsuario, credenciales);
        objUsuario.Token = tokenString;


        return objUsuario;
    }

    public async Task<UsuarioDTO> CrearUsuario(UsuarioRequest usuarioRequest, bool esCliente = false)
    {
        try
        {
            await _repositorioManager.usuarioRepositorio.GetUsuario(usuarioRequest.cuenta);
            throw new UsuarioConflict(usuarioRequest.cuenta);
        }
        catch (UsuarioNotFound e)
        {
        }

        //Validation
        List<RolDto> roles = (await _repositorioManager.usuarioRepositorio.GetRoles()).AsQueryable()
            .ProjectToType<RolDto>().ToList();

        bool validUser = usuarioRequest.Validar(roles);

        var rolCliente = usuarioRequest.Roles.FirstOrDefault(x => x.RolId == 3);

        bool usuarioConRolCliente = !esCliente && rolCliente != null;
        bool clienteSinRolCliente = esCliente && rolCliente == null;
        bool clienteConMasDeUnRol = esCliente && usuarioRequest.Roles.Count() > 1;

        if (usuarioConRolCliente || clienteSinRolCliente || clienteConMasDeUnRol)
            validUser = false;

        if (!validUser)
            throw new Exception("Usuario invalido");

        //Crear autenticacion
        var salt = usuarioRequest.GetNewSalt();
        string obj = Convert.ToHexString(salt);

        usuarioRequest.HashPasword(Encoding.UTF8.GetBytes(obj));
        usuarioRequest.salt = Convert.ToHexString(salt);

        UsuarioDTO newUser = await _repositorioManager.usuarioRepositorio.AgregarUsuario(usuarioRequest);


        return newUser;
    }

    public async Task<IEnumerable<RolDto>> GetRoles()
    {
        return (await _repositorioManager.usuarioRepositorio.GetRoles()).AsQueryable().ProjectToType<RolDto>();
    }

    private async Task<UsuarioLogin> ProcesoAutenticacion(AuthUsuario usuarioAutenticar)
    {
        string userSalt = await _repositorioManager.usuarioRepositorio.GetUsuarioSalt(usuarioAutenticar);

        usuarioAutenticar.HashPasword(Encoding.UTF8.GetBytes(userSalt));
        var result = await _repositorioManager.usuarioRepositorio.AutenticarUsuario(usuarioAutenticar);

        return result;
    }

    public Task<bool> ForgotPassword(RecuperarPassword cuenta)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ResetPassword(ResetPassword cuenta, string? usuario)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<UsuarioDTO>> GetUsuarios()
    {
        List<UsuarioDTO> usuarios = (await _repositorioManager.usuarioRepositorio.GetUsuarios()).ToList();
        foreach (var usuarioDto in usuarios)
        {
            var roles = await _repositorioManager.usuarioRepositorio.GetRoles(usuarioDto.UsuarioId);
            usuarioDto.Roles = roles.OrderByDescending(x => x.RolId).AsQueryable().ProjectToType<RolDto>();
        }

        return usuarios;
    }

    public async Task<IEnumerable<UsuarioDTO>> GetTecnicos()
    {
        var usuarios = await _repositorioManager.usuarioRepositorio.GetTecnicos();


        return usuarios;
    }

    public async Task<IEnumerable<TrabajoAveria>> BuscarTrabajoAverias(int clienteId)
    {
        return await _repositorioManager.usuarioRepositorio.BuscarTrabajoAverias(clienteId);
    }

    public async Task<IEnumerable<TrabajoHecho>> BuscarTrabajosHechosTecnico(int tecnicoId)
    {
        return await _repositorioManager.usuarioRepositorio.BuscarTrabajosHechosTecnico(tecnicoId);
    }

    public Task<bool> DeleteUsuario(int usuarioId)
    {
        throw new NotImplementedException();
    }

    public Task<UsuarioDTO> EditarUsuario(int usuarioId, EditarUsuario usuario)
    {
        throw new NotImplementedException();
    }
}