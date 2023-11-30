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
    public class SalidasViewController : ControllerBase
    {
        private readonly InventarioServices _inventarioServices;
        private IMapper _mapper;

        public SalidasViewController(InventarioServices inventarioServices, IMapper mapper)
        {
            _inventarioServices = inventarioServices;
            _mapper = mapper;
        }


        [HttpGet("Listar")]
        public IActionResult Listar()
        {
            var data = _inventarioServices.SalidasView_Listar();
            var datos = _mapper.Map<IEnumerable<VW_tbSalidas>>(data.Data);
            return Ok(datos);
        }

        [HttpGet("Listar_Filtrado")]
        public IActionResult Listar_Filtrado(int? sucu, string? inicio, string? fin)
        {
            var data = _inventarioServices.SalidasView_Listar_Filtered(sucu, inicio, fin);
            return Ok(data);
        }

        [HttpGet("Sucursal_Status")]
        public IActionResult Sucursal_Status(int Id)
        {
            var data = _inventarioServices.Sucursal_Status(Id);
            return Ok(data);
        }
    }
}
