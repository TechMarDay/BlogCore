using Common.Exceptions;
using Common.Helpers;
using DAL;
using System;
using System.Threading;
using System.Threading.Tasks;
using Users.UI.Interfaces.Queries;
using Users.UI.Interfaces.Repositories;
using Web.Controllers;

namespace Users.Commands.UserCommands
{
    public class ChangeRolesCommandHandler : BaseCommandHandler<ChangeRolesCommand, int>
    {
        private readonly IUserQueries userQueries = null;
        private readonly IRoleQueries roleQueries = null;
        private readonly IUserRoleRepository userRoleRepository = null;
        public ChangeRolesCommandHandler(IRoleQueries roleQueries, IUserQueries userQueries, IUserRoleRepository userRoleRepository)
        {
            this.userRoleRepository = userRoleRepository;
            this.userQueries = userQueries;
            this.roleQueries = roleQueries;
        }
        public override async Task<int> HandleCommand(ChangeRolesCommand request, CancellationToken cancellationToken)
        {
            var user = await userQueries.Get(request.UserId);
            if (user == null)
            {
                throw new BusinessException("NotExistedAccount");
            }
            if (user.IsExternalUser)
            {
                throw new BusinessException("ExternalAccount");
            }
            var rs = -1;
            using (var conn = DalHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        userRoleRepository.JoinTransaction(conn, trans);
                        roleQueries.JoinTransaction(conn, trans);
                        await userRoleRepository.DeleteRole(user.Id);
                        foreach (var item in request.RoleIds)
                        {
                            var role = await roleQueries.Get(item);
                            if (role != null && role.IsActive && !role.IsExternalRole && role.Id != 1) // != Administrator
                            {
                                await userRoleRepository.Add(new UI.Models.UserAccountRole()
                                {
                                    RoleId = role.Id,
                                    UserId = user.Id
                                });
                            }
                        }
                        return rs = 0;
                    }
                    finally
                    {
                        if (rs == 0)
                        {
                            trans.Commit();
                        }
                        else
                        {
                            try
                            {
                                trans.Rollback();
                            }
                            catch (Exception ex)
                            {
                                LogHelper.GetLogger().Error(ex);
                            }
                        }
                    }
                }
            }
        }
    }
}
