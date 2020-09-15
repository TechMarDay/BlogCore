using Web.Controllers;

namespace Users.Commands.AuthenCommands
{
    public class RegisterResetPasswordCommand : BaseCommand<int>
    {
        public string UserName { set; get; }

        public RegisterResetPasswordCommand(string userName)
        {
            UserName = userName;
        }
    }
}