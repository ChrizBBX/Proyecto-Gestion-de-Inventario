namespace API_ProyectoGestionInventario.Models.ModelsInventario
{
    public class SalidasViewModel
    {
        public int sali_Id { get; set; }

        public int usua_Id { get; set; }

        public int sucu_Id { get; set; }

        public string sucu_SalidaEstado { get; set; }

        public int usua_UsuarioCreacion { get; set; }

        public DateTime sali_FechaCreacion { get; set; }

        public int? usua_UsuarioModificacion { get; set; }

        public DateTime? saliFechaModificacion { get; set; }
    }
}
