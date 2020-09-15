using Web.Controllers;

namespace Users.Commands.RoleCommands
{
    public class DeleteCommand : BaseCommand<int>
    {
        public int RoleId { set; get; }

        public DeleteCommand(int roleId)
        {
            RoleId = roleId;
        }
    }
}