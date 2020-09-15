using Common.Models;
using DAL;
using Dapper;
using System.Threading.Tasks;
using Users.UI.Interfaces.Repositories;
using Users.UI.Models;

namespace Users.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private const string SP_USER_ADD = "sp_UserRepository_Add";
        private const string SP_USER_UPDATE = "sp_UserRepository_Update";

        public async Task<int> Add(UserAccount user, int? userRegister = null)
        {
            var param = new DynamicParameters();
            param.Add("@UserName", user.Username, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@Password", user.Password, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@SecurityPassword", user.SecurityPassword, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@Email", user.Email, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@DisplayName", user.DisplayName, System.Data.DbType.String, System.Data.ParameterDirection.Input, 100);
            param.Add("@PhoneNumber", user.PhoneNumber, System.Data.DbType.String, System.Data.ParameterDirection.Input, 15);
            param.Add("@PasswordResetcode", user.PasswordResetCode, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@IsExternalUser", user.IsExternalUser, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@IsSuperadmin", user.IsSuperAdmin, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@IsActivated", user.IsActived, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@IsUsed", user.IsUsed, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@IsDeleted", user.IsDeleted, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@CreatedDate", user.CreatedDate, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            param.Add("@CreatedBy", user.CreatedBy, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@ModifiedDate", user.ModifiedDate, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            param.Add("@ModifiedBy", user.ModifiedBy, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

            return await DalHelper.SPExecute(SP_USER_ADD, param, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> Update(UserAccount user)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", user.Id, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@UserName", user.Username, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@Password", user.Password, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@SecurityPassword", user.SecurityPassword, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@Email", user.Email, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@DisplayName", user.DisplayName, System.Data.DbType.String, System.Data.ParameterDirection.Input, 100);
            param.Add("@PhoneNumber", user.PhoneNumber, System.Data.DbType.String, System.Data.ParameterDirection.Input, 15);
            param.Add("@PasswordResetcode", user.PasswordResetCode, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@IsExternalUser", user.IsExternalUser, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@IsSuperadmin", user.IsSuperAdmin, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@IsActivated", user.IsActived, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@IsUsed", user.IsUsed, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@IsDeleted", user.IsDeleted, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@CreatedDate", user.CreatedDate, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            param.Add("@CreatedBy", user.CreatedBy, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@ModifiedDate", user.ModifiedDate, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            param.Add("@ModifiedBy", user.ModifiedBy, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

            return (await DalHelper.SPExecute(SP_USER_UPDATE, param, dbTransaction: DbTransaction, connection: DbConnection)) > 0 ? 0 : -1;
        }
    }
}