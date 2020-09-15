using Common.Models;
using DAL;
using Dapper;
using System.Linq;
using System.Threading.Tasks;
using Users.UI.Interfaces.Repositories;
using Users.UI.ViewModels;

namespace Users.Repositories
{
    public class AuthenRepository : BaseRepository, IAuthenRepository
    {
        private const string SP_ACTIVEUSER = "sp_AuthenRepository_ActiveUser";
        private const string SP_CHANGEPASSWORD = "sp_AuthenRepository_ChangePassword";
        private const string SP_LOGIN = "sp_AuthenRepository_LogIn";
        private const string SP_LOGIN_ANOTHER = "sp_AuthenRepository_LoginAnotherUser";
        private const string SP_LOGOUT = "sp_AuthenRepository_LogOut";
        private const string SP_REGISTER_RESETPASS = "sp_AuthenRepository_RegisterResetPassword";
        private const string SP_RESETPASS = "sp_AuthenRepository_ResetPassword";

        public async Task<int> ActiveUser(int userId, string newPassword)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", userId, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@NewPassword", newPassword.GetSQLValue(), System.Data.DbType.String, System.Data.ParameterDirection.Input);

            return (await DalHelper.SPExecute(SP_ACTIVEUSER, param, dbTransaction: DbTransaction, connection: DbConnection)) > 0 ? 0 : -1;
        }

        public async Task<int> ChangePassword(int userId, string password)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", userId, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@PasswordStr".GetSQLValue(), password, System.Data.DbType.String, System.Data.ParameterDirection.Input);

            return (await DalHelper.SPExecute(SP_CHANGEPASSWORD, param, dbTransaction: DbTransaction, connection: DbConnection)) > 0 ? 0 : -1;
        }

        public async Task<UserSession> Login(UserAccountViewModel user, AccessTokenModel token)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", user.Id, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@AccessToken", token.AccessToken, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@LoginDate", token.LoginDate, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            param.Add("@ExpiredDate", token.ExpiredDate, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);

            var sessionId = (await DalHelper.SPExecuteQuery<long>(SP_LOGIN, param, dbTransaction: DbTransaction, connection: DbConnection)).First();

            return new UserSession()
            {
                LoginResult = 0,
                SessionId = sessionId,
                Username = user.Username,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                DisplayName = user.DisplayName,
                IsSuperAdmin = user.IsSuperAdmin,
                UserId = user.Id,
                RoleIds = user.Roles.Select(r => r.Id).ToList(),
                AccessToken = token.AccessToken
            };
        }

        public async Task<UserSession> LoginAnotherUser(long sessionId, UserAccountViewModel user, AccessTokenModel token)
        {
            var param = new DynamicParameters();
            param.Add("@SessionId", sessionId, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@UserId", user.Id, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@AccessToken", token.AccessToken, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@LoginDate", token.LoginDate, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            param.Add("@ExpiredDate", token.ExpiredDate, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);

            await DalHelper.SPExecute(SP_LOGIN_ANOTHER, param, dbTransaction: DbTransaction, connection: DbConnection);

            return new UserSession()
            {
                LoginResult = 0,
                SessionId = sessionId,
                Username = user.Username,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                DisplayName = user.DisplayName,
                IsSuperAdmin = user.IsSuperAdmin,
                UserId = user.Id,
                RoleIds = user.Roles.Select(r => r.Id).ToList(),
                AccessToken = token.AccessToken
            };
        }

        public async Task Logout(UserSession userSession)
        {
            var param = new DynamicParameters();
            param.Add("@SessionId", userSession.SessionId, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            await DalHelper.SPExecute(SP_LOGOUT, param, dbTransaction: DbTransaction, connection: DbConnection);
        }

        public async Task<int> RegisterResetPassword(string userName, string pinCode)
        {
            var param = new DynamicParameters();
            param.Add("@UserName", userName.GetSQLValue(), System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@PinCode", pinCode.GetSQLValue(), System.Data.DbType.String, System.Data.ParameterDirection.Input);

            return (await DalHelper.SPExecute(SP_REGISTER_RESETPASS, param, dbTransaction: DbTransaction, connection: DbConnection)) > 0 ? 0 : -1;
        }

        public async Task<int> ResetPassword(int userId, string password)
        {
            var param = new DynamicParameters();
            param.Add("@UseriD", userId, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@PasswordStr", password.GetSQLValue(), System.Data.DbType.String, System.Data.ParameterDirection.Input);

            return (await DalHelper.SPExecute(SP_RESETPASS, param, dbTransaction: DbTransaction, connection: DbConnection)) > 0 ? 0 : -1;
        }
    }
}