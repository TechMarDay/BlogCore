using System.Threading;
using System.Threading.Tasks;
using Users.UI.Interfaces.Queries;
using Users.UI.Interfaces.Repositories;
using Web.Controllers;

namespace Users.Commands.RoleCommands
{
    public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
    {
        private readonly IRoleRepository roleRepository = null;
        private readonly IRoleQueries roleQueries = null;
        public DeleteCommandHandler(IRoleRepository roleRepository, IRoleQueries roleQueries)
        {
            this.roleRepository = roleRepository;
            this.roleQueries = roleQueries;
        }
        public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
        {
            var role = await roleQueries.Get(request.RoleId);
            if (role != null && !role.IsDeleted)
            {
                role = DeleteBuild(role, request.LoginSession);
                return await roleRepository.Update(role);
            }
            return 0;
        }
    }
}
