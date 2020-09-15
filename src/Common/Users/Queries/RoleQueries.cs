using Common.Models;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.UI.ViewModels;
using Users.UI.Interfaces.Queries;
using Users.UI.Models;

namespace Users.Queries
{
    public class RoleQueries : BaseQueries, IRoleQueries
    {
        private const string SP_GET = "sp_RoleQueries_Get";
        private const string SP_GET_BYNAME = "sp_RoleQueries_Get_ByName";
        private const string SP_GETS = "sp_RoleQueries_Gets";
        private const string SP_GET_LIST = "sp_RoleQueries_GetsPage";

        public async Task<RoleViewModel> Get(int id)
        {
            RoleViewModel result = null;
            var param = new DynamicParameters();
            param.Add("@Id", id, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            try
            {
                var rd = await DbConnection.QueryMultipleAsync(SP_GET, param, transaction: DbTransaction, commandType: System.Data.CommandType.StoredProcedure);
                rd.Read<Role, RolePermission, RoleViewModel>(
                    (rRs, pRs) =>
                    {
                        if (result == null)
                        {
                            result = null;
                        }

                        if (pRs != null)
                        {
                            var permission = result.RolePermissions.FirstOrDefault(p => p.Id == pRs.Id);
                            if (permission == null)
                            {
                                result.RolePermissions.Add(pRs);
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

        public async Task<RoleViewModel> GetByName(string name)
        {
            RoleViewModel result = null;
            var param = new DynamicParameters();
            param.Add("@RoleName", name, System.Data.DbType.String, System.Data.ParameterDirection.Input, 50);
            try
            {
                var rd = await DbConnection.QueryMultipleAsync(SP_GET_BYNAME, param, transaction: DbTransaction, commandType: System.Data.CommandType.StoredProcedure);
                rd.Read<Role, RolePermission, RoleViewModel>(
                    (rRs, pRs) =>
                    {
                        if (result == null)
                        {
                            result = null;
                        }

                        if (pRs != null)
                        {
                            var permission = result.RolePermissions.FirstOrDefault(p => p.Id == pRs.Id);
                            if (permission == null)
                            {
                                result.RolePermissions.Add(pRs);
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

        public async Task<IEnumerable<RoleViewModel>> List(int InputCurrentPage, int InputPageSize, string SearchText, int InputViewMode)
        {
            List<RoleViewModel> result = new List<RoleViewModel>();
            var param = new DynamicParameters();
            param.Add("@InputCurrentPage", InputCurrentPage, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@InputPageSize", InputPageSize, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@SearchText", SearchText, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@InputViewMode", InputViewMode, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            try
            {
                return await DbConnection.QueryAsync<RoleViewModel>(SP_GET_LIST, param, transaction: DbTransaction, commandType: System.Data.CommandType.StoredProcedure);
            }
            finally
            {
                if (!IsJoinTransaction)
                {
                    DbConnection.Dispose();
                }
            }
        }

        public async Task<IEnumerable<RoleViewModel>> Gets(string condition = "")
        {
            List<RoleViewModel> result = new List<RoleViewModel>();
            var param = new DynamicParameters();
            param.Add("@ConditionStr", condition, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            try
            {
                var rd = await DbConnection.QueryMultipleAsync(SP_GETS, param, transaction: DbTransaction, commandType: System.Data.CommandType.StoredProcedure);
                rd.Read<Role, RolePermission, RoleViewModel>(
                    (rRs, pRs) =>
                    {
                        var role = result.FirstOrDefault(r => r.Id == rRs.Id);
                        if (role == null)
                        {
                            role = null;
                            result.Add(role);
                        }

                        if (pRs != null)
                        {
                            var permission = role.RolePermissions.FirstOrDefault(p => p.Id == pRs.Id);
                            if (permission == null)
                            {
                                role.RolePermissions.Add(pRs);
                            }
                        }

                        return role;
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