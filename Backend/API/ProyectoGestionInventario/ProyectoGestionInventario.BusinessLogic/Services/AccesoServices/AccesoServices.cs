using ProyectoGestionInventario.BussinessLogic;
using ProyectoGestionInventario.DataAccess.Repositories.Acce;
using ProyectoGestionInventario.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGestionInventario.BusinessLogic.Services.AccesoServices
{
    public class AccesoServices
    {
        private readonly UsuariosRepository _usuariosRepository;
        private readonly PantallasRepository _pantallasRepository;
        private readonly RolesPorPantallaViewRepository _rolesPorPantallaViewRepository;

        public AccesoServices(UsuariosRepository usuariosRepository, PantallasRepository pantallasRepository, RolesPorPantallaViewRepository rolesPorPantallaViewRepository)
        {
            _usuariosRepository = usuariosRepository;
            _pantallasRepository = pantallasRepository;
            _rolesPorPantallaViewRepository = rolesPorPantallaViewRepository;
        }

        #region Usuarios
        public ServiceResult Login(string usuario,string contrasenia)
        {
            var result = new ServiceResult();
            try
            {
                var map = _usuariosRepository.Login(usuario,contrasenia);
                return result.Ok(map);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }

        #endregion

        #region RolesPorPantallasView
        public ServiceResult Pantallas_Listar(int? id)
        {
            var result = new ServiceResult();
            try
            {
                var map = _rolesPorPantallaViewRepository.Find(id);
                return result.Ok(map);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }
        #endregion

        #region Pantallas
        public ServiceResult Pantallas_Listar()
        {
            var result = new ServiceResult();
            try
            {
                var map = _pantallasRepository.List();
                return result.Ok(map);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }
        #endregion
    }
}
