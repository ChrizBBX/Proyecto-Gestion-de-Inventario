using Dapper;
using Microsoft.Data.SqlClient;
using ProyectoGestionInventario.DataAccess.Repositories.Inve;
using ProyectoGestionInventario.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGestionInventario.DataAccess.Repositories.Acce
{
    public class PantallasRepository : IRepository<tbPantallas>
    {
        public RequestStatus Delete(tbPantallas item)
        {
            throw new NotImplementedException();
        }

        public tbPantallas Find(int? id)
        {
            throw new NotImplementedException();
        }

        public RequestStatus Insert(tbPantallas item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tbPantallas> List()
        {
            using var db = new SqlConnection(ProyectoGestionInventario.ConnectionString);
            return db.Query<tbPantallas>(ScriptsDataBase.UDP_tbPantallas_Select, null, commandType: CommandType.StoredProcedure);
        }

        public RequestStatus Update(tbPantallas item)
        {
            throw new NotImplementedException();
        }
    }
}
