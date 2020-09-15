using Common.Exceptions;
using Common.Extensions;
using System.Threading;
using System.Threading.Tasks;
using Users.UI.Interfaces.Queries;
using Users.UI.Interfaces.Repositories;
using Web.Controllers;

namespace Users.Commands.AuthenCommands
{
    public class ActiveUserPasswordCommandHandler : BaseCommandHandler<ActiveUserPasswordCommand, int>
    {
        private readonly IAuthenRepository authenRepository = null;
        private readonly IUserQueries userQueries = null;
        public ActiveUserPasswordCommandHandler(IAuthenRepository authenRepository, IUserQueries userQueries)
        {
            this.authenRepository = authenRepository;
            this.userQueries = userQueries;
        }
        public override async Task<int> HandleCommand(ActiveUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await userQueries.Get(request.UserName);

            if (user != null)
            {
                if (user.IsActived)
                {
                    throw new BusinessException("User.ActivedAccount");
                }
                else
                {
                    return await authenRepository.ActiveUser(user.Id, (request.Password + user.SecurityPassword.ToString()).CalculateMD5Hash());
                }
            }
            throw new BusinessException("User.NotExistedAccount");
        }
    }
}
