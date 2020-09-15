using Common.Exceptions;
using Common.Extensions;
using System.Threading;
using System.Threading.Tasks;
using Users.UI.Interfaces.Queries;
using Users.UI.Interfaces.Repositories;
using Web.Controllers;

namespace Users.Commands.AuthenCommands
{
    public class ChangePasswordCommandHandler : BaseCommandHandler<ChangePasswordCommand, int>
    {
        private readonly IAuthenRepository authenRepository = null;
        private readonly IUserQueries userQueries = null;
        public ChangePasswordCommandHandler(IAuthenRepository authenRepository, IUserQueries userQueries)
        {
            this.authenRepository = authenRepository;
            this.userQueries = userQueries;
        }
        public override async Task<int> HandleCommand(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await userQueries.Get(request.LoginSession.Username);
            if (user == null)
            {
                throw new BusinessException("User.NotExistedAccount");
            }

            if (user.Password != (request.OldPassword.Trim() + user.SecurityPassword.ToString()).CalculateMD5Hash())
            {
                throw new BusinessException("User.WrongOldPasswordAccount");
            }

            return await authenRepository.ChangePassword(request.LoginSession.UserId, (request.NewPassword.Trim() + user.SecurityPassword.ToString()).CalculateMD5Hash());
        }
    }
}
