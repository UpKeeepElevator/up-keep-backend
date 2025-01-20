using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UpKeep.Data.DTO;
using UpKeep.Data.DTO.Core.Ascensores;
using UpKepp.Services.Contracts;

namespace UpKeepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RutaController : ControllerBase
    {
        private readonly IServicioManager _servicioManager;


        public RutaController(IServicioManager servicioManager)
        {
            _servicioManager = servicioManager;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseGeneric), StatusCodes.Status200OK)]
        public async Task<IActionResult> CrearRuta([FromBody] RutaRequest request)
        {
            bool exito = await _servicioManager.RutaService.CrearRuta(request);

            ResponseGeneric response = new ResponseGeneric();
            response.Message = $"Ruta creada con éxito";
            response.StatusCode = 200;
            return Ok(response);
        }

        [HttpPost("{rutaId}/ascensor")]
        [ProducesResponseType(typeof(ResponseGeneric), StatusCodes.Status200OK)]
        public async Task<IActionResult> AgregarAscensorARuta([FromRoute] string rutaId,
            [FromBody] AscensorRutaDto dto)
        {
            dto.RutaId = rutaId;
            bool exito = await _servicioManager.RutaService.AgregarAscensorARuta(dto);

            ResponseGeneric response = new ResponseGeneric();
            response.Message = exito ? "Ascensor agregado a la ruta con éxito" : "Error al agregar ascensor a la ruta";
            response.StatusCode = exito ? 200 : 400;

            return exito ? Ok(response) : BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetRutas()
        {
            IEnumerable<RutaDto> rutas = await _servicioManager.RutaService.GetRutas();
            return Ok(rutas);
        }

        [HttpGet("{rutaId}")]
        public async Task<IActionResult> GetRuta([FromRoute] string rutaId)
        {
            RutaDto ruta = await _servicioManager.RutaService.GetRuta(rutaId);
            return Ok(ruta);
        }
    }
}

/*

Ruta
- Crear ruta
- Agregar ruta
- editar ruta
- Ver ruta

*/