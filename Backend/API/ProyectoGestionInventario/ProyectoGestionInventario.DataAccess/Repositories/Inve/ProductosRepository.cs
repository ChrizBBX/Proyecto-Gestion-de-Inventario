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
    public class ProductosRepository : IRepository<tbProductos>
    {
        public RequestStatus Delete(tbProductos item)
        {
            throw new NotImplementedException();
        }

        public tbProductos Find(int? id)
        {
            throw new NotImplementedException();
        }

        public RequestStatus Insert(tbProductos item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tbProductos> List()
        {
            using var db = new SqlConnection(ProyectoGestionInventario.ConnectionString);
            var parametros = new DynamicParameters();
            return db.Query<tbProductos>(ScriptsDataBase.SelectProductos, null, commandType: CommandType.StoredProcedure);
        }

        public RequestStatus Update(tbProductos item)
        {
            throw new NotImplementedException();
        }
    }
}
