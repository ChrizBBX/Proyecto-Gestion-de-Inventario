namespace ProyectoGestionInventarioCAAG._Features.Empleados.Dtos
{
    public class EmpleadoDto
    {
        public int EmpleadoId { get; set; }

        public string? EmpleadoNombre { get; set; }

        public string? EmpleadoApellido { get; set; }

        public string? EmpleadoIdentidad { get; set; }

        public DateTime? EmpleadoFechaNacimiento { get; set; }

        public string? EmpleadoSexo { get; set; }

        public string? EmpleadoTelefono { get; set; }

        public int UsuarioCreacion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int? UsuarioModificacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public bool? Activo { get; set; }
    }
}
