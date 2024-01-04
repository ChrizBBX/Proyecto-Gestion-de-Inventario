using Farsiman.Application.Core.Standard.DTOs;
using ProyectoGestionInventarioCAAG._Features.Lotes.Dtos;
using ProyectoGestionInventarioCAAG._Features.Productos;
using ProyectoGestionInventarioCAAG._Features.Usuarios;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;

namespace ProyectoGestionInventarioCAAG._Features.Lotes
{
    public class LoteDomain
    {
        UsuarioDomain usuarioDomain = new UsuarioDomain();
        ProductoDomain productoDomain = new ProductoDomain();
        public Respuesta<bool> ValidacionesLotesInsertar(LoteDto entidad,List<Usuario> listaUsuario,List<Producto> listaProductos)
        {

            var ValidacionUsuarioId = usuarioDomain.ValidarUsuarioCreacion(listaUsuario, entidad.UsuarioCreacion);
            if (!ValidacionUsuarioId.Ok)
                return Respuesta<bool>.Fault(ValidacionUsuarioId.Mensaje);

            var ValidacionProductoId = productoDomain.ValidarProductoId(listaProductos, entidad.ProductoId);
            if (!ValidacionProductoId.Ok)
                return Respuesta<bool>.Fault(ValidacionProductoId.Mensaje);

            return Respuesta<bool>.Success(true);
        }
    }
}
        