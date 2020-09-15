using Users.UI.ViewModels;
using Web.Controllers;

namespace Users.Commands.UserCommands
{
    public class UpdateCommand : BaseCommand<int>
    {
        public UserAccountViewModel User { set; get; }

        public UpdateCommand(UserAccountViewModel user)
        {
            User = user;
        }
    }
}