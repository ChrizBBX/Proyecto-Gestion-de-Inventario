using API_ProyectoGestionInventario.Models.ModelsAcceso;
using API_ProyectoGestionInventario.Models.ModelsInventario;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoGestionInventario.BusinessLogic.Services.AccesoServices;
using ProyectoGestionInventario.BusinessLogic.Services.InventarioServices;
using ProyectoGestionInventario.Entities.Entities;

namespace API_ProyectoGestionInventario.Controllers.ControllersInventario
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly InventarioServices _inventarioServices;
        private IMapper _mapper;

        public ProductosController(InventarioServices inventarioServices, IMapper mapper)
        {
            _inventarioServices = inventarioServices;
            _mapper = mapper;
        }

        [HttpGet("Listar")]
        public IActionResult Listar()
        {
            var data = _inventarioServices.Productos_Listar();
            var datos = _mapper.Map<IEnumerable<ProductosViewModel>>(data.Data);
            return Ok(datos);
        }

        [HttpPost("Insertar")]
        public IActionResult Insertar(ProductosViewModel item)
        {
            var mapeo = _mapper.Map<tbProductos>(item);
            var data = _inventarioServices.Productos_Insertar(mapeo);
            return Ok(data);
        }

        [HttpPost("Actualizar")]
        public IActionResult Actualizar(ProductosViewModel item)
        {
            var mapeo = _mapper.Map<tbProductos>(item);
            var data = _inventarioServices.Productos_Actualizar(mapeo);
            return Ok(data);
        }

        [HttpPost("Eliminar")]
        public IActionResult Eliminar(ProductosViewModel item)
        {
            var mapeo = _mapper.Map<tbProductos>(item);
            var data = _inventarioServices.Productos_Eliminar(mapeo);
            return Ok(data);
        }
    }
}
