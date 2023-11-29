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
    public class SalidasDetallesRepository : IRepository<tbSalidasDetalles>
    {
        public RequestStatus Delete(tbSalidasDetalles item)
        {
            throw new NotImplementedException();
        }

        public tbSalidasDetalles Find(int? id)
        {
            throw new NotImplementedException();
        }

        public RequestStatus Insert(tbSalidasDetalles item)
        {
            using var db = new SqlConnection(ProyectoGestionInventario.ConnectionString);
            RequestStatus result = new RequestStatus();
            var parametros = new DynamicParameters();
            parametros.Add("@sali_Id", item.sali_Id, DbType.String, ParameterDirection.Input);
            parametros.Add("@lote_Id", item.lote_Id, DbType.String, ParameterDirection.Input);
            parametros.Add("@sade_Cantidad", item.sade_Cantidad, DbType.String, ParameterDirection.Input);
            parametros.Add("@usua_UsuarioCreacion", item.usua_UsuarioCreacion, DbType.String, ParameterDirection.Input);
            parametros.Add("@sade_FechaCreacion", item.sade_FechaCreacion, DbType.DateTime, ParameterDirection.Input);
            var resultado = db.QueryFirst<string>(ScriptsDataBase.UDP_tbSalidasDetalles_Insert, parametros, commandType: CommandType.StoredProcedure);
            result.MessageStatus = resultado;
            return result;
        }

        public IEnumerable<tbSalidasDetalles> List()
        {
            throw new NotImplementedException();
        }

        public RequestStatus Update(tbSalidasDetalles item)
        {
            throw new NotImplementedException();
        }
    }
}
