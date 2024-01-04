using Farsiman.Application.Core.Standard.DTOs;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;
using ProyectoGestionInventarioCAAG.Utility;

namespace ProyectoGestionInventarioCAAG._Features.Estados
{
    public class EstadosDomain
    {
        public Respuesta<bool> ValidarEstadoId(List<EstadosSalida> listaEstadosSalida, int estadoSalidaId)
        {
            var result = listaEstadosSalida.FirstOrDefault(x => x.EstadoSalidaId == estadoSalidaId);
            if (result == null)
                return Respuesta<bool>.Fault(OutputMessage.FaultEstadoSalidaNotExists);

            return Respuesta<bool>.Success(true);
        }
    }
}
