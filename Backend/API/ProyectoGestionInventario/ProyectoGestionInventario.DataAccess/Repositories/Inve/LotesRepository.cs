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
    public class LotesRepository : IRepository<VW_tbLotes>
    {
        public RequestStatus Delete(VW_tbLotes item)
        {
            throw new NotImplementedException();
        }

        public VW_tbLotes Find(int? id)
        {
            throw new NotImplementedException();
        }

        public RequestStatus Insert(VW_tbLotes item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VW_tbLotes> List()
        {
            using var db = new SqlConnection(ProyectoGestionInventario.ConnectionString);
            var parametros = new DynamicParameters();
            return db.Query<VW_tbLotes>(ScriptsDataBase.UDP_tbLotes_Select, null, commandType: CommandType.StoredProcedure);
        }

        public RequestStatus Update(VW_tbLotes item)
        {
            throw new NotImplementedException();
        }
    }
}
