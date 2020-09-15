using User.UI.ViewModels;
using Web.Controllers;

namespace Users.Commands.RoleCommands
{
    public class UpdateCommand : BaseCommand<int>
    {
        public RoleViewModel Role { set; get; }

        public UpdateCommand(RoleViewModel role)
        {
            Role = role;
        }
    }
}