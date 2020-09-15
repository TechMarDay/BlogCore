using Common.Exceptions;
using Common.Extensions;
using System.Threading;
using System.Threading.Tasks;
using Users.UI.Interfaces.Queries;
using Users.UI.Interfaces.Repositories;
using Web.Controllers;

namespace Users.Commands.UserCommands
{
    public class UpdatePasswordCommandHandler : BaseCommandHandler<UpdatePasswordCommand, int>
    {
        private readonly IUserRepository userRepository = null;
        private readonly IUserQueries userQueries = null;
        public UpdatePasswordCommandHandler(IUserRepository userRepository, IUserQueries userQueries)
        {
            this.userRepository = userRepository;
            this.userQueries = userQueries;
        }
        public override async Task<int> HandleCommand(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await userQueries.Get(request.UserId);
            if (user == null)
            {
                throw new BusinessException("User.NotExistedAccount");
            }
            user.Password = (request.Password.Trim() + user.SecurityPassword.ToString()).CalculateMD5Hash();
            user = UpdateBuild(user, request.LoginSession);
            return await userRepository.Update(user);
        }
    }
}
