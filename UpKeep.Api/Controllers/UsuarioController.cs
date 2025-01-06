using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpKeep.Data.DTO.Core;
using UpKeep.Data.Exceptions.NotFound;
using UpKeep.Data.Models;
using UpKepp.Services.Contracts;

namespace UpKeepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IServicioManager _servicioManager;

        private readonly IConfiguration _config;

        public UsuarioController(IServicioManager servicioManager, IConfiguration config)
        {
            _config = config;
            _servicioManager = servicioManager;
        }

        [Tags(["1 - Auth"])]
        [HttpPost("auth")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(AuthUsuario usuario)
        {
            string[] credenciales =
                [_config["JwtSettings:Key"], _config["JwtSettings:Issuer"], _config["JwtSettings:Audience"]];
            UsuarioLogin user = new();

            try
            {
                user = await _servicioManager.UsuarioServicio.AutenticarUsuario(usuario, credenciales);
            }
            catch (UsuarioNotFound e)
            {
                return Unauthorized();
            }


            return Ok(user);
        }


        [HttpPost("registrar")]
        [ProducesResponseType(typeof(UsuarioDTO), StatusCodes.Status201Created)]
        public async Task<IActionResult> CrearUsuario([FromBody] UsuarioRequest usuario)
        {
            var nuevoUsuario = await _servicioManager.UsuarioServicio.CrearUsuario(usuario);

            return Created("", nuevoUsuario);
        }
    }
}