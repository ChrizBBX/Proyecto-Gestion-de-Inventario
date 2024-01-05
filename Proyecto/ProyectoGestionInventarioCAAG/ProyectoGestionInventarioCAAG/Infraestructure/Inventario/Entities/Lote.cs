using FluentValidation;
using System;
using System.Collections.Generic;

namespace ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;

public partial class Lote
{
    public int LoteId { get; set; }

    public int? ProductoId { get; set; }

    public int? LoteCantidadInicial { get; set; }

    public decimal? LoteCostoCantidad { get; set; }

    public DateTime? LoteFechaVencimiento { get; set; }

    public int LoteCantidad { get; set; }

    public int UsuarioCreacion { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int? UsuarioModificacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public bool? Activo { get; set; }

    public virtual Producto? Producto { get; set; }

    public virtual ICollection<SalidasDetalle> SalidasDetalles { get; set; } = new List<SalidasDetalle>();

    public virtual Usuario UsuarioCreacionNavigation { get; set; } = null!;

    public virtual Usuario? UsuarioModificacionNavigation { get; set; }

    public class LoteValidations : AbstractValidator<Lote>
    {
        public LoteValidations()
        {
            RuleFor(x => x.ProductoId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.LoteCostoCantidad).NotEmpty().GreaterThan(0);
            RuleFor(x => x.LoteFechaVencimiento).NotEmpty();
            RuleFor(x => x.LoteCantidad).NotEmpty();
        }
    }
}
