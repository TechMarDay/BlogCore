using Common.Models;
using DAL;
using Dapper;
using System.Linq;
using System.Threading.Tasks;
using Users.UI.Interfaces.Repositories;
using Users.UI.Models;

namespace Users.Repositories
{
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        private const string SP_ROLE_ADD = "sp_RoleRepository_Add";
        private const string SP_ROLE_UPDATE = "sp_RoleRepository_Update";

        public async Task<int> Add(Role role)
        {
            var param = new DynamicParameters();
            param.Add("@Name", role.Name, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@Description", role.Description, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@IsExternalRole", role.IsExternalRole, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@IsActive", role.IsActive, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@IsDeleted", role.IsDeleted, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@CreatedDate", role.CreatedDate, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            param.Add("@CreatedBy", role.CreatedBy, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@ModifiedDate", role.ModifiedDate, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            param.Add("@ModifiedBy", role.ModifiedBy, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

            return (await DalHelper.SPExecuteQuery<int>(SP_ROLE_ADD, param, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> Update(Role role)
        {
            var param = new DynamicParameters();
            param.Add("@Id", role.Id, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@Name", role.Name, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@Description", role.Description, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@IsExternalRole", role.IsExternalRole, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@IsActive", role.IsActive, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@IsDeleted", role.IsDeleted, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@CreatedDate", role.CreatedDate, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            param.Add("@CreatedBy", role.CreatedBy, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@ModifiedDate", role.ModifiedDate, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            param.Add("@ModifiedBy", role.ModifiedBy, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

            return (await DalHelper.SPExecute(SP_ROLE_UPDATE, param, dbTransaction: DbTransaction, connection: DbConnection)) > 0 ? 0 : -1;
        }
    }
}