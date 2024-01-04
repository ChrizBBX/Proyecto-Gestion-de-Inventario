using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;
using FluentValidation;
using FluentValidation.Results;
using ProyectoGestionInventarioCAAG._Features.Lotes.Dtos;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;
using ProyectoGestionInventarioCAAG.Utility;
using System.Linq.Expressions;
using static ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities.Lote;
using static ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities.Usuario;

namespace ProyectoGestionInventarioCAAG._Features.Lotes
{
    public class LoteService : ILote<LoteDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly LoteDomain _loteDomain;

        public LoteService(IMapper mapper, UnitOfWorkBuilder unitOfWork, LoteDomain loteDomain)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork.BuilderProyectoGestionInventarioCAAG();
            _loteDomain = loteDomain;
        }

        public Respuesta<List<LoteDto>> ListadoLotes()
        {
            var result = (from lote in _unitOfWork.Repository<Lote>().AsQueryable()
                          where lote.LoteCantidad > 0
                          select new LoteDto
                          {
                            LoteId = lote.LoteId,
                            LoteCantidad = lote.LoteCantidad,
                            LoteCostoCantidad = lote.LoteCostoCantidad,
                            LoteFechaVencimiento = lote.LoteFechaVencimiento
                          }
                           ).ToList();

            return Respuesta.Success(result);
        }

        public Respuesta<string> InsertarLote(LoteDto entidad)
        {
            var objeto = _mapper.Map<Lote>(entidad);
            LoteValidations validator = new LoteValidations();
            validator.RuleFor(x => x.UsuarioCreacion).NotEmpty().GreaterThan(0);
            ValidationResult validationResult = validator.Validate(objeto);

            if (!validationResult.IsValid)
            {
                IEnumerable<string> Errors = validationResult.Errors.Select(s => s.ErrorMessage);
                string menssageValidation = string.Join(Environment.NewLine, Errors);
                return Respuesta.Fault<string>(menssageValidation, OutputMessage.FaultUserEntity);
            }
            var listaUsuarios = _unitOfWork.Repository<Usuario>().AsQueryable().ToList();
            var listaProductos = _unitOfWork.Repository<Producto>().AsQueryable().ToList();

            var validacionesLotesInsertar = _loteDomain.ValidacionesLotesInsertar(entidad,listaUsuarios,listaProductos);
            if (!validacionesLotesInsertar.Ok)
                return Respuesta<string>.Fault(validacionesLotesInsertar.Mensaje);

            objeto.LoteCantidadInicial = objeto.LoteCantidad;
            objeto.FechaCreacion = DateTime.Now;
            objeto.FechaModificacion = null;
            objeto.UsuarioModificacion = null;

            _unitOfWork.Repository<Lote>().Add(objeto);
            _unitOfWork.SaveChanges();

            return Respuesta<string>.Success(OutputMessage.SuccessInsertLote);
        }
    }
}
