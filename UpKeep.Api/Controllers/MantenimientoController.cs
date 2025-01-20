using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UpKeep.Data.DTO.Core.Cliente;
using UpKeep.Data.Models;
using UpKepp.Services.Contracts;

namespace UpKeepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MantenimientoController : ControllerBase
    {
        private readonly IServicioManager _servicioManager;


        public MantenimientoController(IServicioManager servicioManager)
        {
            _servicioManager = servicioManager;
        }

        [HttpPost]
        public async Task<IActionResult> PostMantenimiento([FromForm] MantenimientoRequest mantenimientoRequest)
        {
            bool exito = await _servicioManager.MantenimientoService.PostMantenimiento(mantenimientoRequest);

            return Ok();
        }

        [HttpPost("{mantenimientoId}/chequeo")]
        public async Task<IActionResult> PostMantenimientoChequeo([FromForm] ChequeoRequest chequeoRequest)
        {
            bool exito = await _servicioManager.MantenimientoService.PostMantenimientoChequeo(chequeoRequest);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetMantenimientos()
        {
            IEnumerable<MantenimientoDto> mantenimientos =
                await _servicioManager.MantenimientoService.GetMantenimientos();

            return Ok(mantenimientos);
        }

        [HttpGet("{mantenimientoId}")]
        public async Task<IActionResult> GetMantenimiento([FromRoute] int mantenimientoId)
        {
            MantenimientoDto mantenimientos =
                await _servicioManager.MantenimientoService.GetMantenimiento(mantenimientoId);

            return Ok(mantenimientos);
        }

        [HttpGet("visitador/{tecnicoId}")]
        public async Task<IActionResult> GetMantenimientosTecnico([FromRoute] int tecnicoId)
        {
            IEnumerable<MantenimientoDto> mantenimientos =
                await _servicioManager.MantenimientoService.GetMantenimientosTecnico(tecnicoId);

            return Ok(mantenimientos);
        }


        [HttpGet("estado-seccion")]
        public async Task<IActionResult> GetEstadoSeccion()
        {
            IEnumerable<EstadoSeccionDto> mantenimientos =
                await _servicioManager.MantenimientoService.GetEstadosSeccion();

            return Ok(mantenimientos);
        }

        [HttpGet("chequeo/{chequeoId}")]
        public async Task<IActionResult> GetChequeo([FromRoute] int chequeoId)
        {
            ChequeoDto mantenimientos =
                await _servicioManager.MantenimientoService.GetChequeo(chequeoId);

            return Ok(mantenimientos);
        }
    }
}

/*

Mantenimiento

- Buscar mantenimientos registrados


*/