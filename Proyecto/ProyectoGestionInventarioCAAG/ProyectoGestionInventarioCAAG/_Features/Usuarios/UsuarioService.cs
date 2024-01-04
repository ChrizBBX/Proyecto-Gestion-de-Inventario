using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;
using FluentValidation;
using FluentValidation.Results;
using ProyectoGestionInventarioCAAG._Features.Usuarios.Dtos;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;
using ProyectoGestionInventarioCAAG.Utility;
using static ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities.Usuario;

namespace ProyectoGestionInventarioCAAG._Features.Usuarios
{
    public class UsuarioService : IUsuario<UsuarioDto>
    {
        private readonly IMapper _mappper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UsuarioDomain _usuarioDomain = new UsuarioDomain();

        public UsuarioService(IMapper mappper, UnitOfWorkBuilder unitOfWork, UsuarioDomain usuarioDomain)
        {
            _mappper = mappper;
            _unitOfWork = unitOfWork.BuilderProyectoGestionInventarioCAAG();
            _usuarioDomain = usuarioDomain;
        }

        public Respuesta<List<UsuarioDto>> ListadoUsuarios()
        {
            var list = (from usuario in _unitOfWork.Repository<Usuario>().AsQueryable()
                        select new UsuarioDto
                        {
                            UsuarioNombreUsuario = usuario.UsuarioNombreUsuario,
                            UsuarioContrasena = usuario.UsuarioContrasena,
                            PerfilId = usuario.PerfilId,
                            EmpleadoId = usuario.EmpleadoId
                        }
                        ).ToList();
            return Respuesta.Success(list);
        }

        public Respuesta<string> InsertarUsuarios(UsuarioDto entidad)
        {
            var objeto = _mappper.Map<Usuario>(entidad);
            UsuarioValidations validator = new UsuarioValidations();
            validator.RuleFor(x => x.PerfilId).GreaterThan(0);
            validator.RuleFor(x => x.EmpleadoId).GreaterThan(0);
            validator.RuleFor(x => x.UsuarioCreacion).NotEmpty().GreaterThan(0);
            ValidationResult validationResult = validator.Validate(objeto);

            if (!validationResult.IsValid)
            {
                IEnumerable<string> Errors = validationResult.Errors.Select(s => s.ErrorMessage);
                string menssageValidation = string.Join(Environment.NewLine, Errors);
                return Respuesta.Fault<string>(menssageValidation, OutputMessage.FaultUserEntity);
            }

            InicioSesionSeguridad seguridad = new InicioSesionSeguridad();
            string password = seguridad.HashPassword(entidad.UsuarioContrasena);

        
            var listaUsuarios = _unitOfWork.Repository<Usuario>().AsQueryable().ToList();
            var listaPerfiles = _unitOfWork.Repository<Perfil>().AsQueryable().ToList();
            var listaEmpleados = _unitOfWork.Repository<Empleado>().AsQueryable().ToList();

            var validacionesInsertarUsuarios = _usuarioDomain.ValidacionesInsertarUsuarios(entidad, listaUsuarios, listaPerfiles, listaEmpleados);
            if (!validacionesInsertarUsuarios.Ok)
                return Respuesta.Fault<string>(validacionesInsertarUsuarios.Mensaje);

            objeto.UsuarioContrasena = password;
            objeto.FechaModificacion = null;
            objeto.UsuarioModificacion = null;
            objeto.FechaCreacion = DateTime.Now;

            _unitOfWork.Repository<Usuario>().Add(objeto);
            _unitOfWork.SaveChanges();

            return Respuesta.Success<string>(OutputMessage.SuccessInsertUser);
        }

        public Respuesta<List<UsuarioDto>> InicioSesion(UsuarioDto entidad)
        {
            var objeto = _mappper.Map<Usuario>(entidad);
            UsuarioValidations validator = new UsuarioValidations();
            ValidationResult validationResult = validator.Validate(objeto);

            if (!validationResult.IsValid)
            {
                IEnumerable<string> Errors = validationResult.Errors.Select(s => s.ErrorMessage);
                string menssageValidation = string.Join(Environment.NewLine, Errors);
                return Respuesta.Fault<List<UsuarioDto>>(menssageValidation, OutputMessage.FaultUserEntity);
            }

            InicioSesionSeguridad seguridad = new InicioSesionSeguridad();
            string password = seguridad.HashPassword(entidad.UsuarioContrasena);

            var result = (from usuario in _unitOfWork.Repository<Usuario>().AsQueryable()
                          where usuario.UsuarioNombreUsuario == objeto.UsuarioNombreUsuario && usuario.UsuarioContrasena == password
                          select new UsuarioDto
                          {
                              UsuarioId = usuario.UsuarioId,
                              UsuarioNombreUsuario = usuario.UsuarioNombreUsuario,
                              UsuarioContrasena = usuario.UsuarioContrasena,
                              EmpleadoId = usuario.EmpleadoId,
                              PerfilId = usuario.PerfilId
                          }).ToList();
            if (result.Count == 0) { return Respuesta.Fault<List<UsuarioDto>>(OutputMessage.FaultUserLogin); }
            return Respuesta.Success(result);
        }
    }
}
