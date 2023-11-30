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
    public class SalidasRepository : IRepository<tbSalidas>
    {
        public RequestStatus Delete(tbSalidas item)
        {
            throw new NotImplementedException();
        }

        public tbSalidas Find(int? id)
        {
            throw new NotImplementedException();
        }

        public RequestStatus Insert(tbSalidas item)
        {
            using var db = new SqlConnection(ProyectoGestionInventario.ConnectionString);
            RequestStatus result = new RequestStatus();
            var parametros = new DynamicParameters();
            parametros.Add("@usua_Id", item.usua_UsuarioCreacion, DbType.String, ParameterDirection.Input);
            parametros.Add("@sucu_Id", item.sucu_Id, DbType.String, ParameterDirection.Input);
            parametros.Add("@sucu_SalidaEstado", item.sucu_SalidaEstado, DbType.String, ParameterDirection.Input);
            parametros.Add("@sali_FechaCreacion", item.sali_FechaCreacion, DbType.DateTime, ParameterDirection.Input);
            var resultado = db.QueryFirst<string>(ScriptsDataBase.UDP_tbSalidas_Insert, parametros, commandType: CommandType.StoredProcedure);
            result.MessageStatus = resultado;
            return result;
        }

        public IEnumerable<tbSalidas> List()
        {
            throw new NotImplementedException();
        }

        public RequestStatus Update(tbSalidas item)
        {
            using var db = new SqlConnection(ProyectoGestionInventario.ConnectionString);
            RequestStatus result = new RequestStatus();
            var parametros = new DynamicParameters();
            parametros.Add("@sali_Id", item.sali_Id, DbType.String, ParameterDirection.Input);
            parametros.Add("@usua_UsuarioModificacion", item.usua_UsuarioModificacion, DbType.String, ParameterDirection.Input);
            parametros.Add("@sali_FechaModificacion", item.saliFechaModificacion, DbType.DateTime, ParameterDirection.Input);
            var resultado = db.QueryFirst<string>(ScriptsDataBase.UDP_tbSalidas_Update, parametros, commandType: CommandType.StoredProcedure);
            result.MessageStatus = resultado;
            return result;
        }
    }
}
