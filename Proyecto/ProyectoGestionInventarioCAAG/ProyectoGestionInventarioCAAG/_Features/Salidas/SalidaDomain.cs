using Farsiman.Application.Core.Standard.DTOs;
using ProyectoGestionInventarioCAAG._Features.Estados;
using ProyectoGestionInventarioCAAG._Features.Productos;
using ProyectoGestionInventarioCAAG._Features.Sucursales;
using ProyectoGestionInventarioCAAG._Features.Usuarios;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;

namespace ProyectoGestionInventarioCAAG._Features.Salidas
{
    public class SalidaDomain
    {
        UsuarioDomain usuarioDomain = new UsuarioDomain();
        SucursalDomain sucursalDomain = new SucursalDomain();
        ProductoDomain productoDomain = new ProductoDomain();
        public Respuesta<bool> ValidacionesSalidaInsertar(Salida entidad, List<Usuario> listaUsuario, List<Sucursal> listaSucursales,List<Producto> listaProductos)
        {
            var validacionUsuarioId = usuarioDomain.ValidarUsuarioId(listaUsuario, entidad.UsuarioId);
            if (!validacionUsuarioId.Ok)
                return Respuesta<bool>.Fault(validacionUsuarioId.Mensaje);

            var validarSucursalId = sucursalDomain.ValidarSucursalId(listaSucursales, entidad.SucursalId);
            if(!validarSucursalId.Ok)
                return Respuesta<bool>.Fault(validarSucursalId.Mensaje);

            var validarProductoId = productoDomain.ValidarProductoId(listaProductos, entidad.ProductoId);
            if (!validarProductoId.Ok)
                return Respuesta<bool>.Fault(validarProductoId.Mensaje);

            return Respuesta<bool>.Success(true);
        }

    }
}
