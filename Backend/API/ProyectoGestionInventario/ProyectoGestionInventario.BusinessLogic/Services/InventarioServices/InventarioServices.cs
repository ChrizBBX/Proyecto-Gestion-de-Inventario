using ProyectoGestionInventario.BussinessLogic;
using ProyectoGestionInventario.DataAccess.Repositories.Acce;
using ProyectoGestionInventario.DataAccess.Repositories.Inve;
using ProyectoGestionInventario.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGestionInventario.BusinessLogic.Services.InventarioServices
{
    public class InventarioServices
    {
        private readonly ProductosRepository _productosRepository;
        private readonly LotesRepository _lotesRepository;

        public InventarioServices(ProductosRepository productosRepository, LotesRepository lotesRepository)
        {
            _productosRepository = productosRepository;
            _lotesRepository = lotesRepository;
        }

        #region Productos
        public ServiceResult Productos_Listar()
        {
            var result = new ServiceResult();
            try
            {
                var map = _productosRepository.List();
                return result.Ok(map);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }

        public ServiceResult Productos_Insertar(tbProductos item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _productosRepository.Insert(item);
                return result.Ok(map);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }

        public ServiceResult Productos_Actualizar(tbProductos item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _productosRepository.Update(item);
                return result.Ok(map);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }

        public ServiceResult Productos_Eliminar(tbProductos item)
        {
            var result = new ServiceResult();
            try
            {
                var map = _productosRepository.Delete(item);
                return result.Ok(map);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }
        #endregion

        #region Lotes
        public ServiceResult Lotes_Listar()
        {
            var result = new ServiceResult();
            try
            {
                var map = _lotesRepository.List();
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
