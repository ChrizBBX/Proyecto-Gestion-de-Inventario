using Farsiman.Application.Core.Standard.DTOs;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;
using ProyectoGestionInventarioCAAG.Utility;

namespace ProyectoGestionInventarioCAAG._Features.Sucursales
{
    public class SucursalDomain
    {
        public Respuesta<bool> ValidarSucursalId (List<Sucursal> listaSucursales, int sucursalId)
        {
            var result = listaSucursales.FirstOrDefault(x => x.SucursalId == sucursalId);
            if (result == null)
                return Respuesta<bool>.Fault(OutputMessage.FaultSucursalNotExists);

            return Respuesta<bool>.Success(true);
        }
    }
}
