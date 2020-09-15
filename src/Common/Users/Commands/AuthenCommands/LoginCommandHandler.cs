using Common.Exceptions;
using Common.Extensions;
using Common.Models;
using System.Threading;
using System.Threading.Tasks;
using Users.UI.Interfaces.Queries;
using Users.UI.Interfaces.Repositories;
using Web.Controllers;
using Web.Helpers;

namespace Users.Commands.AuthenCommands
{
    public class LoginCommandHandler : BaseCommandHandler<LoginCommand, UserSession>
    {
        private readonly IAuthenRepository authenRepository = null;
        private readonly IUserQueries userQueries = null;
        public LoginCommandHandler(IAuthenRepository authenRepository, IUserQueries userQueries)
        {
            this.authenRepository = authenRepository;
            this.userQueries = userQueries;
        }
        public override async Task<UserSession> HandleCommand(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await userQueries.GetUserWithRole(request.UserName);

            if (user == null)
            {
                throw new BusinessException("User.NotExistedAccount");
            }

            if (!user.IsUsed)
            {
                throw new BusinessException("User.LockedAccount");
            }

            if (!user.IsActived)
            {
                throw new BusinessException("User.NotActivedAccount");
            }

            //check password
            if (!(request.Password.Trim() + user.SecurityPassword).CalculateMD5Hash().Equals(user.Password))
            {
                throw new BusinessException("User.WrongPasswordAccount");
            }

            var token = SessionHelper.CreateAccessToken(user.Username, request.IsRememberMe);
            var userSession = await authenRepository.Login(user, token);

            return userSession;
        }
    }
}
