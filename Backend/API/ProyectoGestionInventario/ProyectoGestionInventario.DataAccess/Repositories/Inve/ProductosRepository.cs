using Dapper;
using Microsoft.Data.SqlClient;
using ProyectoGestionInventario.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGestionInventario.DataAccess.Repositories.Inve
{
    public class ProductosRepository : IRepository<tbProducto>
    {
        public RequestStatus Delete(tbProducto item)
        {
            throw new NotImplementedException();
        }

        public tbProducto Find(int? id)
        {
            throw new NotImplementedException();
        }

        public RequestStatus Insert(tbProducto item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tbProducto> List()
        {
            using var db = new SqlConnection(ProyectoGestionInventario.ConnectionString);
            var parametros = new DynamicParameters();
            return db.Query<tbProducto>(ScriptsDataBase.SelectProductos, null, commandType: CommandType.StoredProcedure);
        }

        public RequestStatus Update(tbProducto item)
        {
            throw new NotImplementedException();
        }
    }
}
