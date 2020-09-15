using Common.Models;
using DAL;
using Dapper;
using System.Threading.Tasks;
using Users.UI.Interfaces.Repositories;
using Users.UI.Models;

namespace Users.Repositories
{
    public class UserRoleRepository : BaseRepository, IUserRoleRepository
    {
        private const string SP_USERROLE_ADD = "sp_UserRoleRepository_Add";
        private const string SP_USERROLE_DELETE = "sp_UserRoleRepository_Delete";

        public async Task<int> Add(UserAccountRole accountRole)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", accountRole.UserId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@RoleId", accountRole.RoleId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

            return await DalHelper.SPExecute(SP_USERROLE_ADD, param, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> DeleteRole(int userId)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", userId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

            return await DalHelper.SPExecute(SP_USERROLE_DELETE, param, dbTransaction: DbTransaction, connection: DbConnection);
        }
    }
}