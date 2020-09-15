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
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly IRoleRepository roleRepository = null;
        private readonly IRoleQueries roleQueries = null;
        private readonly IRolePermissionRepository rolePermissionRepository = null;
        public UpdateCommandHandler(IRoleRepository roleRepository, IRoleQueries roleQueries, IRolePermissionRepository rolePermissionRepository)
        {
            this.roleRepository = roleRepository;
            this.roleQueries = roleQueries;
            this.rolePermissionRepository = rolePermissionRepository;
        }
        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
        {
            if (request.Role == null || request.Role.Id == 0)
            {
                throw new BusinessException("Common.WrongInput");
            }
            var role = await roleQueries.Get(request.Role.Id);
            if (role == null)
            {
                throw new BusinessException("User.NotExistedRole");
            }

            var checkingRole = await roleQueries.GetByName(request.Role.Name);
            if (checkingRole != null && checkingRole.Id != request.Role.Id)
            {
                throw new BusinessException("User.ExistedRole");
            }

            var rs = -1;
            using (var conn = DalHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        roleRepository.JoinTransaction(conn, trans);
                        rolePermissionRepository.JoinTransaction(conn, trans);

                        role.Name = request.Role.Name;
                        role.Description = request.Role.Description;
                        role.IsActive = request.Role.IsActive;
                        role = UpdateBuild(role, request.LoginSession);
                        var rsUp = await roleRepository.Update(role);

                        if (rsUp == -1) return rs;

                        foreach (var item in request.Role.RolePermissions)
                        {
                            item.RoleId = role.Id;
                            await rolePermissionRepository.AddOrUpdate(item);
                        }

                        rs = 0;
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

            return rs;
        }
    }
}
