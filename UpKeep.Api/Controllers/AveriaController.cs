using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpKeep.Data.DTO;
using UpKeep.Data.DTO.Core.Averias;
using UpKepp.Services.Contracts;

namespace UpKeepApi.Controllers;

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
    [ProducesResponseType(typeof(ResponseGeneric), StatusCodes.Status200OK)]
    public async Task<IActionResult> ReportarAveria([FromForm] AveriaRegistroRequest registroRequest)
    {
        bool exito = await _servicioManager.AveriaServicio.ReportarAveria(registroRequest);


        ResponseGeneric response = new ResponseGeneric();
        response.Message = $"Averia registrada para ascensor-{registroRequest.AscensorId}";

        return Ok(response);
    }


    [HttpPost("{averiaId}/cerrar")]
    [ProducesResponseType(typeof(ResponseGeneric), StatusCodes.Status200OK)]
    public async Task<IActionResult> CerrarAveria([FromRoute] int averiaId,
        [FromForm] AveriaCierreRequest cierreRequest)
    {
        bool exito = await _servicioManager.AveriaServicio.CerrarAveria(cierreRequest);

        ResponseGeneric response = new ResponseGeneric();
        response.Message = $"Averia cerrada para averia-{cierreRequest.AveriaId}";

        return Ok(response);
    }


    [HttpPost("asignar")]
    [ProducesResponseType(typeof(AveriaDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> AsignarAveriaTecnico([FromBody] AveriaAsignacionRequest asignacionRequest)
    {
        AveriaDto response = await _servicioManager.AveriaServicio.AsignarTecnicoAveria(asignacionRequest);

        return Ok(response);
    }


    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AveriaDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAverias()
    {
        IEnumerable<AveriaDto> averias = await _servicioManager.AveriaServicio.GetAverias();

        return Ok(averias);
    }

//- Buscar averias de cliente.
    [HttpGet("cliente/{clienteId}")]
    [ProducesResponseType(typeof(IEnumerable<AveriaDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAveriasCliente([FromRoute] int clienteId)
    {
        IEnumerable<AveriaDto> averias = await _servicioManager.AveriaServicio.GetAveriasCliente(clienteId);

        return Ok(averias);
    }


//- Buscar averias asignadas a tecnico
    [HttpGet("tecnico/{tecnicoId}")]
    [ProducesResponseType(typeof(IEnumerable<AveriaDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAveriasTecnico([FromRoute] int tecnicoId)
    {
        IEnumerable<AveriaDto> averias = await _servicioManager.AveriaServicio.GetAveriasTecnicoAsignadas(tecnicoId);

        return Ok(averias);
    }

    [HttpGet("tecnico/{tecnicoId}/activas")]
    [ProducesResponseType(typeof(IEnumerable<AveriaDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAveriasTecnicoActivas([FromRoute] int tecnicoId)
    {
        IEnumerable<AveriaDto> averias =
            await _servicioManager.AveriaServicio.GetAveriasTecnicoAsignadasActivas(tecnicoId);

        return Ok(averias);
    }

//- Buscar averias activas de cliente
    [HttpGet("cliente/{clienteId}/activas")]
    [ProducesResponseType(typeof(IEnumerable<AveriaDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAveriasClienteActivas([FromRoute] int clienteId)
    {
        IEnumerable<AveriaDto> averias = await _servicioManager.AveriaServicio.GetAveriasClienteActivas(clienteId);

        return Ok(averias);
    }

//- Buscar averias activas
    [HttpGet("activas")]
    [ProducesResponseType(typeof(IEnumerable<AveriaDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAveriasActivas()
    {
        IEnumerable<AveriaDto> averias = await _servicioManager.AveriaServicio.GetAveriasActivas();

        return Ok(averias);
    }

//- Buscar averia
    [HttpGet("{averiaId}")]
    [ProducesResponseType(typeof(AveriaDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAverias([FromRoute] int averiaId)
    {
        AveriaDto averias = await _servicioManager.AveriaServicio.GetAveria(averiaId);

        return Ok(averias);
    }
}