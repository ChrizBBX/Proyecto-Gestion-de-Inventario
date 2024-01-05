using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoGestionInventarioCAAG._Features.Salidas;
using ProyectoGestionInventarioCAAG._Features.Salidas.Dtos;

namespace ProyectoGestionInventarioCAAG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalidaController : ControllerBase
    {
        private readonly SalidaService _salidaService;

        public SalidaController(SalidaService salidaService)
        {
            _salidaService = salidaService;
        }

        [HttpPost("InsertarSalida")]
        public IActionResult InsertarSalida(SalidaDto entidad)
        {
            var result = _salidaService.InsertarSalida(entidad);
            return Ok(result);
        }
    }
}
