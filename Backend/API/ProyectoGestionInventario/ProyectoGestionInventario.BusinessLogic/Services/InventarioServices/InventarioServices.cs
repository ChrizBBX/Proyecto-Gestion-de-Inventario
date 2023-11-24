using ProyectoGestionInventario.BussinessLogic;
using ProyectoGestionInventario.DataAccess.Repositories.Inve;
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

        public InventarioServices(ProductosRepository productosRepository)
        {
            _productosRepository = productosRepository;
        }

        #region Inventario

        public ServiceResult ListarProductos()
        {
            var result = new ServiceResult();
            try
            {
                var list = _productosRepository.List();
                return result.Ok(list);
            }
            catch (Exception e)
            {
                return result.Error(e.Message);
            }
        }
        #endregion
    }
}
