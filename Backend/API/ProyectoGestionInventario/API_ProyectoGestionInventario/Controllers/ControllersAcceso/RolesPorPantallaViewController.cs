using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoGestionInventario.BusinessLogic.Services.AccesoServices;

namespace API_ProyectoGestionInventario.Controllers.ControllersAcceso
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesPorPantallaViewController : ControllerBase
    {
        private readonly AccesoServices _accesoServices;
        private IMapper mapper;

        public RolesPorPantallaViewController(AccesoServices accesoServices, IMapper mapper)
        {
            _accesoServices = accesoServices;
            this.mapper = mapper;
        }

    }
}
