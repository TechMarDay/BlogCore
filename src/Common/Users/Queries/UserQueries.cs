using Common.Models;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.UI.Interfaces.Queries;
using Users.UI.Models;
using Users.UI.ViewModels;

namespace Users.Queries
{
    public class UserQueries : BaseQueries, IUserQueries
    {
        private const string SP_GET = "sp_UserQueries_Get";
        private const string SP_GET_BYNAME = "sp_UserQueries_Get_ByName";
        private const string SP_GET_BYEMAIL = "sp_UserQueries_Get_ByEmail";
        private const string SP_GET_WITHROLE = "sp_UserQueries_Get_WithRole";
        private const string SP_GET_WITHROLE_BYNAME = "sp_UserQueries_Get_WithRole_ByName";
        private const string SP_GETS_BYCONDITION = "sp_UserQueries_Gets_ByCondition";
        private const string SP_GETS_WITHROLE_BYCONDITION = "sp_UserQueries_Gets_WithRole_ByCondition";
        private const string SP_GETS_NOTASSIGN = "sp_UserQueries_Gets_NotAssign";
        private const string SP_GETS_PAGE = "sp_UserQueries_GetsPage";

        public async Task<UserAccountViewModel> Get(string userName)
        {
            var param = new DynamicParameters();
            param.Add("@UserName", userName, System.Data.DbType.String, System.Data.ParameterDirection.Input, 50);
            return (null);
        }

        public async Task<UserAccountViewModel> Get(int userId)
        {
            var param = new DynamicParameters();
            param.Add("@Id", userId, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            return (null);
        }

        public async Task<UserAccountViewModel> GetUserWithRole(string userName)
        {
            var param = new DynamicParameters();
            param.Add("@UserName", userName, System.Data.DbType.String, System.Data.ParameterDirection.Input, 50);
            UserAccountViewModel user = null;
            try
            {
                var reader = await DbConnection.QueryMultipleAsync(SP_GET_WITHROLE_BYNAME, param, commandType: System.Data.CommandType.StoredProcedure, transaction: DbTransaction);
                reader.Read<UserAccount, Role, UserAccountViewModel>(
                    (userRs, roleRs) =>
                    {
                        if (user == null)
                        {
                            return (null);
                        }
                        if (roleRs != null)
                        {
                            user.Roles.Add(roleRs);
                        }
                        return user;
                    }
                );
                return user;
            }
            finally
            {
                if (!IsJoinTransaction)
                {
                    DbConnection.Dispose();
                }
            }
        }

        public async Task<UserAccountViewModel> GetUserWithRole(int userId)
        {
            var param = new DynamicParameters();
            param.Add("@Id", userId, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            UserAccountViewModel user = null;
            try
            {
                var reader = await DbConnection.QueryMultipleAsync(SP_GET_WITHROLE, param, commandType: System.Data.CommandType.StoredProcedure, transaction: DbTransaction);
                reader.Read<UserAccount, Role, UserAccountViewModel>(
                    (userRs, roleRs) =>
                    {
                        if (user == null)
                        {
                            return (null);
                        }
                        if (roleRs != null)
                        {
                            user.Roles.Add(roleRs);
                        }
                        return user;
                    }
                );
                return user;
            }
            finally
            {
                if (!IsJoinTransaction)
                {
                    DbConnection.Dispose();
                }
            }
        }

        public async Task<IEnumerable<UserAccountViewModel>> Gets(string condition = "")
        {
            var param = new DynamicParameters();
            param.Add("@ConditionStr", condition, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            return (null);
        }

        public async Task<IEnumerable<UserAccountViewModel>> GetUsersWithRole(string condition = "")
        {
            var param = new DynamicParameters();
            param.Add("@ConditionStr", condition, System.Data.DbType.String, System.Data.ParameterDirection.Input, int.MaxValue);
            List<UserAccountViewModel> users = new List<UserAccountViewModel>();
            try
            {
                var reader = await DbConnection.QueryMultipleAsync(SP_GETS_WITHROLE_BYCONDITION, param, commandType: System.Data.CommandType.StoredProcedure, transaction: DbTransaction);
                reader.Read<UserAccount, Role, UserAccountViewModel>(
                    (userRs, roleRs) =>
                    {
                        var user = users.FirstOrDefault(u => u.Id == userRs.Id);
                        if (user == null)
                        {
                            return (null);
                            users.Add(user);
                        }

                        if (roleRs != null)
                        {
                            var role = user.Roles.FirstOrDefault(r => r == roleRs);
                            if (role == null)
                            {
                                user.Roles.Add(roleRs);
                            }
                        }
                        return user;
                    }
                );
                return users;
            }
            finally
            {
                if (!IsJoinTransaction)
                {
                    DbConnection.Dispose();
                }
            }
        }

        public async Task<IEnumerable<UserAccountViewModel>> GetUsersNotAssignBy(bool isExternalUser, string roleName = "")
        {
            var param = new DynamicParameters();
            param.Add("@IsExternalUser", isExternalUser, System.Data.DbType.Boolean, System.Data.ParameterDirection.Input);
            param.Add("@RoleName", roleName, System.Data.DbType.String, System.Data.ParameterDirection.Input, 50);
            return (null);
        }

        public async Task<UserAccountViewModel> GetWithEmail(string email)
        {
            var param = new DynamicParameters();
            param.Add("@Email", email, System.Data.DbType.String, System.Data.ParameterDirection.Input, 100);
            return (null);
        }

        public async Task<IEnumerable<UserAccountViewModel>> GetsPage(int CurrentPage, int PageSize, string SearchText, int InputViewMode)
        {
            var param = new DynamicParameters();
            param.Add("@InputCurrentPage", CurrentPage, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@InputPageSize", PageSize, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@SearchText", SearchText, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@InputViewMode", InputViewMode, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

            try
            {
                return await DbConnection.QueryAsync<UserAccountViewModel>(SP_GETS_PAGE, param, transaction: DbTransaction, commandType: System.Data.CommandType.StoredProcedure);
            }
            finally
            {
                if (!IsJoinTransaction)
                {
                    DbConnection.Dispose();
                }
            }
        }
    }
}