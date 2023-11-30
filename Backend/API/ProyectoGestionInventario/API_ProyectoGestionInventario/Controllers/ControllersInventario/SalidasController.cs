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
    public class SalidasController : ControllerBase
    {
        private readonly InventarioServices _inventarioServices;
        private IMapper _mapper;

        public SalidasController(InventarioServices inventarioServices, IMapper mapper)
        {
            _inventarioServices = inventarioServices;
            _mapper = mapper;
        }

        [HttpPost("Insertar")]
        public IActionResult Insertar(SalidasViewModel item)
        {
            var mapeo = _mapper.Map<tbSalidas>(item);
            var data = _inventarioServices.Salidas_Insertar(mapeo);
            return Ok(data);
        }

        [HttpPost("Actualizar")]
        public IActionResult Actualizar(SalidasViewModel item)
        {
            var mapeo = _mapper.Map<tbSalidas>(item);
            var data = _inventarioServices.Salidas_Actualizar(mapeo);
            return Ok(data);
        }
    }
}
