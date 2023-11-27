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
        #endregion

    }
}

