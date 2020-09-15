using Common.Helpers;
using System.Threading;
using System.Threading.Tasks;
using Users.UI.Interfaces.Repositories;
using Web.Controllers;

namespace Users.Commands.AuthenCommands
{
    public class RegisterResetPasswordCommandHandler : BaseCommandHandler<RegisterResetPasswordCommand, int>
    {
        private readonly IAuthenRepository authenRepository = null;
        public RegisterResetPasswordCommandHandler(IAuthenRepository authenRepository)
        {
            this.authenRepository = authenRepository;
        }
        public override async Task<int> HandleCommand(RegisterResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var pinCode = CommonHelper.GenerateRandomString(8, 2);
            var rs = await authenRepository.RegisterResetPassword(request.UserName, pinCode);

            //Send notify by email here

            return rs;
        }
    }
}
