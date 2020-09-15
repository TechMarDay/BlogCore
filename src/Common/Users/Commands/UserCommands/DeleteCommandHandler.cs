using System.Threading;
using System.Threading.Tasks;
using Users.UI.Interfaces.Queries;
using Users.UI.Interfaces.Repositories;
using Web.Controllers;

namespace Users.Commands.UserCommands
{
    public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
    {
        private readonly IUserRepository userRepository = null;
        private readonly IUserQueries userQueries = null;
        public DeleteCommandHandler(IUserRepository userRepository, IUserQueries userQueries)
        {
            this.userRepository = userRepository;
            this.userQueries = userQueries;
        }
        public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == 1) //Administrator
            {
                return 0;
            }
            var data = await userQueries.Get(request.UserId);
            if (data != null && !data.IsDeleted)
            {
                data = DeleteBuild(data, request.LoginSession);
                return await userRepository.Update(data);
            }
            return 0;
        }
    }
}
