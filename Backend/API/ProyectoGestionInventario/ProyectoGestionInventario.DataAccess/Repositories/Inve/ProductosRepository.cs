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
            using var db = new SqlConnection(ProyectoGestionInventario.ConnectionString);
            RequestStatus result = new RequestStatus();
            var parametros = new DynamicParameters();
            parametros.Add("@prod_Descripcion", item.prod_Descripcion, DbType.String, ParameterDirection.Input);
            parametros.Add("@prod_Precio", item.prod_Precio, DbType.String, ParameterDirection.Input);
            parametros.Add("@usua_UsuarioCreacion", item.usua_UsuarioCreacion, DbType.String, ParameterDirection.Input);
            parametros.Add("@prod_FechaCreacion", item.prod_FechaCreacion, DbType.DateTime, ParameterDirection.Input);
            var resultado = db.QueryFirst<string>(ScriptsDataBase.UDP_tbProductos_Insert, parametros, commandType: CommandType.StoredProcedure);
            result.MessageStatus = resultado;
            return result;
        }

        public IEnumerable<tbProductos> List()
        {
            using var db = new SqlConnection(ProyectoGestionInventario.ConnectionString);
            var parametros = new DynamicParameters();
            return db.Query<tbProductos>(ScriptsDataBase.UDP_tbProductos_Select, null, commandType: CommandType.StoredProcedure);
        }

        public RequestStatus Update(tbProductos item)
        {
            throw new NotImplementedException();
        }
    }
}
