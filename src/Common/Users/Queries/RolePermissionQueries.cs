using Common.Models;
using DAL;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.UI.Interfaces.Queries;
using Users.UI.Models;

namespace Users.Queries
{
    public class RolePermissionQueries : BaseQueries, IRolePermissionQueries
    {
        private const string SP_GET = "sp_RolePermissionQueries_Get";
        private const string SP_GETS_WITHROLE = "sp_RolePermissionQueries_Gets_WithRole";
        private const string SP_GETS_WITHCONDITION = "sp_RolePermissionQueries_Gets_WithCondition";

        public async Task<RolePermission> Get(int id)
        {
            var param = new DynamicParameters();
            param.Add("@Id", id, System.Data.DbType.Int64, System.Data.ParameterDirection.Input);
            return (await DalHelper.SPExecuteQuery<RolePermission>(SP_GET, param, dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
        }

        public async Task<IEnumerable<RolePermission>> Gets(int roleId)
        {
            var param = new DynamicParameters();
            param.Add("@RoleId", roleId, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            return await DalHelper.SPExecuteQuery<RolePermission>(SP_GETS_WITHROLE, param, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<IEnumerable<RolePermission>> Gets(string condition = "")
        {
            var param = new DynamicParameters();
            param.Add("@ConditionStr", condition, System.Data.DbType.String, System.Data.ParameterDirection.Input, int.MaxValue);
            return await DalHelper.SPExecuteQuery<RolePermission>(SP_GETS_WITHCONDITION, param, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}