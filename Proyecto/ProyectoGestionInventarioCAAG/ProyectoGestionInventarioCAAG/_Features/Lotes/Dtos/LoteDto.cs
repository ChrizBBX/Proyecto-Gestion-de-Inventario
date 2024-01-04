namespace ProyectoGestionInventarioCAAG._Features.Lotes.Dtos
{
    public class LoteDto
    {
        public int LoteId { get; set; }

        public int? ProductoId { get; set; }

        public decimal? LoteCostoCantidad { get; set; }
        public int? LoteCantidadInicial { get; set; }

        public DateTime? LoteFechaVencimiento { get; set; }

        public int? LoteCantidad { get; set; }

        public int UsuarioCreacion { get; set; }
    }
}
