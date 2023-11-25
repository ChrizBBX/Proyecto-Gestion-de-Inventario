using ProyectoGestionInventario.Entities.Entities;

namespace API_ProyectoGestionInventario.Models.ModelsAcceso
{
    public class PantallasViewModel
    {
        public int pant_Id { get; set; }

        public string pant_Nombre { get; set; }

        public string pant_Url { get; set; }

        public string pant_Identificador { get; set; }

        public string pant_Icono { get; set; }

        public string pant_Componente { get; set; }

        public string pant_PropiedadExtra { get; set; }

        public string pant_PropiedadExtra_1 { get; set; }

        public string pant_PropiedadExtra_2 { get; set; }

        public int usua_UsuarioCreacion { get; set; }

        public DateTime pant_FechaCreacion { get; set; }

        public int? usua_UsuarioModificacion { get; set; }

        public DateTime? pant_FechaModificacion { get; set; }

        public bool? pant_Estado { get; set; }
    }
}
