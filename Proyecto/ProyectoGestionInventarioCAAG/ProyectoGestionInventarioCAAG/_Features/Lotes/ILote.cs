using Farsiman.Application.Core.Standard.DTOs;
using ProyectoGestionInventarioCAAG._Features.Lotes.Dtos;

namespace ProyectoGestionInventarioCAAG._Features.Lotes
{
    public interface ILote <T>
    {
        public Respuesta<List<LoteDto>> ListadoLotes();
        public Respuesta<string> InsertarLote(LoteDto entidad);
    }
}
