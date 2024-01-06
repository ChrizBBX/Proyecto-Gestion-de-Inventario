using Farsiman.Application.Core.Standard.DTOs;
using ProyectoGestionInventarioCAAG._Features.Estados;
using ProyectoGestionInventarioCAAG._Features.Productos;
using ProyectoGestionInventarioCAAG._Features.Sucursales;
using ProyectoGestionInventarioCAAG._Features.Usuarios;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;
using ProyectoGestionInventarioCAAG.Utility;

namespace ProyectoGestionInventarioCAAG._Features.Salidas
{
    public class SalidaDomain
    {
        UsuarioDomain usuarioDomain = new UsuarioDomain();
        SucursalDomain sucursalDomain = new SucursalDomain();
        ProductoDomain productoDomain = new ProductoDomain();
        public Respuesta<bool> ValidacionesSalidaInsertar(Salida entidad, List<Usuario> listaUsuario, List<Sucursal> listaSucursales,List<Producto> listaProductos,List<Salida> listaSalidas)
        {
            var validacionUsuarioId = usuarioDomain.ValidarUsuarioId(listaUsuario, entidad.UsuarioId);
            if (!validacionUsuarioId.Ok)
                return Respuesta<bool>.Fault($"{OutputMessage.FaultNotAdmin}, usuarioId: {entidad.UsuarioId}");

            var validarSucursalId = sucursalDomain.ValidarSucursalId(listaSucursales, entidad.SucursalId);
            if(!validarSucursalId.Ok)
                return Respuesta<bool>.Fault(validarSucursalId.Mensaje);

            var validarProductoId = productoDomain.ValidarProductoId(listaProductos, entidad.ProductoId);
            if (!validarProductoId.Ok)
                return Respuesta<bool>.Fault(validarProductoId.Mensaje);

            var validacionValorSucursal = ValidarValorSucursal(listaSalidas, entidad.SucursalId);
                if (!validacionValorSucursal.Ok)
                return Respuesta<bool>.Fault(validacionValorSucursal.Mensaje);

            return Respuesta<bool>.Success(true);
        }

        public Respuesta<bool> ValidacionesRecibirSalida(List<Salida> listaSalidas,List<Usuario> listaUsuarios, int salidaId, int usuarioId)
        {
            var validacionUsuarioId = usuarioDomain.ValidarUsuarioIdNotSalida(listaUsuarios, usuarioId);
            if (!validacionUsuarioId.Ok)
                return Respuesta<bool>.Fault($"{OutputMessage.FaultUserNotExists}, usuarioId: {usuarioId}");

            var validacionVerificarSalidaId = ValidarSalidaId(listaSalidas, salidaId);
            if (!validacionVerificarSalidaId.Ok)
                return Respuesta<bool>.Fault(validacionVerificarSalidaId.Mensaje);

            var validacionVerificarEstadoSalidaId = ValidarEstadoSalida(listaSalidas,salidaId);
            if (!validacionVerificarEstadoSalidaId.Ok)
                return Respuesta<bool>.Fault(validacionVerificarEstadoSalidaId.Mensaje);

            return Respuesta<bool>.Success(true);
        }

        public Respuesta<bool> ValidarValorSucursal(List<Salida> listaSalidas, int sucursalId)
        {
            decimal? valorEnSucursal = (from salida in listaSalidas
                                       where salida.SucursalId == sucursalId && salida.EstadoSalidaId == 1
                                       select salida.SalidaTotal
                                       ).Sum();

            if (valorEnSucursal > 5000)
                return Respuesta<bool>.Fault(OutputMessage.FaultSucursalMaxPendingValue);

            return Respuesta<bool>.Success(true);
        }

        public Respuesta<bool> ValidarSalidaId(List<Salida> listaSalidas, int salidaId)
        {
            Salida result = listaSalidas.FirstOrDefault(x => x.SalidaId == salidaId);
            if (result == null)
                return Respuesta<bool>.Fault($"{OutputMessage.FaultSalidaNotExists}, salidaId: {salidaId}");

            return Respuesta<bool>.Success(true);
        }

        public Respuesta<bool> ValidarEstadoSalida(List<Salida> listaSalidas,int salidaId)
        {
            Salida salida = listaSalidas.FirstOrDefault(x => x.SalidaId == salidaId);
            if (salida == null)
                return Respuesta<bool>.Fault($"{OutputMessage.FaultSalidaNotExists}, salidaId: {salidaId}");

            if (salida.EstadoSalidaId != 1)
                return Respuesta<bool>.Fault(OutputMessage.FaultSalidaAlreadyReceived);

            return Respuesta<bool>.Success(true);
        }

    }
}
