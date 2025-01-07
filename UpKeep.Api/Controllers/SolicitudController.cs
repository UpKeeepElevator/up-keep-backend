
using Microsoft.AspNetCore.Mvc;
using UpKepp.Services.Contracts;

namespace UpKeepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudController : ControllerBase
    {
        private readonly IServicioManager _servicioManager;


        public SolicitudController(IServicioManager servicioManager)
        {
            _servicioManager = servicioManager;
        }




    }
}

/*

Solicitud
- SOlicitar servicio
- Buscar servicios
- Buscar solicitud
- Buscar servicios de ascensor
*/