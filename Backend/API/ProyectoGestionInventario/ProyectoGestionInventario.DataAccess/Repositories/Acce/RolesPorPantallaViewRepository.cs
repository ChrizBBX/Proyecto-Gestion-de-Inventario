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
    public class RolesPorPantallaViewRepository : IRepository<VW_tbRolesPorPantalla>
    {
        public RequestStatus Delete(VW_tbRolesPorPantalla item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VW_tbRolesPorPantalla> Listar_Find(int? id)
        {
            using var db = new SqlConnection(ProyectoGestionInventario.ConnectionString);

            var parametros = new DynamicParameters();

            parametros.Add("@role_Id", id, DbType.String, ParameterDirection.Input);

            return db.Query<VW_tbRolesPorPantalla>(ScriptsDataBase.UDP_tbRolesPorPantalla_Filtrado, parametros, commandType: CommandType.StoredProcedure);
        }

        public RequestStatus Insert(VW_tbRolesPorPantalla item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VW_tbRolesPorPantalla> List()
        {
            throw new NotImplementedException();
        }

        public RequestStatus Update(VW_tbRolesPorPantalla item)
        {
            throw new NotImplementedException();
        }

        VW_tbRolesPorPantalla IRepository<VW_tbRolesPorPantalla>.Find(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
