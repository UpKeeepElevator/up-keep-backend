using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpKeep.Data.Configuration;
using UpKeep.Data.DTO;
using UpKeep.Data.DTO.Core;
using UpKeep.Data.DTO.Core.Averias;
using UpKeep.Data.DTO.Core.Cliente;
using UpKeep.Data.DTO.Core.Usuarios;
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
        [ProducesResponseType(typeof(UsuarioLogin), StatusCodes.Status200OK)]
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

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] RecuperarPassword cuenta)
        {
            bool exito = await _servicioManager.UsuarioServicio.ForgotPassword(cuenta);

            ResponseGeneric response = new()
            {
                Message = "Solicitud de recuperar contraseña enviada"
            };


            return Ok(response);
        }

        //Agregar restriccion de claim de rol para reset
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword cuenta)
        {
            string usuario = User.FindFirstValue(IdentityData.NameIdentifierClaimName);

            bool exito = await _servicioManager.UsuarioServicio.ResetPassword(cuenta, usuario);

            ResponseGeneric response = new()
            {
                Message = "Contraseña restablecida"
            };


            return Ok(response);
        }

        [HttpGet("")]
        public async Task<IActionResult> BuscarUsuarios()
        {
            IEnumerable<UsuarioDTO> tecnicos = await _servicioManager.UsuarioServicio.GetUsuarios();

            return Ok(tecnicos);
        }


        [HttpGet("tecnicos")]
        public async Task<IActionResult> BuscarTecnicos()
        {
            IEnumerable<UsuarioDTO> tecnicos = await _servicioManager.UsuarioServicio.GetTecnicos();

            return Ok(tecnicos);
        }

        [HttpGet("tecnico/{tecnicoId}/trabajos")]
        public async Task<IActionResult> BuscarTrabajosTecnico([FromRoute] int tecnicoId)
        {
            IEnumerable<TrabajoHecho> trabajosHechos =
                await _servicioManager.UsuarioServicio.BuscarTrabajosHechosTecnico(tecnicoId);

            return Ok(trabajosHechos);
        }


        [HttpGet("cliente/{clienteId}/averias")]
        public async Task<IActionResult> BuscarTrabajosCliente([FromRoute] int clienteId)
        {
            IEnumerable<TrabajoAveria> trabajosHechos =
                await _servicioManager.UsuarioServicio.BuscarTrabajoAverias(clienteId);

            return Ok(trabajosHechos);
        }


        [HttpDelete("{usuarioId}")]
        public async Task<IActionResult> DeleteUsuario([FromRoute] int usuarioId)
        {
            bool exito = await _servicioManager.UsuarioServicio.DeleteUsuario(usuarioId);

            ResponseGeneric response = new()
            {
                Message = $"Usuario-{usuarioId} desactivado"
            };


            return Ok(response);
        }

        [HttpPut("{usuarioId}")]
        public async Task<IActionResult> EditarUsuario([FromRoute] int usuarioId, [FromBody] EditarUsuario usuario)
        {
            UsuarioDTO nuevoUsuario = await _servicioManager.UsuarioServicio.EditarUsuario(usuarioId, usuario);

            return Ok(nuevoUsuario);
        }
    }
}