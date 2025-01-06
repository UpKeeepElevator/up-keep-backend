using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using UpKeep.Data.Configuration;
using UpKeep.Data.Contracts;
using UpKeep.Data.DTO.Core;
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

        //TODO: utilizar roles correspondiente.
        bool esAdmin = false;
        bool esSuperAdmin = false;

        // foreach (Rol objRol in userInfo.roles)
        // {
        //     // if (objRol.cod_rol == 1 || objRol.cod_rol == 3)
        //     //     esAdmin = true;
        //     //
        //     // if (objRol.cod_rol == 8)
        //     //     esSuperAdmin = true;
        // }


        var claims = new[]
        {
            new Claim(IdentityData.AdminClaimName, esAdmin.ToString()),
            // new Claim(IdentityData.LaboratorioCodClaimName, userInfo.laboratorio.laboratorio_cod),
            // new Claim(ClaimTypes.NameIdentifier, userInfo.cod_usuario.ToString()),
            new Claim(IdentityData.SuperAdminClaimName, esSuperAdmin.ToString())
        };

        var token = new JwtSecurityToken(_config[1],
            _config[2],
            claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<UsuarioDTO> AutenticarUsuario(AuthUsuario usuarioAutenticar, string[] credenciales)
    {
        Log.Information($"Solicitud de autenticacion : {usuarioAutenticar.cuenta}");

        UsuarioDTO objUsuario = await PRocesoAutenticacion(usuarioAutenticar);
        // if (objUsuario.nombres == "")
        //     return null;

        // Log.Information($"Usuario: {objUsuario.cuenta} autenticado");

        var tokenString = GenerateJSONWebToken(objUsuario, credenciales);
        // objUsuario.Token = tokenString;


        return objUsuario;
    }

    private async Task<UsuarioDTO> PRocesoAutenticacion(AuthUsuario usuarioAutenticar)
    {
        // string userSalt = await _repositorioManager.usuarioRepositorio.GetUsuarioSalt(usuarioAutenticar);
        string userSalt = "";

        usuarioAutenticar.HashPasword(Encoding.UTF8.GetBytes(userSalt));
        // var result =  await _repositorioManager.usuarioRepositorio.AutenticarUsuario(usuarioAutenticar, app);

        return new();
    }
}