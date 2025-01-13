using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpKeep.Data.DTO;
using UpKeep.Data.DTO.Core.Ascensores;
using UpKepp.Services.Contracts;

namespace UpKeepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AscensorController : ControllerBase
    {
        private readonly IServicioManager _servicioManager;


        public AscensorController(IServicioManager servicioManager)
        {
            _servicioManager = servicioManager;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseGeneric), StatusCodes.Status200OK)]
        public async Task<IActionResult> AgregarAscensor([FromBody] AscensorRequest request)
        {
            bool exito = await _servicioManager.AscensorServicio.AgregarAscensor(request);

            ResponseGeneric response = new ResponseGeneric();
            response.Message = $"Ascensor agregado a edificio-{request.EdificioId}";
            response.StatusCode = 200;
            return Ok(response);
        }

        [HttpPost("{ascensorId}")]
        public async Task<IActionResult> AgregarSeccionesAscensor([FromRoute] int ascensorId,
            [FromBody] AscensorRequest request)
        {
            bool exito = await _servicioManager.AscensorServicio.AgregarSeccionesAscensor(ascensorId, request);

            AscensorDto ascensorDto = await _servicioManager.AscensorServicio.GetAscensor(ascensorId);
            return Ok(ascensorDto);
        }
    }
}
/*

Ascensores
- Buscar ascensor
- Buscar ascensores de edificio
- editar ascensores
*/