using Farsiman.Application.Core.Standard.DTOs;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;
using ProyectoGestionInventarioCAAG.Utility;

namespace ProyectoGestionInventarioCAAG._Features.Perfiles
{
    public class PerfilDomain
    {
        public Respuesta<bool> ValidarPerfilId(List<Perfil> listaPerfiles, int perfilId)
        {
            var result = listaPerfiles.FirstOrDefault(x => x.PerfilId == perfilId);
            if (result == null)
                return Respuesta<bool>.Fault(OutputMessage.FaultPerfilNotExists);
            else
                return Respuesta<bool>.Success(true);
        }
    }
}
