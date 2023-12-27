using FluentValidation;
using System;
using System.Collections.Generic;

namespace ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;

public partial class Empleado
{
    public int EmpleadoId { get; set; }

    public string? EmpleadoNombre { get; set; }

    public string? EmpleadoApellido { get; set; }

    public DateTime? EmpleadoFechaNacimiento { get; set; }

    public string? EmpleadoSexo { get; set; }

    public string? EmpleadoTelefono { get; set; }

    public int UsuarioCreacion { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int? UsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public bool? Activo { get; set; }

    public virtual Usuario UsuarioCreacionNavigation { get; set; } = null!;

    public virtual Usuario? UsuarioModificacionNavigation { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();

    public class EmpleadoValidations : AbstractValidator<Empleado>
    {
        public EmpleadoValidations()
        {
            RuleFor(x => x.EmpleadoNombre).NotEmpty();
            RuleFor(x => x.EmpleadoNombre).MaximumLength(200);
            RuleFor(x => x.EmpleadoApellido).NotEmpty();
            RuleFor(x => x.EmpleadoApellido).MaximumLength(200);
            RuleFor(x => x.EmpleadoFechaNacimiento).NotEmpty();
            RuleFor(x => x.EmpleadoSexo).NotEmpty();
            RuleFor(x => x.EmpleadoSexo).InclusiveBetween("M","F");
            RuleFor(x => x.EmpleadoTelefono).NotEmpty();
            RuleFor(x => x.EmpleadoTelefono).MaximumLength(14);

        }
    }
}
