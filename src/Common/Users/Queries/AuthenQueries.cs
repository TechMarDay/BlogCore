using Common.Models;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.UI.Interfaces.Queries;
using Users.UI.Models;

namespace Users.Queries
{
    public class AuthenQueries : BaseQueries, IAuthenQueries
    {
        private const string SP_GET = "sp_AuthenQueries_Get";
        private const string SP_GETS = "sp_AuthenQueries_Gets";

        public async Task<UserSession> Get(string accessToken)
        {
            UserSession result = null;
            var param = new DynamicParameters();
            param.Add("@AccessToken", accessToken, System.Data.DbType.String, System.Data.ParameterDirection.Input, 100);
            try
            {
                var rd = await DbConnection.QueryMultipleAsync(SP_GET, param, transaction: DbTransaction, commandType: System.Data.CommandType.StoredProcedure);
                rd.Read<UserAccessToken, UserAccount, Role, UserSession>(
                    (tkRs, uRs, rRs) =>
                    {
                        if (result == null)
                        {
                            result = new UserSession()
                            {
                                AccessToken = tkRs.AccessToken,
                                UserId = uRs.Id,
                                Email = uRs.Email,
                                IsSuperAdmin = uRs.IsSuperAdmin,
                                Username = uRs.Username,
                                SessionId = tkRs.Id,
                                LoginResult = 0,
                                LoginCaptionMessage = string.Empty
                            };
                        }

                        if (rRs != null)
                        {
                            var role = result.RoleIds.FirstOrDefault(r => r == rRs.Id);
                            if (role == 0)
                            {
                                result.RoleIds.Add(rRs.Id);
                            }
                        }
                        return result;
                    }
                );
                return result;
            }
            finally
            {
                if (!IsJoinTransaction)
                {
                    DbConnection.Dispose();
                }
            }
        }

        public async Task<IEnumerable<UserSession>> Gets()
        {
            List<UserSession> result = new List<UserSession>();
            try
            {
                var rd = await DbConnection.QueryMultipleAsync(SP_GETS, transaction: DbTransaction, commandType: System.Data.CommandType.StoredProcedure);
                rd.Read<UserAccessToken, UserAccount, Role, UserSession>(
                    (tkRs, uRs, rRs) =>
                    {
                        var session = result.FirstOrDefault(s => s.SessionId == tkRs.Id);
                        if (session == null)
                        {
                            session = new UserSession()
                            {
                                AccessToken = tkRs.AccessToken,
                                UserId = uRs.Id,
                                Email = uRs.Email,
                                IsSuperAdmin = uRs.IsSuperAdmin,
                                Username = uRs.Username,
                                SessionId = tkRs.Id,
                                LoginResult = 0,
                                LoginCaptionMessage = string.Empty
                            };
                            result.Add(session);
                        }

                        if (rRs != null)
                        {
                            var role = session.RoleIds.FirstOrDefault(r => r == rRs.Id);
                            if (role == 0)
                            {
                                session.RoleIds.Add(rRs.Id);
                            }
                        }
                        return session;
                    }
                );
                return result;
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