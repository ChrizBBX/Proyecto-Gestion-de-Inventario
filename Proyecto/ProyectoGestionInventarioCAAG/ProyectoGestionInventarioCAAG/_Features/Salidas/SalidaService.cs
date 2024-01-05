using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;
using FluentValidation.Results;
using ProyectoGestionInventarioCAAG._Features.Salidas.Dtos;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;
using ProyectoGestionInventarioCAAG.Utility;
using static ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities.Salida;

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
            try
            {
                _unitOfWork.BeginTransaction();
                Salida salida = new Salida
                {
                    SalidaId = 0,
                    ProductoId = entidad.ProductoId,
                    SucursalId = entidad.SucursalId,
                    UsuarioId = entidad.UsuarioId,
                    SalidaFecha = entidad.SalidaFecha,

                    SalidaFechaRecibido = null,
                    SalidaTotal = null,
                    EstadoSalidaId = 1,
                    UsuarioCreacion = entidad.UsuarioId,
                    FechaCreacion = DateTime.Now,
                    UsuarioModificacion = null,
                    FechaModificacion = null
                };

                SalidaValidations validator = new SalidaValidations();
                ValidationResult validationResult = validator.Validate(salida);

                if (!validationResult.IsValid)
                {
                    IEnumerable<string> Errors = validationResult.Errors.Select(s => s.ErrorMessage);
                    string menssageValidation = string.Join(Environment.NewLine, Errors);
                    return Respuesta.Fault<string>(menssageValidation, OutputMessage.FaultSalidaEntity);
                }

                var listaUsuarios = _unitOfWork.Repository<Usuario>().AsQueryable().ToList();
                var listaSucursales = _unitOfWork.Repository<Sucursal>().AsQueryable().ToList();
                var listaProductos = _unitOfWork.Repository<Producto>().AsQueryable().ToList();

                var validacionesInsertarSalida = _salidaDomain.ValidacionesSalidaInsertar(salida, listaUsuarios, listaSucursales,listaProductos);
                if (!validacionesInsertarSalida.Ok)
                    return Respuesta<string>.Fault(validacionesInsertarSalida.Mensaje);

                _unitOfWork.Repository<Salida>().Add(salida);
                _unitOfWork.SaveChanges();
                int id = salida.SalidaId;

                List<SalidasDetalle> salidasDetalles = new List<SalidasDetalle>();

                var lotesDisponibles = _unitOfWork.Repository<Lote>().AsQueryable().OrderBy(x => x.LoteFechaVencimiento).Where(x => x.ProductoId == entidad.ProductoId).ToList();
                int cantidadRestante = entidad.Cantidad;


                    foreach (var item in lotesDisponibles)
                    {
                        if (cantidadRestante > item.LoteCantidad)
                        {
                            Lote lote = _unitOfWork.Repository<Lote>().FirstOrDefault(x => x.LoteId == item.LoteId);
                            cantidadRestante = cantidadRestante - lote.LoteCantidad;
                            lote.LoteCantidad = 0;
                            _unitOfWork.Repository<Lote>().Update(lote);
                        }
                        else
                        {
                            Lote lote = _unitOfWork.Repository<Lote>().FirstOrDefault(x => x.LoteId == item.LoteId);
                            lote.LoteCantidad = lote.LoteCantidad - cantidadRestante;
                            cantidadRestante = 0;
                            _unitOfWork.Repository<Lote>().Update(lote);
                        }
                    }

                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
                return Respuesta<string>.Success(OutputMessage.SuccessInsertSalida);
            }
            catch(Exception ex)
            {
                _unitOfWork.RollBack();
                return Respuesta<string>.Fault($"{OutputMessage.FaultInsertSalida}, error: {ex}");
            }
        }
    }
}
