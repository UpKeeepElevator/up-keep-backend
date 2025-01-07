using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpKeep.Data.DTO;
using UpKeep.Data.DTO.Core.Averias;
using UpKepp.Services.Contracts;

namespace UpKeepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AveriaController : ControllerBase
    {
        private readonly IServicioManager _servicioManager;


        public AveriaController(IServicioManager servicioManager)
        {
            _servicioManager = servicioManager;
        }

        [HttpPost]
        public async Task<IActionResult> ReportarAveria([FromBody] AveriaRegistroRequest registroRequest)
        {
            bool exito = await _servicioManager.AveriaServicio.ReportarAveria(registroRequest);

            ResponseGeneric response = new ResponseGeneric();
            response.Message = $"Averia registrada para ascensor-{registroRequest.AscensorId}";

            return Ok(response);
        }
    }
}