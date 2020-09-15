using User.UI.ViewModels;
using Web.Controllers;

namespace Users.Commands.RoleCommands
{
    public class AddCommand : BaseCommand<int>
    {
        public RoleViewModel Role { set; get; }

        public AddCommand(RoleViewModel role)
        {
            Role = role;
        }
    }
}