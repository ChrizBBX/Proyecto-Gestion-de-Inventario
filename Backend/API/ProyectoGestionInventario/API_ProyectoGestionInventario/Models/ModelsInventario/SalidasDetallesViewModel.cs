namespace API_ProyectoGestionInventario.Models.ModelsInventario
{
    public class SalidasDetallesViewModel
    {
        public int sade_Id { get; set; }

        public int sali_Id { get; set; }

        public int lote_Id { get; set; }

        public int? sade_Cantidad { get; set; }

        public int usua_UsuarioCreacion { get; set; }

        public DateTime sade_FechaCreacion { get; set; }

        public int? usua_UsuarioModificacion { get; set; }

        public DateTime? sade_FechaModificacion { get; set; }
    }
}
