
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    }
}

/*

Mantenimiento

- Buscar mantenimientos registrados


*/