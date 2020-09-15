using Common.Models;
using Web.Controllers;

namespace Users.Commands.AuthenCommands
{
    public class LoginAnotherCommand : BaseCommand<UserSession>
    {
        public int UserId { set; get; }

        public LoginAnotherCommand(int userId)
        {
            UserId = userId;
        }
    }
}