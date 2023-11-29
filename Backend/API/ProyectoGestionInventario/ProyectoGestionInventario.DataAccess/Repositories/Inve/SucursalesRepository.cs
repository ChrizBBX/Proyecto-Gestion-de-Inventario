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
    public class SucursalesRepository : IRepository<VW_tbSucursales>
    {
        public RequestStatus Delete(VW_tbSucursales item)
        {
            throw new NotImplementedException();
        }

        public VW_tbSucursales Find(int? id)
        {
            throw new NotImplementedException();
        }

        public RequestStatus Insert(VW_tbSucursales item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VW_tbSucursales> List()
        {
            using var db = new SqlConnection(ProyectoGestionInventario.ConnectionString);
            var parametros = new DynamicParameters();
            return db.Query<VW_tbSucursales>(ScriptsDataBase.UDP_tbSucursales_Select, null, commandType: CommandType.StoredProcedure);
        }

        public RequestStatus Update(VW_tbSucursales item)
        {
            throw new NotImplementedException();
        }
    }
}
