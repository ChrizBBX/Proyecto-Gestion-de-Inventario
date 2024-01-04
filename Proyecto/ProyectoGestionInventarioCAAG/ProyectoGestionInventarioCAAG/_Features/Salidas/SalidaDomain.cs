using Farsiman.Application.Core.Standard.DTOs;
using ProyectoGestionInventarioCAAG._Features.Estados;
using ProyectoGestionInventarioCAAG._Features.Sucursales;
using ProyectoGestionInventarioCAAG._Features.Usuarios;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;

namespace ProyectoGestionInventarioCAAG._Features.Salidas
{
    public class SalidaDomain
    {
        UsuarioDomain usuarioDomain = new UsuarioDomain();
        SucursalDomain sucursalDomain = new SucursalDomain();
        EstadosDomain estadosDomain = new EstadosDomain();
        public Respuesta<bool> ValidacionesSalidaInsertar(Salida entidad, List<Usuario> listaUsuario, List<Sucursal> listaSucursales,List<EstadosSalida> listaEstadosSalida)
        {
            var validacionUsuarioCreacion = usuarioDomain.ValidarUsuarioCreacion(listaUsuario,entidad.UsuarioCreacion);
            if (!validacionUsuarioCreacion.Ok)
                return Respuesta<bool>.Fault(validacionUsuarioCreacion.Mensaje);

            var validacionUsuarioId = usuarioDomain.ValidarUsuarioId(listaUsuario, entidad.UsuarioCreacion);
            if (!validacionUsuarioId.Ok)
                return Respuesta<bool>.Fault(validacionUsuarioId.Mensaje);

            var validarSucursalId = sucursalDomain.ValidarSucursalId(listaSucursales, entidad.SucursalId);
            if(!validarSucursalId.Ok)
                return Respuesta<bool>.Fault(validarSucursalId.Mensaje);

            var validarEstadoSalida = estadosDomain.ValidarEstadoId(listaEstadosSalida, entidad.EstadoId);
            if (!validarEstadoSalida.Ok)
                return Respuesta<bool>.Fault(validarEstadoSalida.Mensaje);

            return Respuesta<bool>.Success(true);
        }
    }
}
