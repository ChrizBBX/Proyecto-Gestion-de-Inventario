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

        [HttpPost("RecibirSalida")]
        public IActionResult RecibirSalida(SalidaRecibirDto entidad)
        {
            var result = _salidaService.RecibirSalida(entidad);
            return Ok(result);
        }

        [HttpGet("Reporte")]
        public IActionResult Reporte(DateTime? fechaInicio, DateTime? fechaFin, int? sucursalId)
        {
            var result = _salidaService.Reporte(fechaInicio,fechaFin, sucursalId);
            return Ok(result);
        }
    }
}
