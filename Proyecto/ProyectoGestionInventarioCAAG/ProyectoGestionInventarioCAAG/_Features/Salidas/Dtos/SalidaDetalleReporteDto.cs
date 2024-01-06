namespace ProyectoGestionInventarioCAAG._Features.Salidas.Dtos
{
    public class SalidaDetalleReporteDto
    {
        public int SalidaDetalle { get; set; }

        public int SalidaId { get; set; }

        public int LoteId { get; set; }

        public int? SalidaDetalleCantidad { get; set; }

        public int UsuarioCreacion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int? UsuarioModificacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public bool? Activo { get; set; }
    }
}
