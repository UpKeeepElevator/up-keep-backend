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


    //- Buscar averia
    [HttpGet("{averiaId}")]
    [ProducesResponseType(typeof(AveriaDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAverias([FromRoute] int averiaId)
    {
        AveriaDto averias = await _servicioManager.AveriaServicio.GetAveria(averiaId);

        return Ok(averias);
    }


    /// <summary>
    /// Crear reporte de averia.
    /// </summary>
    /// <remarks>
    /// Crear el repoorte de una veria indicando el tipo de averia y un comentario junto con evidencia para describir la falla. 
    /// </remarks>
    /// <param name="registroRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(ResponseGeneric), StatusCodes.Status200OK)]
    public async Task<IActionResult> ReportarAveria([FromForm] AveriaRegistroRequest registroRequest)
    {
        bool exito = await _servicioManager.AveriaServicio.ReportarAveria(registroRequest);


        ResponseGeneric response = new ResponseGeneric();
        response.Message = $"Averia registrada para ascensor-{registroRequest.AscensorId}";

        return Ok(response);
    }


    /// <summary>
    /// Cerrar reporte de averia
    /// </summary>
    /// <remarks>
    /// Completa el reporte de una averia (Despues de ser atendida). 
    /// ## Cierre
    /// Necesita la descripción del error, la solución al problema y evidencias con relación al error y la solución.
    /// 
    /// > En el cierre se toman las marcas de tiempo para el seguimiento de las averias.
    /// </remarks>
    /// <param name="averiaId"></param>
    /// <param name="cierreRequest"></param>
    /// <returns></returns>
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


    /// <summary>
    /// Asignar averia a técnico
    /// </summary>
    /// <param name="asignacionRequest"></param>
    /// <returns></returns>
    [HttpPost("asignar")]
    [ProducesResponseType(typeof(AveriaDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> AsignarAveriaTecnico([FromBody] AveriaAsignacionRequest asignacionRequest)
    {
        AveriaDto response = await _servicioManager.AveriaServicio.AsignarTecnicoAveria(asignacionRequest);

        return Ok(response);
    }

    [HttpGet("tipo-averias")]
    [ProducesResponseType(typeof(IEnumerable<AveriaDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAveriasTipos()
    {
        IEnumerable<TipoAveriaDto> averias = await _servicioManager.AveriaServicio.GetTiposAverias();

        return Ok(averias);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AveriaDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAverias()
    {
        IEnumerable<AveriaDto> averias = await _servicioManager.AveriaServicio.GetAverias();

        return Ok(averias);
    }

    /// <summary>
    /// Buscar averias de cliente.
    /// </summary>
    /// <param name="clienteId"></param>
    /// <returns></returns>
    [HttpGet("cliente/{clienteId}")]
    [ProducesResponseType(typeof(IEnumerable<AveriaDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAveriasCliente([FromRoute] int clienteId)
    {
        IEnumerable<AveriaDto> averias = await _servicioManager.AveriaServicio.GetAveriasCliente(clienteId);

        return Ok(averias);
    }


    /// <summary>
    /// Buscar averias asignadas a tecnico
    /// </summary>
    /// <param name="tecnicoId"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Buscar averias activas
    /// </summary>
    /// <returns></returns>
    [HttpGet("activas")]
    [ProducesResponseType(typeof(IEnumerable<AveriaDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAveriasActivas()
    {
        IEnumerable<AveriaDto> averias = await _servicioManager.AveriaServicio.GetAveriasActivas();

        return Ok(averias);
    }

}