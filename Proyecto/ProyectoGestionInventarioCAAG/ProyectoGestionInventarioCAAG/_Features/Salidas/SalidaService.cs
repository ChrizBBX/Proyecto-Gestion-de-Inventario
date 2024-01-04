using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;
using ProyectoGestionInventarioCAAG._Features.Salidas.Dtos;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;
using ProyectoGestionInventarioCAAG.Utility;

namespace ProyectoGestionInventarioCAAG._Features.Salidas
{
    public class SalidaService
    {
        private readonly IMapper _mapper;
        private readonly SalidaDomain _salidaDomain;
        private readonly IUnitOfWork _unitOfWork;

        public SalidaService(IMapper mapper, SalidaDomain salidaDomain, UnitOfWorkBuilder unitOfWork)
        {
            _mapper = mapper;
            _salidaDomain = salidaDomain;
            _unitOfWork = unitOfWork.BuilderProyectoGestionInventarioCAAG();
        }

        public Respuesta<string> InsertarSalida(SalidaDto entidad)
        {
            var objeto = _mapper.Map<Salida>(entidad);


           return Respuesta<string>.Success(OutputMessage.SuccessInsertSalida);
        }
    }
}
