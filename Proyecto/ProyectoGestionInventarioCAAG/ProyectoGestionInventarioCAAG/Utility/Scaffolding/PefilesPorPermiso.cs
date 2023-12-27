using System;
using System.Collections.Generic;

namespace ProyectoGestionInventarioCAAG.Utility.Scaffolding;

public partial class PefilesPorPermiso
{
    public int PerfilPorPermisoId { get; set; }

    public int PerfilId { get; set; }

    public int PermisoId { get; set; }

    public int UsuarioCreacion { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int? UsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public bool? Activo { get; set; }

    public virtual Perfile Perfil { get; set; } = null!;

    public virtual Permiso Permiso { get; set; } = null!;

    public virtual Usuario UsuarioCreacionNavigation { get; set; } = null!;

    public virtual Usuario? UsuarioModificacionNavigation { get; set; }
}
