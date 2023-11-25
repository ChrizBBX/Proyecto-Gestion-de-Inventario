using API_ProyectoGestionInventario.Models.ModelsAcceso;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoGestionInventario.BusinessLogic.Services.AccesoServices;
using ProyectoGestionInventario.Entities.Entities;

namespace API_ProyectoGestionInventario.Controllers.ControllersAcceso
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AccesoServices _accesoServices;
        private readonly IMapper _mapper;

        public UsuariosController(AccesoServices accesoServices, IMapper mapper)
        {
            _accesoServices = accesoServices;
            _mapper = mapper;
        }

        [HttpPost("Login")]
        public IActionResult Insertar(string usuario,string contrasenia)
        {
            var data = _accesoServices.Login(usuario, contrasenia);
            var datos = _mapper.Map<UsuariosViewModel>(data.Data);
            return Ok(datos);
        }
    }
}
