﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ProyectoGestionInventario.Entities.Entities;

public partial class tbRolesPorPantalla
{
    public int ropa_Id { get; set; }

    public int role_Id { get; set; }

    public int pant_Id { get; set; }

    public int usua_UsuarioCreacion { get; set; }

    public DateTime ropa_FechaCreacion { get; set; }

    public int? usua_UsuarioModificacion { get; set; }

    public DateTime? ropa_FechaModificacion { get; set; }

    public bool? ropa_Estado { get; set; }

    public virtual tbUsuarios usua_UsuarioCreacionNavigation { get; set; }

    public virtual tbUsuarios usua_UsuarioModificacionNavigation { get; set; }
}