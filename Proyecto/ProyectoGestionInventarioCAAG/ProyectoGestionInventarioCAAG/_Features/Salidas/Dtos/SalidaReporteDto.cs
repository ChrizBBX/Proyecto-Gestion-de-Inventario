using ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;

namespace ProyectoGestionInventarioCAAG._Features.Salidas.Dtos
{
    public class SalidaReporteDto
    {
        public int SalidaId { get; set; }

        public int ProductoId { get; set; }

        public int SucursalId { get; set; }

        public int UsuarioId { get; set; }

        public DateTime SalidaFecha { get; set; }

        public DateTime? SalidaFechaRecibido { get; set; }

        public decimal? SalidaTotal { get; set; }

        public int EstadoSalidaId { get; set; }

        public int UsuarioCreacion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int? UsuarioModificacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public bool? Activo { get; set; }
        public int? UnidadesTotales { get; set; }

        public List<SalidasDetalle> salidaDetalles { get; set; }

    }
}
