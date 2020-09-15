using Common.Models;
using DAL;
using Dapper;
using System.Linq;
using System.Threading.Tasks;
using Users.UI.Interfaces.Repositories;
using Users.UI.Models;

namespace Users.Repositories
{
    public class RolePermissionRepository : BaseRepository, IRolePermissionRepository
    {
        private const string SP_CHECKEXIST = "sp_RolePermissionRepository_CheckExist";
        private const string SP_ROLE_PERMISSION_ADD = "sp_RolePermissionRepository_Add";
        private const string SP_ROLE_PERMISSION_UPDATE = "sp_RolePermissionRepository_Update";

        private const string SP_ROLE_PERMISSION_DELETE = "sp_RolePermissionRepository_Delete";
        private const string SP_ROLE_PERMISSION_DELETEBYROLE = "sp_RolePermissionRepository_DeleteByRole";

        public async Task<int> AddOrUpdate(RolePermission rolePermission)
        {
            var param = new DynamicParameters();
            param.Add("@RoleId", rolePermission.RoleId, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@FeatureId", rolePermission.FeatureId, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@PermissionId", rolePermission.PermissionId, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@IsEnabled", rolePermission.IsEnabled, System.Data.DbType.Boolean, System.Data.ParameterDirection.Input);

            var permission = (await DalHelper.SPExecuteQuery<RolePermission>(SP_CHECKEXIST, param, dbTransaction: DbTransaction, connection: DbConnection)).FirstOrDefault();
            if (permission != null)
            {
                return (await DalHelper.SPExecute(SP_ROLE_PERMISSION_UPDATE, param, dbTransaction: DbTransaction, connection: DbConnection)) > 0 ? 0 : -1;
            }
            else
            {
                return (await DalHelper.SPExecuteQuery<int>(SP_ROLE_PERMISSION_ADD, param, dbTransaction: DbTransaction, connection: DbConnection)).First();
            }
        }

        public async Task<int> Delete(int rolePermissionId)
        {
            var param = new DynamicParameters();
            param.Add("@RolePermissionId", rolePermissionId, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

            return await DalHelper.SPExecute(SP_ROLE_PERMISSION_DELETE, param, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> DeleteByRole(int roleId)
        {
            var param = new DynamicParameters();
            param.Add("@RoleId", roleId, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

            return await DalHelper.SPExecute(SP_ROLE_PERMISSION_DELETEBYROLE, param, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}