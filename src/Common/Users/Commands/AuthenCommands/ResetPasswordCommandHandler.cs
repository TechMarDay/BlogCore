using Common.Exceptions;
using Common.Extensions;
using System.Threading;
using System.Threading.Tasks;
using Users.UI.Interfaces.Queries;
using Users.UI.Interfaces.Repositories;
using Web.Controllers;

namespace Users.Commands.AuthenCommands
{
    public class ResetPasswordCommandHandler : BaseCommandHandler<ResetPasswordCommand, int>
    {
        private readonly IAuthenRepository authenRepository = null;
        private readonly IUserQueries userQueries = null;
        public ResetPasswordCommandHandler(IAuthenRepository authenRepository, IUserQueries userQueries)
        {
            this.authenRepository = authenRepository;
            this.userQueries = userQueries;
        }
        public override async Task<int> HandleCommand(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await userQueries.Get(request.UserName);

            if (user == null || user.PasswordResetCode == null || !user.IsUsed || !user.IsActived)
            {
                throw new BusinessException("User.NotEnoughConditionResetPassword");
            }

            if (user.PasswordResetCode != request.PinCode)
            {
                throw new BusinessException("User.WrongPinCodeAccount");
            }

            return await authenRepository.ResetPassword(user.Id, (request.Password.Trim() + user.SecurityPassword.ToString()).CalculateMD5Hash());
        }
    }
}
