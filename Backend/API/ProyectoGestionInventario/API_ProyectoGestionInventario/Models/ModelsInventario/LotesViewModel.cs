namespace API_ProyectoGestionInventario.Models.ModelsInventario
{
    public class LotesViewModel
    {
        public int lote_Id { get; set; }

        public int prod_Id { get; set; }

        public string prod_Descripcion { get; set; }

        public decimal prod_Precio { get; set; }

        public int lote_Cantidad { get; set; }

        public DateTime lote_FechaVencimiento { get; set; }

        public int usua_UsuarioCreacion { get; set; }

        public string usua_UsuarioCrecion_Usuario { get; set; }

        public DateTime lote_FechaCreacion { get; set; }

        public int? usua_UsuarioModificacion { get; set; }

        public string usua_UsuarioModificacion_Usuario { get; set; }

        public DateTime? lote_FechaModificacion { get; set; }

        public bool? lote_Estado { get; set; }
    }
}
