using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpKeep.Data.DTO.Core;
using UpKeep.Data.DTO.Core.Cliente;
using UpKeep.Data.DTO.Core.Usuarios;
using UpKepp.Services.Contracts;

namespace UpKeepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClienteController : ControllerBase
    {
        private readonly IServicioManager _servicioManager;


        public ClienteController(IServicioManager servicioManager)
        {
            _servicioManager = servicioManager;
        }


        [HttpPost("RegistrarCliente")]
        [ProducesResponseType(typeof(ClienteDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegistrarCliente([FromBody] ClienteRequest request)
        {
            UsuarioRequest nuevoUsuario = new()
            {
                cuenta = request.Usuario.cuenta,
                contrasena = request.Usuario.contrasena,
                Nombres = request.Nombre,
                Telefono = request.Telefono,
                Roles = (await _servicioManager.UsuarioServicio.GetRoles()).Where(x => x.RolId == 3).AsQueryable()
                    .ProjectToType<RolDto>()
            };
            var usuario = await _servicioManager.UsuarioServicio.CrearUsuario(nuevoUsuario, true);
            request.UsuarioId = usuario.UsuarioId;

            var cliente = await _servicioManager.ClienteServicio.RegistrarCliente(request);

            cliente.Usuario = usuario.Adapt<UsuarioShort>();


            return Created("", cliente);
        }

        [HttpPost("{clienteId}/edificio")]
        [ProducesResponseType(typeof(ClienteDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> AgregarEdficio([FromBody] EdificioRequest request, [FromRoute] int clienteId)
        {
            request.ClienteId = clienteId;

            IEnumerable<EdificioDto> edificios = await _servicioManager.ClienteServicio.AgregarEdificio(request);

            var respuesta = await _servicioManager.ClienteServicio.GetCliente(clienteId);
            respuesta.Edificios = edificios;

            return Ok(respuesta);
        }
    }
}
/*
Cliente
- Editar cliente
- Buscar clientes
*/