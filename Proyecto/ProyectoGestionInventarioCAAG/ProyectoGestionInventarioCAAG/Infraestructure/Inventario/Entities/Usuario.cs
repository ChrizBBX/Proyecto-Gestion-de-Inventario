﻿using FluentValidation;
using System;
using System.Collections.Generic;

namespace ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string UsuarioNombreUsuario { get; set; } = null!;

    public string UsuarioContrasena { get; set; } = null!;

    public int PerfilId { get; set; }

    public int EmpleadoId { get; set; }

    public int UsuarioCreacion { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int? UsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public bool? Activo { get; set; }

    public virtual Empleado Empleado { get; set; } = null!;

    public virtual ICollection<Empleado> EmpleadoUsuarioCreacionNavigations { get; set; } = new List<Empleado>();

    public virtual ICollection<Empleado> EmpleadoUsuarioModificacionNavigations { get; set; } = new List<Empleado>();

    public virtual ICollection<EstadosSalida> EstadosSalidaUsuarioCreacionNavigations { get; set; } = new List<EstadosSalida>();

    public virtual ICollection<EstadosSalida> EstadosSalidaUsuarioModificacionNavigations { get; set; } = new List<EstadosSalida>();

    public virtual ICollection<Usuario> InverseUsuarioCreacionNavigation { get; set; } = new List<Usuario>();

    public virtual ICollection<Usuario> InverseUsuarioModificacionNavigation { get; set; } = new List<Usuario>();

    public virtual ICollection<Lote> LoteUsuarioCreacionNavigations { get; set; } = new List<Lote>();

    public virtual ICollection<Lote> LoteUsuarioModificacionNavigations { get; set; } = new List<Lote>();

    public virtual ICollection<PerfilesPorPermiso> PefilesPorPermisoUsuarioCreacionNavigations { get; set; } = new List<PerfilesPorPermiso>();

    public virtual ICollection<PerfilesPorPermiso> PefilesPorPermisoUsuarioModificacionNavigations { get; set; } = new List<PerfilesPorPermiso>();

    public virtual Perfil Perfil { get; set; } = null!;

    public virtual ICollection<Perfil> PerfileUsuarioCreacionNavigations { get; set; } = new List<Perfil>();

    public virtual ICollection<Perfil> PerfileUsuarioModificacionNavigations { get; set; } = new List<Perfil>();

    public virtual ICollection<Permiso> PermisoUsuarioCreacionNavigations { get; set; } = new List<Permiso>();

    public virtual ICollection<Permiso> PermisoUsuarioModificacionNavigations { get; set; } = new List<Permiso>();

    public virtual ICollection<Producto> ProductoUsuarioCreacionNavigations { get; set; } = new List<Producto>();

    public virtual ICollection<Producto> ProductoUsuarioModificacionNavigations { get; set; } = new List<Producto>();

    public virtual ICollection<Salida> SalidaUsuarioCreacionNavigations { get; set; } = new List<Salida>();

    public virtual ICollection<Salida> SalidaUsuarioModificacionNavigations { get; set; } = new List<Salida>();

    public virtual ICollection<Salida> SalidaUsuarios { get; set; } = new List<Salida>();

    public virtual ICollection<SalidasDetalle> SalidasDetalleUsuarioCreacionNavigations { get; set; } = new List<SalidasDetalle>();

    public virtual ICollection<SalidasDetalle> SalidasDetalleUsuarioModificacionNavigations { get; set; } = new List<SalidasDetalle>();

    public virtual ICollection<Sucursal> SucursaleUsuarioCreacionNavigations { get; set; } = new List<Sucursal>();

    public virtual ICollection<Sucursal> SucursaleUsuarioModificacionNavigations { get; set; } = new List<Sucursal>();

    public virtual Usuario UsuarioCreacionNavigation { get; set; } = null!;

    public virtual Usuario? UsuarioModificacionNavigation { get; set; }

    public class UsuarioValidations : AbstractValidator<Usuario>
    {
        public UsuarioValidations()
        {
            RuleFor(x => x.UsuarioNombreUsuario).NotEmpty().MaximumLength(100);
            RuleFor(x => x.UsuarioContrasena).NotEmpty();
        }
    }
}
