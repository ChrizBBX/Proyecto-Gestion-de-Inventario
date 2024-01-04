using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProyectoGestionInventarioCAAG._Features.Usuarios;
using ProyectoGestionInventarioCAAG._Features.Usuarios.Dtos;

namespace ProyectoGestionInventarioCAAG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService= usuarioService;
        }

        [HttpGet("ListadoUsuarios")]
        public IActionResult ListadoUsuarios()
        {
            var result = _usuarioService.ListadoUsuarios();
            return Ok(result);
        }

        [HttpPost("InsertarUsuarios")]
        public IActionResult InsertarUsuarios(UsuarioDto usuario)
        {
            var result = _usuarioService.InsertarUsuarios(usuario);
            return Ok(result);
        }

        [HttpPost("InicioSesion")]
        public IActionResult InicioSesion(UsuarioDto usuario)
        {
            var result = _usuarioService.InicioSesion(usuario);
            return Ok(result);
        }
    }
}