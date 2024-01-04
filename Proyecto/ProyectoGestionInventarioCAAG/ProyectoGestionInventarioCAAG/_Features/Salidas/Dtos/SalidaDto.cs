namespace ProyectoGestionInventarioCAAG._Features.Salidas.Dtos
{
    public class SalidaDto
    {
        public int SalidaId { get; set; }

        public int SucursalId { get; set; }

        public int UsuarioId { get; set; }

        public DateTime SalidaFecha { get; set; }

        public DateTime SalidaFechaRecibido { get; set; }

        public decimal SalidaTotal { get; set; }

        public int EstadoId { get; set; }

        public int UsuarioCreacion { get; set; }
    }
}
