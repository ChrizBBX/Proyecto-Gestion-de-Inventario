using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoGestionInventarioCAAG._Features.Empleados;

namespace ProyectoGestionInventarioCAAG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly EmpleadoService _empleadoService;

        public EmpleadoController(IMapper mapper, EmpleadoService empleadoService)
        {
            _mapper = mapper;
            _empleadoService = empleadoService;
        }

        [HttpGet("ObtenerEmpleados")]
        public IActionResult ObtenerEmpleados() 
        {
            var result = _empleadoService.ObtenerEmpleados();
            return Ok(result);
        }
    }
}
