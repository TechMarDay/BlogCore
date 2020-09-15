using System.Threading;
using System.Threading.Tasks;
using Users.UI.Interfaces.Repositories;
using Web.Controllers;

namespace Users.Commands.AuthenCommands
{
    public class LogoutCommandHandler : BaseCommandHandler<LogoutCommand, int>
    {
        private readonly IAuthenRepository authenRepository = null;
        public LogoutCommandHandler(IAuthenRepository authenRepository)
        {
            this.authenRepository = authenRepository;
        }
        public override async Task<int> HandleCommand(LogoutCommand request, CancellationToken cancellationToken)
        {
            if (request.LoginSession != null)
            {
                await authenRepository.Logout(request.LoginSession);
            }
            return 0;
        }
    }
}
