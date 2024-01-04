using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoGestionInventarioCAAG._Features.Lotes;
using ProyectoGestionInventarioCAAG._Features.Lotes.Dtos;

namespace ProyectoGestionInventarioCAAG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoteController : ControllerBase
    {
        private readonly LoteService _loteService;

        public LoteController(LoteService loteService)
        {
            _loteService = loteService;
        }

        [HttpGet("ListadoLotes")]
        public IActionResult ListadoLotes()
        {
            var result = _loteService.ListadoLotes();
            return Ok(result);
        }

        [HttpPost("InsertarLote")]
        public IActionResult InsertarLote(LoteDto entidad)
        {
            var result = _loteService.InsertarLote(entidad);
            return Ok(result);
        }
    }
}
