using Web.Controllers;

namespace Users.Commands.AuthenCommands
{
    public class ActiveUserPasswordCommand : BaseCommand<int>
    {
        public string UserName { set; get; }
        public string Password { set; get; }

        public ActiveUserPasswordCommand(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}