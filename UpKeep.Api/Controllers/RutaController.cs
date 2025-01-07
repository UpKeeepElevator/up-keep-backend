
using Microsoft.AspNetCore.Mvc;
using UpKepp.Services.Contracts;

namespace UpKeepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RutaController : ControllerBase
    {
        private readonly IServicioManager _servicioManager;


        public RutaController(IServicioManager servicioManager)
        {
            _servicioManager = servicioManager;
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