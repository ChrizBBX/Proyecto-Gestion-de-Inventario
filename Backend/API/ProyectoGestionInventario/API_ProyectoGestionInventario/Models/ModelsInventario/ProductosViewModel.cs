namespace API_ProyectoGestionInventario.Models.ModelsInventario
{
    public class ProductosViewModel
    {

        public int prod_Id { get; set; }

        public string prod_Descripcion { get; set; }

        public int? prov_Id { get; set; }

        public int? cate_Id { get; set; }
    }
}
