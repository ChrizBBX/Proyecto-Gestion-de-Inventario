﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ProyectoGestionInventario.Entities.Entities;

public partial class tbUsuarios
{
    public int usua_Id { get; set; }

    public string usua_Usuario { get; set; }

    public string usua_Contrasenia { get; set; }

    public int role_Id { get; set; }

    public bool usua_Admin { get; set; }

    public int usua_UsuarioCreacion { get; set; }

    public DateTime usua_FechaCreacion { get; set; }

    public int? usua_UsuarioModificacion { get; set; }

    public DateTime? usua_FechaModificacion { get; set; }

    public bool? usua_Estado { get; set; }

    public virtual ICollection<tbUsuarios> Inverseusua_UsuarioCreacionNavigation { get; set; } = new List<tbUsuarios>();

    public virtual ICollection<tbUsuarios> Inverseusua_UsuarioModificacionNavigation { get; set; } = new List<tbUsuarios>();

    public virtual tbRoles role { get; set; }

    public virtual ICollection<tbLotes> tbLotesusua_UsuarioCreacionNavigation { get; set; } = new List<tbLotes>();

    public virtual ICollection<tbLotes> tbLotesusua_UsuarioModificacionNavigation { get; set; } = new List<tbLotes>();

    public virtual ICollection<tbPantallas> tbPantallasusua_UsuarioCreacionNavigation { get; set; } = new List<tbPantallas>();

    public virtual ICollection<tbPantallas> tbPantallasusua_UsuarioModificacionNavigation { get; set; } = new List<tbPantallas>();

    public virtual ICollection<tbProductos> tbProductosusua_UsuarioCreacionNavigation { get; set; } = new List<tbProductos>();

    public virtual ICollection<tbProductos> tbProductosusua_UsuarioModificacionNavigation { get; set; } = new List<tbProductos>();

    public virtual ICollection<tbRolesPorPantalla> tbRolesPorPantallausua_UsuarioCreacionNavigation { get; set; } = new List<tbRolesPorPantalla>();

    public virtual ICollection<tbRolesPorPantalla> tbRolesPorPantallausua_UsuarioModificacionNavigation { get; set; } = new List<tbRolesPorPantalla>();

    public virtual ICollection<tbRoles> tbRolesusua_UsuarioCreacionNavigation { get; set; } = new List<tbRoles>();

    public virtual ICollection<tbRoles> tbRolesusua_UsuarioModificacionNavigation { get; set; } = new List<tbRoles>();

    public virtual ICollection<tbSalidasDetalles> tbSalidasDetallesusua_UsuarioCreacionNavigation { get; set; } = new List<tbSalidasDetalles>();

    public virtual ICollection<tbSalidasDetalles> tbSalidasDetallesusua_UsuarioModificacionNavigation { get; set; } = new List<tbSalidasDetalles>();

    public virtual ICollection<tbSalidas> tbSalidasusua { get; set; } = new List<tbSalidas>();

    public virtual ICollection<tbSalidas> tbSalidasusua_UsuarioCreacionNavigation { get; set; } = new List<tbSalidas>();

    public virtual ICollection<tbSalidas> tbSalidasusua_UsuarioModificacionNavigation { get; set; } = new List<tbSalidas>();

    public virtual ICollection<tbSucursales> tbSucursalesusua_UsuarioCreacionNavigation { get; set; } = new List<tbSucursales>();

    public virtual ICollection<tbSucursales> tbSucursalesusua_UsuarioModificacionNavigation { get; set; } = new List<tbSucursales>();

    public virtual tbUsuarios usua_UsuarioCreacionNavigation { get; set; }

    public virtual tbUsuarios usua_UsuarioModificacionNavigation { get; set; }
}