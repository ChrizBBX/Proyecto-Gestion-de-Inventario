
using Microsoft.Extensions.DependencyInjection;
using ProyectoGestionInventario.BusinessLogic.Services.AccesoServices;
using ProyectoGestionInventario.BusinessLogic.Services.InventarioServices;
using ProyectoGestionInventario.DataAccess.Repositories.Acce;
using ProyectoGestionInventario.DataAccess.Repositories.Inve;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGestionInventario.BussinessLogic
{
    public static class ServiceConfiguration
    {
        public static void DataAccess(this IServiceCollection services, string connection)
        {
            ProyectoGestionInventario.DataAccess.ProyectoGestionInventario.BuildConnectionString(connection);


            #region Acceso
            //Usuarios
            services.AddScoped<UsuariosRepository>();
            services.AddScoped<PantallasRepository>();
            services.AddScoped<RolesPorPantallaViewRepository>();
            #endregion

            #region Inventario
            //Productos
            services.AddScoped<ProductosRepository>();
            services.AddScoped<LotesRepository>();
            services.AddScoped<SucursalesRepository>();
            services.AddScoped<SalidasRepository>();
            services.AddScoped<SalidasDetallesRepository>();
            services.AddScoped<SalidasViewRepository>();
            #endregion
        }


        public static void BussinessLogic(this IServiceCollection services)
        {
            services.AddScoped<InventarioServices>();
            services.AddScoped<AccesoServices>();
        }
    }
}
