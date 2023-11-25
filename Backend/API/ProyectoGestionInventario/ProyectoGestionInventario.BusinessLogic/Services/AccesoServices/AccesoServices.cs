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

        public AccesoServices(UsuariosRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
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
    }
}
