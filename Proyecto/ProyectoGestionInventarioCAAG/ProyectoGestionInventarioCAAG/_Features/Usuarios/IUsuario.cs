using Farsiman.Application.Core.Standard.DTOs;
using ProyectoGestionInventarioCAAG._Features.Usuarios.Dtos;

namespace ProyectoGestionInventarioCAAG._Features.Usuarios
{
    public interface IUsuario <T>
    {
        public Respuesta<List<UsuarioDto>> ListadoUsuarios();
        public Respuesta<string> InsertarUsuarios(UsuarioDto entidad);
        public Respuesta<List<UsuarioDto>> InicioSesion(UsuarioDto entidad);
    }
}
