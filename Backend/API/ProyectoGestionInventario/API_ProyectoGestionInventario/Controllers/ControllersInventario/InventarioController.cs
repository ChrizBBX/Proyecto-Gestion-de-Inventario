using API_ProyectoGestionInventario.Models.ModelsInventario;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoGestionInventario.BusinessLogic.Services.InventarioServices;

namespace API_ProyectoGestionInventario.Controllers.ControllersInventario
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly InventarioServices _inventarioServices;
        private readonly IMapper _mapper;

        public InventarioController(InventarioServices inventarioServices, IMapper mapper)
        {
            _inventarioServices = inventarioServices;
            _mapper = mapper;
        }

        [HttpGet("Listar")]
        public IActionResult Index()
        {
            var listado = _inventarioServices.ListarProductos();

            listado.Data = _mapper.Map<IEnumerable<ProductosViewModel>>(listado.Data);

            return Ok(listado);
        }
    }
}
