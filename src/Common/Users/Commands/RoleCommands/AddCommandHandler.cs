using Common.Exceptions;
using Common.Helpers;
using DAL;
using System;
using System.Threading;
using System.Threading.Tasks;
using Users.UI.Interfaces.Queries;
using Users.UI.Interfaces.Repositories;
using Web.Controllers;

namespace Users.Commands.RoleCommands
{
    public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
    {
        private readonly IRoleRepository roleRepository = null;
        private readonly IRoleQueries roleQueries = null;
        private readonly IRolePermissionRepository rolePermissionRepository = null;
        public AddCommandHandler(IRoleRepository roleRepository, IRoleQueries roleQueries, IRolePermissionRepository rolePermissionRepository)
        {
            this.roleRepository = roleRepository;
            this.roleQueries = roleQueries;
            this.rolePermissionRepository = rolePermissionRepository;
        }
        public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
        {
            if (request.Role == null || string.IsNullOrEmpty(request.Role.Name))
            {
                throw new BusinessException("Common.WrongInput");
            }

            var checkingRole = await roleQueries.GetByName(request.Role.Name);
            if (checkingRole != null)
            {
                throw new BusinessException("User.ExistedRole");
            }

            var roleId = -1;
            using (var conn = DalHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        roleRepository.JoinTransaction(conn, trans);
                        rolePermissionRepository.JoinTransaction(conn, trans);

                        request.Role.IsExternalRole = false;
                        request.Role = CreateBuild(request.Role, request.LoginSession);
                        roleId = await roleRepository.Add(request.Role);

                        foreach (var item in request.Role.RolePermissions)
                        {
                            item.RoleId = roleId;
                            await rolePermissionRepository.AddOrUpdate(item);
                        }
                    }
                    finally
                    {
                        if (roleId > 0)
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

            return roleId;
        }
    }
}
