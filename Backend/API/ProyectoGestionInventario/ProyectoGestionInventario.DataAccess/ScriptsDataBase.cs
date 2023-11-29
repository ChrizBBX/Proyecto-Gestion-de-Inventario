using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGestionInventario.DataAccess
{
    public class ScriptsDataBase
    {
        #region Acceso
        public static string UDP_Login = "acce.UDP_Login";
        public static string UDP_tbPantallas_Select = "acce.UDP_tbPantallas_Select";
        public static string UDP_tbRolesPorPantalla_Filtrado = "acce.UDP_tbRolesPorPantalla_Filtrado";
        #endregion

        #region Inventario
        public static string UDP_tbProductos_Select = "inve.UDP_tbProductos_Select";
        public static string UDP_tbProductos_Insert = "inve.UDP_tbProductos_Insert";
        public static string UDP_tbProductos_Update = "inve.UDP_tbProductos_Update";
        public static string UDP_tbProductos_Delete = "inve.UDP_tbProductos_Delete";
        public static string UDP_tbSucursales_Select = "inve.UDP_tbSucursales_Select";
        public static string UDP_tbSalidas_Insert = "inve.UDP_tbSalidas_Insert";
        public static string UDP_tbSalidasDetalles_Insert = "inve.UDP_tbSalidasDetetalles_Insert";
        #endregion

        #region Lotes
        public static string UDP_tbLotes_Select = "inve.UDP_tbLotes_Select";
        public static string UDP_tbLotes_SelectPorProducto = "inve.UDP_tbLotes_SelectPorProducto";
        #endregion
    }
}

