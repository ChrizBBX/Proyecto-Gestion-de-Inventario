using Farsiman.Application.Core.Standard.DTOs;
using ProyectoGestionInventarioCAAG._Features.Salidas.Dtos;

namespace ProyectoGestionInventarioCAAG._Features.Salidas
{
    public interface ISalida <T>
    {
        public Respuesta<string> InsertarSalida(SalidaDto entidad);
        public Respuesta<string> RecibirSalida(SalidaRecibirDto entidad);
        public Respuesta<List<SalidaReporteDto>> Reporte(DateTime? fechaInicio, DateTime? fechaFin, int? sucursalId);
    }
}
