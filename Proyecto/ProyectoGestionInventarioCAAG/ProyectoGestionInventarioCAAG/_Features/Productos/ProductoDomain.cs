using Farsiman.Application.Core.Standard.DTOs;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;
using ProyectoGestionInventarioCAAG.Utility;

namespace ProyectoGestionInventarioCAAG._Features.Productos
{
    public class ProductoDomain
    {
        public Respuesta<bool> ValidarProductoId(List<Producto> listaProductos, int? productoId)
        {
            var result = listaProductos.FirstOrDefault(x => x.ProductoId == productoId);
            if (result == null)
                return Respuesta<bool>.Fault(OutputMessage.FaultProductoNotExists);

            return Respuesta<bool>.Success(true);
        }
    }
}
