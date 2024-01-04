namespace ProyectoGestionInventarioCAAG._Features.Usuarios.Dtos
{
    public class UsuarioDto
    {
        public int UsuarioId { get; set; }

        public string UsuarioNombreUsuario { get; set; } = null!;

        public string UsuarioContrasena { get; set; } = null!;

        public int PerfilId { get; set; }

        public int EmpleadoId { get; set; }

        public int UsuarioCreacion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int? UsuarioModificacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public bool? Activo { get; set; }
    }
}
