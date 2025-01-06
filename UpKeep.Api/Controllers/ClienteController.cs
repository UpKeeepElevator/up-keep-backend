using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpKeep.Data.DTO.Core;
using UpKepp.Services.Contracts;

namespace UpKeepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClienteController : ControllerBase
    {
        private readonly IServicioManager _servicioManager;


        public ClienteController(IServicioManager servicioManager, IConfiguration config)
        {
            _servicioManager = servicioManager;
        }


        [HttpPost("RegistrarCliente")]
        [ProducesResponseType(typeof(ClienteDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegistrarCliente([FromBody] ClienteRequest request)
        {
            var cliente = await _servicioManager.ClienteServicio.RegistrarCliente(request);

            return Created("", cliente);
        }
    }
}