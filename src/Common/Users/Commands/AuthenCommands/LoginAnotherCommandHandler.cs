using Common.Exceptions;
using Common.Models;
using System.Threading;
using System.Threading.Tasks;
using Users.UI.Interfaces.Queries;
using Users.UI.Interfaces.Repositories;
using Web.Controllers;
using Web.Helpers;

namespace Users.Commands.AuthenCommands
{
    public class LoginAnotherCommandHandler : BaseCommandHandler<LoginAnotherCommand, UserSession>
    {
        private readonly IAuthenRepository authenRepository = null;
        private readonly IUserQueries userQueries = null;
        public LoginAnotherCommandHandler(IAuthenRepository authenRepository, IUserQueries userQueries)
        {
            this.authenRepository = authenRepository;
            this.userQueries = userQueries;
        }
        public override async Task<UserSession> HandleCommand(LoginAnotherCommand request, CancellationToken cancellationToken)
        {
            if (!request.LoginSession.IsSuperAdmin)
            {
                throw new NotPermissionException();
            }

            var user = await userQueries.Get(request.UserId);
            if (user == null)
            {
                throw new BusinessException("User.NotExistedAccount");
            }

            if (!user.IsUsed)
            {
                throw new BusinessException("User.NotUsedAccount");
            }

            if (!user.IsActived)
            {
                throw new BusinessException("User.NotActivedAccount");
            }

            var token = SessionHelper.CreateAccessToken(user.Username);
            return await authenRepository.LoginAnotherUser(request.LoginSession.SessionId, user, token);
        }
    }
}
