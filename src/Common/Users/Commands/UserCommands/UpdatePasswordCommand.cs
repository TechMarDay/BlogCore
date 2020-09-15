using Web.Controllers;

namespace Users.Commands.UserCommands
{
    public class UpdatePasswordCommand : BaseCommand<int>
    {
        public int UserId { set; get; }
        public string Password { set; get; }

        public UpdatePasswordCommand(int userId, string password)
        {
            UserId = userId;
            Password = password;
        }
    }
}