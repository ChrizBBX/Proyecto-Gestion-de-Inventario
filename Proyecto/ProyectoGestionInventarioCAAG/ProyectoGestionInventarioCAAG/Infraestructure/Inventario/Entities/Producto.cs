﻿using System;
using System.Collections.Generic;

namespace ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;

public partial class Producto
{
    public int ProductoId { get; set; }

    public string ProductoNombre { get; set; } = null!;

    public int UsuarioCreacion { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int? UsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Lote> Lotes { get; set; } = new List<Lote>();

    public virtual ICollection<Salida> Salida { get; set; } = new List<Salida>();

    public virtual Usuario UsuarioCreacionNavigation { get; set; } = null!;

    public virtual Usuario? UsuarioModificacionNavigation { get; set; }
}
