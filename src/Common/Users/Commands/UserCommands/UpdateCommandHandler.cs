using Common.Exceptions;
using System.Threading;
using System.Threading.Tasks;
using Users.UI.Interfaces.Queries;
using Users.UI.Interfaces.Repositories;
using Web.Controllers;

namespace Users.Commands.UserCommands
{
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly IUserRepository userRepository = null;
        private readonly IUserQueries userQueries = null;
        public UpdateCommandHandler(IUserRepository userRepository, IUserQueries userQueries)
        {
            this.userRepository = userRepository;
            this.userQueries = userQueries;
        }
        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
        {
            if (request.User.Id == 1) //Administrator
            {
                return 0;
            }
            var checkingUser = await userQueries.GetWithEmail(request.User.Email);
            if (checkingUser != null && checkingUser.Id != request.User.Id)
            {
                throw new BusinessException("User.ExistedEmail");
            }

            var user = await userQueries.Get(request.User.Id);
            if (user == null)
            {
                throw new BusinessException("User.NotExistedAccount");
            }

            user.DisplayName = request.User.DisplayName;
            user.PhoneNumber = request.User.PhoneNumber;
            user.Email = request.User.Email;
            user.IsUsed = user.IsExternalUser || request.User.IsUsed;
            user = UpdateBuild(user, request.LoginSession);
            return await userRepository.Update(user);
        }
    }
}
