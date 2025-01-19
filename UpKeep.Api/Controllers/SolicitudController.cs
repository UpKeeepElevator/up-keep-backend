using System.Drawing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UpKeep.Data.DTO;
using UpKeep.Data.DTO.Core.Solicitudes;
using UpKepp.Services.Contracts;

namespace UpKeepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
[Authorize]
    public class SolicitudController : ControllerBase
    {
        private readonly IServicioManager _servicioManager;


        public SolicitudController(IServicioManager servicioManager)
        {
            _servicioManager = servicioManager;
        }

        //- SOlicitar servicio

        [HttpPost]
        [ProducesResponseType(typeof(ResponseGeneric), StatusCodes.Status200OK)]
        public async Task<IActionResult> SolicitarServicio([FromBody] SolicitudRequest request)
        {
            bool exito = await _servicioManager.SolicitudServicio.SolicitarServicio(request);

            ResponseGeneric response = new ResponseGeneric();
            response.Message = $"Solicitud creada para ascensor-{request.AscensorId}";

            return Ok(response);
        }


        [HttpPost("servicio")]
        [ProducesResponseType(typeof(ResponseGeneric), StatusCodes.Status200OK)]
        public async Task<IActionResult> AgregarServicio([FromBody] ServicioRequest request)
        {
            bool exito = await _servicioManager.SolicitudServicio.AgregarServicio(request);

            ResponseGeneric response = new ResponseGeneric();
            response.Message = $"Servicio-{request.nombreservicio} creado";

            return Ok(response);
        }




        //- Buscar servicios
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SolicitudDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSolicitudes()
        {
            IEnumerable<SolicitudDto> solicitudes = await _servicioManager.SolicitudServicio.GetSolicitudes();

            return Ok(solicitudes);
        }

        //- Buscar solicitud
        [HttpGet("{solicitudId}")]
        [ProducesResponseType(typeof(SolicitudDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSolicitudes([FromRoute] int solicitudId)

        {
            SolicitudDto solicitudes = await _servicioManager.SolicitudServicio.GetSolicitud(solicitudId);

            return Ok(solicitudes);
        }
        //- Buscar servicios de ascensor

        [HttpGet("ascensor/{ascensorId}")]
        [ProducesResponseType(typeof(IEnumerable<SolicitudDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSolicitudesAscensor([FromRoute] int ascensorId)

        {
            IEnumerable<SolicitudDto> solicitudes =
                await _servicioManager.SolicitudServicio.GetSolicitudesAscensor(ascensorId);

            return Ok(solicitudes);
        }
    }
}