using AutoMapper;
using ProyectoGestionInventarioCAAG._Features.Empleados.Dtos;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;

namespace ProyectoGestionInventarioCAAG.Infraestructure.Inventario
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<EmpleadoDto, Empleado>().ReverseMap();
        }
    }
}
