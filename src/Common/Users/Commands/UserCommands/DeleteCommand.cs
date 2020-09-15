using Web.Controllers;

namespace Users.Commands.UserCommands
{
    public class DeleteCommand : BaseCommand<int>
    {
        public int UserId { set; get; }

        public DeleteCommand(int userId)
        {
            UserId = userId;
        }
    }
}