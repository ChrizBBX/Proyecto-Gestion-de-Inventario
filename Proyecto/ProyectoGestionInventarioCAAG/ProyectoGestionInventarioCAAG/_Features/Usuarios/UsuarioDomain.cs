using Farsiman.Application.Core.Standard.DTOs;
using ProyectoGestionInventarioCAAG._Features.Empleados;
using ProyectoGestionInventarioCAAG._Features.Perfiles;
using ProyectoGestionInventarioCAAG._Features.Productos;
using ProyectoGestionInventarioCAAG._Features.Usuarios.Dtos;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;
using ProyectoGestionInventarioCAAG.Utility;

namespace ProyectoGestionInventarioCAAG._Features.Usuarios
{
    public class UsuarioDomain
    {
        PerfilDomain perfilDomain = new PerfilDomain();
        EmpleadoDomain empleadoDomain = new EmpleadoDomain();

        public Respuesta<bool> ValidarUsuarioId(List<Usuario> listaUsuarios, int usuarioId)
        {
            var result = listaUsuarios.FirstOrDefault(x => x.UsuarioId == usuarioId);
            if (result == null)
                return Respuesta<bool>.Fault(OutputMessage.FaultCreatorUser);
            else
                return Respuesta<bool>.Success(true);
        }

        public Respuesta<bool> ValidarUsuarioCreacion(List<Usuario> listaUsuarios, int usuarioId)
        {
            var result = listaUsuarios.FirstOrDefault(x => x.UsuarioId == usuarioId);
            if (result == null)
                return Respuesta<bool>.Fault(OutputMessage.FaultCreatorUser);
            else
                return Respuesta<bool>.Success(true);
        }

        public Respuesta<bool> UsuarioNombreDisponible(List<Usuario> listaUsuarios, string usuarioNombreUsuario) 
        {
            var result = listaUsuarios.FirstOrDefault(x => x.UsuarioNombreUsuario == usuarioNombreUsuario);
            if (result != null)
                return Respuesta<bool>.Fault(OutputMessage.FaultUserExists);
            else
                return Respuesta<bool>.Success(true);
        }

        public Respuesta<bool> ValidacionesInsertarUsuarios(UsuarioDto entidad,List<Usuario> listaUsuarios,List<Perfil> listaPerfiles,List<Empleado> listaEmpleados)
        {

            var ValidacionUsuarioCreacion = ValidarUsuarioCreacion(listaUsuarios, entidad.UsuarioCreacion);
            if (!ValidacionUsuarioCreacion.Ok)
                return Respuesta<bool>.Fault(ValidacionUsuarioCreacion.Mensaje);

            var ValidacionPerfilId = perfilDomain.ValidarPerfilId(listaPerfiles,entidad.PerfilId);
            if (!ValidacionPerfilId.Ok)
                return Respuesta<bool>.Fault(ValidacionPerfilId.Mensaje);

            var ValidacionEmpleadoId = empleadoDomain.ValidarEmpleadoId(listaEmpleados, entidad.EmpleadoId);
            if (!ValidacionEmpleadoId.Ok)
                return Respuesta<bool>.Fault(ValidacionEmpleadoId.Mensaje);

            var ValidacionNombreUsuarioDisponible = UsuarioNombreDisponible(listaUsuarios, entidad.UsuarioNombreUsuario);
            if (!ValidacionNombreUsuarioDisponible.Ok)
                return Respuesta<bool>.Fault(ValidacionNombreUsuarioDisponible.Mensaje);

            return Respuesta<bool>.Success(true);
        }
    }
}
