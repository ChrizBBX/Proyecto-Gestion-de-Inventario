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
    public class SalidasViewRepository : IRepository<VW_tbSalidas>
    {
        public RequestStatus Delete(VW_tbSalidas item)
        {
            throw new NotImplementedException();
        }

        public VW_tbSalidas Find(int? id)
        {
            throw new NotImplementedException();
        }

        public RequestStatus Insert(VW_tbSalidas item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VW_tbSalidas> List()
        {
            using var db = new SqlConnection(ProyectoGestionInventario.ConnectionString);
            var parametros = new DynamicParameters();
            return db.Query<VW_tbSalidas>(ScriptsDataBase.UDP_VW_tbSalidas_Select, null, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<VW_tbSalidas> List_Filtered(int? sucu, string? inicio, string? fin)
        {
            using var db = new SqlConnection(ProyectoGestionInventario.ConnectionString);
            var parametros = new DynamicParameters();

            if (sucu != 0)
            {
                parametros.Add("@sucu_Id", sucu, DbType.String, ParameterDirection.Input);
            }

            if (fin != "null" && inicio != "null")
            {
                parametros.Add("@fechaInicio", inicio, DbType.String, ParameterDirection.Input);
                parametros.Add("@fechaFin", fin, DbType.String, ParameterDirection.Input);
            }

            return db.Query<VW_tbSalidas>(ScriptsDataBase.UDP_VW_tbSalidas_Select_Filtered, parametros, commandType: CommandType.StoredProcedure);
        }

        public VW_tbSalidas Sucursal_Status(int id)
        {
            using var db = new SqlConnection(ProyectoGestionInventario.ConnectionString);
            var parametros = new DynamicParameters();
            parametros.Add("@sucu_Id", id, DbType.String, ParameterDirection.Input);
            return db.QueryFirst<VW_tbSalidas>(ScriptsDataBase.UDP_VW_tbSalidas_Select_Sucursal_Estado, parametros, commandType: CommandType.StoredProcedure);
  
        }

        public RequestStatus Update(VW_tbSalidas item)
        {
            throw new NotImplementedException();
        }
    }
}
