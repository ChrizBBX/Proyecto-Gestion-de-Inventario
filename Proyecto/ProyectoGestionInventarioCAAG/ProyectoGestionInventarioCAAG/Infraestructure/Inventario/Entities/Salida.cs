using FluentValidation;
using ProyectoGestionInventarioCAAG.Utility.Scaffolding;
using System;
using System.Collections.Generic;

namespace ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;

public partial class Salida
{
    public int SalidaId { get; set; }

    public int SucursalId { get; set; }

    public int UsuarioId { get; set; }

    public DateTime SalidaFecha { get; set; }

    public DateTime SalidaFechaRecibido { get; set; }

    public decimal SalidaTotal { get; set; }

    public int EstadoSalidaId { get; set; }

    public int UsuarioCreacion { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int? UsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public bool? Activo { get; set; }

    public virtual EstadosSalida EstadoSalida { get; set; } = null!;

    public virtual ICollection<SalidasDetalle> SalidasDetalles { get; set; } = new List<SalidasDetalle>();

    public virtual Sucursale Sucursal { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;

    public virtual Usuario UsuarioCreacionNavigation { get; set; } = null!;

    public virtual Usuario? UsuarioModificacionNavigation { get; set; }

    public class SalidaValidations : AbstractValidator<Salida>
    {
        public SalidaValidations()
        {
            RuleFor(x => x.SucursalId).NotEmpty();
            RuleFor(x => x.UsuarioId).NotEmpty();
        }
    }
}
