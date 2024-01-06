using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using ProyectoGestionInventarioCAAG._Features.Salidas.Dtos;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;
using ProyectoGestionInventarioCAAG.Utility;
using static ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities.Salida;

namespace ProyectoGestionInventarioCAAG._Features.Salidas
{
    public class SalidaService : ISalida<SalidaDto>
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

                var listaSaldias = _unitOfWork.Repository<Salida>().AsQueryable().ToList();
                var listaUsuarios = _unitOfWork.Repository<Usuario>().AsQueryable().ToList();
                var listaSucursales = _unitOfWork.Repository<Sucursal>().AsQueryable().ToList();
                var listaProductos = _unitOfWork.Repository<Producto>().AsQueryable().ToList();

                var validacionesInsertarSalida = _salidaDomain.ValidacionesSalidaInsertar(salida, listaUsuarios, listaSucursales,listaProductos,listaSaldias);
                if (!validacionesInsertarSalida.Ok)
                    return Respuesta<string>.Fault(validacionesInsertarSalida.Mensaje);

                _unitOfWork.Repository<Salida>().Add(salida);
                _unitOfWork.SaveChanges();
                int id = salida.SalidaId;

                List<SalidasDetalle> salidasDetalles = new List<SalidasDetalle>();

                List<Lote> lotesDisponibles = _unitOfWork.Repository<Lote>().AsQueryable().OrderBy(x => x.LoteFechaVencimiento).Where(x => x.ProductoId == entidad.ProductoId && x.LoteCantidad > 0).ToList();
                if (lotesDisponibles.Count == 0)
                    return Respuesta<string>.Fault(OutputMessage.FaultLotesProductNotStock);
                
                int stockDisponible = (from stock in lotesDisponibles
                                       select stock.LoteCantidad).Sum();

                if (entidad.Cantidad > stockDisponible)
                    return Respuesta<string>.Fault($"{OutputMessage.FaultNotEnoughStock}, Cantidad Solicitada: {entidad.Cantidad}, Stock Disponible: {stockDisponible}");

                int cantidadRestante = entidad.Cantidad;
                decimal? total = 0;

                    foreach (var item in lotesDisponibles)
                    {
                    if (cantidadRestante > 0)
                    {
                        if (cantidadRestante > item.LoteCantidad)
                        {
                            Lote lote = _unitOfWork.Repository<Lote>().FirstOrDefault(x => x.LoteId == item.LoteId);
                            total += lote.LoteCostoCantidad * lote.LoteCantidad;
                            cantidadRestante = cantidadRestante - lote.LoteCantidad;

                            SalidasDetalle detalle = new SalidasDetalle
                            {
                                SalidaId = id,
                                LoteId = item.LoteId,
                                SalidaDetalleCantidad = lote.LoteCantidad,
                                UsuarioCreacion = entidad.UsuarioId,
                                FechaCreacion = DateTime.Now,
                                UsuarioModificacion = null,
                                FechaModificacion = null,
                            };
                            salidasDetalles.Add(detalle);
                            lote.LoteCantidad = 0;
                            _unitOfWork.Repository<Lote>().Update(lote);
                        }
                        else
                        {
                            Lote lote = _unitOfWork.Repository<Lote>().FirstOrDefault(x => x.LoteId == item.LoteId);
                            total += lote.LoteCostoCantidad * cantidadRestante;
                            lote.LoteCantidad = lote.LoteCantidad - cantidadRestante;
                            SalidasDetalle detalle = new SalidasDetalle
                            {
                                SalidaId = id,
                                LoteId = item.LoteId,
                                SalidaDetalleCantidad = cantidadRestante,
                                UsuarioCreacion = entidad.UsuarioId,
                                FechaCreacion = DateTime.Now,
                                UsuarioModificacion = null,
                                FechaModificacion = null,
                            };
                            salidasDetalles.Add(detalle);
                            cantidadRestante = 0;
                            _unitOfWork.Repository<Lote>().Update(lote);
                        }
                    }
                    else
                        break;
                    }

                foreach (var item in salidasDetalles)
                {
                    _unitOfWork.Repository<SalidasDetalle>().Add(item);
                }

                salida.SalidaTotal = total;
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

        public Respuesta<string> RecibirSalida(SalidaRecibirDto entidad)
        {
            List<Usuario> listaUsuarios = _unitOfWork.Repository<Usuario>().AsQueryable().ToList();
            List<Salida> listaSalidas = _unitOfWork.Repository<Salida>().AsQueryable().ToList();

            var validacionesRecibirSalida = _salidaDomain.ValidacionesRecibirSalida(listaSalidas, listaUsuarios, entidad.salidaId, entidad.usuarioId);
            if (!validacionesRecibirSalida.Ok)
                return Respuesta<string>.Fault(validacionesRecibirSalida.Mensaje);

            Salida salida = listaSalidas.FirstOrDefault(x => x.SalidaId == entidad.salidaId) ?? new();
            salida.FechaModificacion =  DateTime.Now;
            salida.UsuarioModificacion = entidad.usuarioId;
            salida.EstadoSalidaId = 2;
            _unitOfWork.Repository<Salida>().Update(salida);
            _unitOfWork.SaveChanges();

            return Respuesta<string>.Success(OutputMessage.SuccessReceiveSalida);
        }

        public Respuesta<List<SalidaReporteDto>> Reporte(DateTime? fechaInicio, DateTime? fechaFin, int? sucursalId)
        {
            if(sucursalId > 0)
            {
                List<SalidaReporteDto> reporte = (from salida in _unitOfWork.Repository<Salida>().AsQueryable()
                                                  where salida.SalidaFecha >= fechaInicio && salida.SalidaFecha <= fechaFin && salida.SucursalId == sucursalId
                                                  select new SalidaReporteDto
                                                  {
                                                      SalidaId = salida.SalidaId,
                                                      SalidaFecha = salida.SalidaFecha,
                                                      UnidadesTotales = (from detalles in _unitOfWork.Repository<SalidasDetalle>().AsQueryable()
                                                                         where detalles.SalidaId == salida.SalidaId
                                                                         select detalles.SalidaDetalleCantidad
                                                                         ).Sum(),
                                                      SalidaTotal = salida.SalidaTotal,
                                                      EstadoSalidaId = salida.EstadoSalidaId,
                                                      UsuarioId = salida.UsuarioId,
                                                      FechaModificacion = salida.FechaModificacion,
                                                      salidaDetalles = (from detalles in _unitOfWork.Repository<SalidasDetalle>().AsQueryable()
                                                                        where detalles.SalidaId == salida.SalidaId
                                                                        select detalles
                                                                         ).ToList(),
                                                  }
                                  ).ToList();
                return Respuesta.Success(reporte);
            }
            else
            {
                List<SalidaReporteDto> reporte = (from salida in _unitOfWork.Repository<Salida>().AsQueryable()
                                                  where salida.SalidaFecha >= fechaInicio && salida.SalidaFecha <= fechaFin
                                                  select new SalidaReporteDto
                                                  {
                                                      SalidaId = salida.SalidaId,
                                                      SalidaFecha = salida.SalidaFecha,
                                                      UnidadesTotales = (from detalles in _unitOfWork.Repository<SalidasDetalle>().AsQueryable()
                                                                         where detalles.SalidaId == salida.SalidaId
                                                                         select detalles.SalidaDetalleCantidad
                                                                         ).Sum(),
                                                      SalidaTotal = salida.SalidaTotal,
                                                      EstadoSalidaId = salida.EstadoSalidaId,
                                                      UsuarioId = salida.UsuarioId,
                                                      FechaModificacion = salida.FechaModificacion,
                                                      salidaDetalles = (from detalles in _unitOfWork.Repository<SalidasDetalle>().AsQueryable()
                                                                        where detalles.SalidaId == salida.SalidaId
                                                                        select detalles
                                                                         ).ToList(),
                                                  }
                                  ).ToList();
                return Respuesta.Success(reporte);
            }
        }
    }
}
