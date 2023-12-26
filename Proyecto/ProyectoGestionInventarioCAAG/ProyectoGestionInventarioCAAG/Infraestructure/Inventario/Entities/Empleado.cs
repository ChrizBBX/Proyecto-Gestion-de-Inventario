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
}
