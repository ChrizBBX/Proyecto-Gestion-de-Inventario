using API_ProyectoGestionInventario.Models.ModelsInventario;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoGestionInventario.BusinessLogic.Services.InventarioServices;

namespace API_ProyectoGestionInventario.Controllers.ControllersInventario
{
    [Route("api/[controller]")]
    [ApiController]
    public class LotesController : ControllerBase
    {
        private readonly InventarioServices _inventarioServices;
        private IMapper _mapper;

        public LotesController(InventarioServices inventarioServices, IMapper mapper)
        {
            _inventarioServices = inventarioServices;
            _mapper = mapper;
        }

        [HttpGet("Listar")]
        public IActionResult Listar()
        {
            var data = _inventarioServices.Lotes_Listar();
            //var datos = _mapper.Map<IEnumerable<LotesViewModel>>(data.Data);
            return Ok(data);
        }

        [HttpGet("ListarPorProducto")]
        public IActionResult Listar_PorProducto(int Id)
        {
            var data = _inventarioServices.Lotes_ListarPorProducto(Id);
            //var datos = _mapper.Map<IEnumerable<LotesViewModel>>(data.Data);
            return Ok(data);
        }
    }
}
