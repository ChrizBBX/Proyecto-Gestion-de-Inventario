using System;
using System.Collections.Generic;

namespace ProyectoGestionInventarioCAAG.Utility.Scaffolding;

public partial class Salida
{
    public int SalidaId { get; set; }

    public int SucursalId { get; set; }

    public int UsuarioId { get; set; }

    public DateTime SalidaFecha { get; set; }

    public DateTime SalidaFechaRecibido { get; set; }

    public decimal SalidaTotal { get; set; }

    public int EstadoId { get; set; }

    public int UsuarioCreacion { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int? UsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<SalidasDetalle> SalidasDetalles { get; set; } = new List<SalidasDetalle>();

    public virtual Sucursale Sucursal { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;

    public virtual Usuario UsuarioCreacionNavigation { get; set; } = null!;

    public virtual Usuario? UsuarioModificacionNavigation { get; set; }
}
