using API_ProyectoGestionInventario.Models.ModelsAcceso;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoGestionInventario.BusinessLogic.Services.AccesoServices;

namespace API_ProyectoGestionInventario.Controllers.ControllersAcceso
{
    [Route("api/[controller]")]
    [ApiController]
    public class PantallasController : ControllerBase
    {
        private readonly AccesoServices _accesoServices;
        private readonly IMapper _mapper;

        public PantallasController(AccesoServices accesoServices, IMapper mapper)
        {
            _accesoServices = accesoServices;
            _mapper = mapper;
        }

        [HttpGet("Listar")]
        public IActionResult Listar()
        {
            var data = _accesoServices.Pantallas_Listar();
            var datos = _mapper.Map<IEnumerable<PantallasViewModel>>(data.Data);
            return Ok(datos);
        }
    }
}
