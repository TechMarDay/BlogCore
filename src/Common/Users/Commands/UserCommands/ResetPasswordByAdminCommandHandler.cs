using Common;
using Common.Exceptions;
using Common.Extensions;
using System.Threading;
using System.Threading.Tasks;
using Users.UI.Interfaces.Queries;
using Users.UI.Interfaces.Repositories;
using Web.Controllers;

namespace Users.Commands.UserCommands
{
    public class ResetPasswordByAdminCommandHandler : BaseCommandHandler<ResetPasswordByAdminCommand, int>
    {
        private readonly IUserRepository userRepository = null;
        private readonly IUserQueries userQueries = null;
        public ResetPasswordByAdminCommandHandler(IUserRepository userRepository, IUserQueries userQueries)
        {
            this.userRepository = userRepository;
            this.userQueries = userQueries;
        }
        public override async Task<int> HandleCommand(ResetPasswordByAdminCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == 1)
            {
                return 0;
            }
            //check existed username
            var user = await userQueries.Get(request.UserId);
            if (user == null)
            {
                throw new BusinessException("User.NotExistedAccount");
            }

            user.Password = (GlobalConfiguration.DefaultResetPassword + user.SecurityPassword.ToString()).CalculateMD5Hash();
            user = UpdateBuild(user, request.LoginSession);
            return await userRepository.Update(user);
        }
    }
}
