namespace API_ProyectoGestionInventario.Models.ModelsAcceso
{
    public class UsuariosViewModel
    {
        public int usua_Id { get; set; }

        public string usua_Usuario { get; set; }

        public string usua_Contrasenia { get; set; }

        public int role_Id { get; set; }

        public bool usua_Admin { get; set; }

        public int usua_UsuarioCreacion { get; set; }

        public DateTime usua_FechaCreacion { get; set; }

        public int? usua_UsuarioModificacion { get; set; }

        public DateTime? usua_FechaModificacion { get; set; }

        public bool? usua_Estado { get; set; }
    }
}
