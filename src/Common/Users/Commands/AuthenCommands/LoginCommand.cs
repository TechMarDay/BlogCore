using Common.Models;
using Web.Controllers;

namespace Users.Commands.AuthenCommands
{
    public class LoginCommand : BaseCommand<UserSession>
    {
        public string UserName { set; get; }
        public string Password { set; get; }
        public bool IsRememberMe { get; set; }

        public LoginCommand(string userName, string password, bool isRememberMe)
        {
            UserName = userName;
            Password = password;
            IsRememberMe = isRememberMe;
        }
    }
}