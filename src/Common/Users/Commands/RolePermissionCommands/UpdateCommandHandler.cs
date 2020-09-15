using Common.Exceptions;
using System.Threading;
using System.Threading.Tasks;
using Users.UI.Interfaces.Queries;
using Users.UI.Interfaces.Repositories;
using Web.Controllers;

namespace Users.Commands.RolePermissionCommands
{
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly IRoleQueries roleQueries = null;
        private readonly IRolePermissionRepository rolePermissionRepository = null;
        public UpdateCommandHandler(IRoleQueries roleQueries, IUserQueries userQueries, IRolePermissionRepository rolePermissionRepository)
        {
            this.roleQueries = roleQueries;
            this.rolePermissionRepository = rolePermissionRepository;
        }
        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
        {
            var role = await roleQueries.Get(request.RoleId);

            if (role == null)
            {
                throw new BusinessException("User.NotExsitedRole");
            }

            foreach (var item in request.Permissions)
            {
                item.RoleId = request.RoleId;
                await rolePermissionRepository.AddOrUpdate(item);
            }

            return 0;
        }
    }
}
