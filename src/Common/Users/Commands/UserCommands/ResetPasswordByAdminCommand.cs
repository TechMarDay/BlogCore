using Web.Controllers;

namespace Users.Commands.UserCommands
{
    public class ResetPasswordByAdminCommand : BaseCommand<int>
    {
        public int UserId { set; get; }

        public ResetPasswordByAdminCommand(int userId)
        {
            UserId = userId;
        }
    }
}