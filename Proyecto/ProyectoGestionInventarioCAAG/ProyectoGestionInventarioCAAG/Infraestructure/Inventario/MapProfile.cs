using AutoMapper;
using ProyectoGestionInventarioCAAG._Features.Empleados.Dtos;
using ProyectoGestionInventarioCAAG._Features.Lotes.Dtos;
using ProyectoGestionInventarioCAAG._Features.Usuarios.Dtos;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;

namespace ProyectoGestionInventarioCAAG.Infraestructure.Inventario
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<UsuarioDto, Usuario>().ReverseMap();
            CreateMap<EmpleadoDto, Empleado>().ReverseMap();
            CreateMap<LoteDto,Lote>().ReverseMap();
        }
    }
}
