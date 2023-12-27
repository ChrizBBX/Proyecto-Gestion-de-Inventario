using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;
using FluentValidation;
using FluentValidation.Results;
using ProyectoGestionInventarioCAAG._Features.Empleados.Dtos;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;
using ProyectoGestionInventarioCAAG.Utility;
using static ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities.Empleado;

namespace ProyectoGestionInventarioCAAG._Features.Empleados
{
    public class EmpleadoService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EmpleadoService(IMapper mapper, UnitOfWorkBuilder unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork.BuilderProyectoGestionInventarioCAAG();
        }

        public Respuesta<List<EmpleadoDto>> ObtenerEmpleados()
        {
            var result = (from empleado in _unitOfWork.Repository<Empleado>().AsQueryable()
                          where empleado.Activo == true
                          select new EmpleadoDto
                          {
                              EmpleadoId = empleado.EmpleadoId,
                              EmpleadoNombre = empleado.EmpleadoNombre,
                              EmpleadoApellido = empleado.EmpleadoApellido,
                              EmpleadoFechaNacimiento = empleado.EmpleadoFechaNacimiento,
                              EmpleadoSexo = empleado.EmpleadoSexo,
                              EmpleadoTelefono = empleado.EmpleadoTelefono,
                              UsuarioCreacion = empleado.UsuarioCreacion,
                              FechaCreacion = empleado.FechaCreacion,
                              UsuarioModificacion = empleado.UsuarioModificacion,
                              FechaModificacion = empleado.FechaModificacion,
                              Activo = empleado.Activo
                          }
                          ).ToList();

            return Respuesta<List<EmpleadoDto>>.Success(result);
        }

        public Respuesta<string> AgregarEmpleados(EmpleadoDto entidad)
        {
            var objeto = _mapper.Map<Empleado>(entidad);

            EmpleadoValidations validator = new EmpleadoValidations();
            validator.RuleFor(x => x.UsuarioCreacion).NotEqual(0);
            ValidationResult validationResult = validator.Validate(objeto);

            if (!validationResult.IsValid)
            {
                IEnumerable<string> Errors = validationResult.Errors.Select(s => s.ErrorMessage);
                string menssageValidation = string.Join(Environment.NewLine, Errors);
                return Respuesta.Fault<string>(menssageValidation, OutputMessage.FaultInsertEmpleado);
            }

            var VerificarEmpleadoExiste = _unitOfWork.Repository<Empleado>().AsQueryable().Where(x => x.EmpleadoIdentidad == objeto.EmpleadoIdentidad).FirstOrDefault();
            if (VerificarEmpleadoExiste != null) { return Respuesta<string>.Fault(OutputMessage.FaultEmpleadoExists); }

            var VerififcarUsuarioCreador = _unitOfWork.Repository<Usuario>().AsQueryable().Where(x => x.UsuarioCreacion == objeto.UsuarioCreacion).ToList();
            if (VerififcarUsuarioCreador.Count == 0) { return Respuesta<string>.Fault(OutputMessage.FaultUserCreationNotExists); }

            objeto.FechaCreacion = DateTime.Now;
            objeto.FechaModificacion = null;
            objeto.UsuarioModificacion = null;

            _unitOfWork.Repository<Empleado>().Add(objeto);
            _unitOfWork.SaveChanges();
            return Respuesta<string>.Success(OutputMessage.SuccessInsertEmpleado);
        }
    }
}
