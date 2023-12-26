using System;
using System.Collections.Generic;

namespace ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;

public partial class Permiso
{
    public int PermisoId { get; set; }

    public string? PermisoNombre { get; set; }

    public int UsuarioCreacion { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int? UsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<PerfilesPorPermiso> PefilesPorPermisos { get; set; } = new List<PerfilesPorPermiso>();

    public virtual Usuario UsuarioCreacionNavigation { get; set; } = null!;

    public virtual Usuario? UsuarioModificacionNavigation { get; set; }
}
