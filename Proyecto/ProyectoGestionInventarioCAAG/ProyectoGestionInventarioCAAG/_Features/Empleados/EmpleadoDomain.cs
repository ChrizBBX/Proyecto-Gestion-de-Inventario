using Farsiman.Application.Core.Standard.DTOs;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;
using ProyectoGestionInventarioCAAG.Utility;

namespace ProyectoGestionInventarioCAAG._Features.Empleados
{
    public class EmpleadoDomain
    {

        public Respuesta<bool> ValidarEmpleadoId(List<Empleado> listaEmpelados, int empleadoId)
        {
            var result = listaEmpelados.FirstOrDefault(x => x.EmpleadoId == empleadoId);
            if (result == null)
                return Respuesta<bool>.Fault(OutputMessage.FaultEmpleadoNotExists);
            else
                return Respuesta<bool>.Success(true);
        }

        public Respuesta<bool> ValidacionesInsertarEmpleado()
        {

            return Respuesta<bool>.Success(true);
        }
    }
}
