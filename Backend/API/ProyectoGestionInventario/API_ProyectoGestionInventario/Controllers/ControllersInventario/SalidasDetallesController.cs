using API_ProyectoGestionInventario.Models.ModelsInventario;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoGestionInventario.BusinessLogic.Services.InventarioServices;
using ProyectoGestionInventario.Entities.Entities;

namespace API_ProyectoGestionInventario.Controllers.ControllersInventario
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalidasDetallesController : ControllerBase
    {
        private readonly InventarioServices _inventarioServices;
        private IMapper _mapper;

        public SalidasDetallesController(InventarioServices inventarioServices, IMapper mapper)
        {
            _inventarioServices = inventarioServices;
            _mapper = mapper;
        }

        [HttpPost("Insertar")]
        public IActionResult Insertar(SalidasDetallesViewModel item)
        {
            var mapeo = _mapper.Map<tbSalidasDetalles>(item);
            var datos = _inventarioServices.SalidasDetalles_Insertar(mapeo);
            return Ok(datos);
        }
    }
}
