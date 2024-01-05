using ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;

namespace ProyectoGestionInventarioCAAG._Features.Salidas.Dtos
{
    public class SalidaDto
    {
        public int SucursalId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }

        public int UsuarioId { get; set; }

        public DateTime SalidaFecha { get; set; }
    }
}
