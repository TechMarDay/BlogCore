using System.Collections.Generic;
using Web.Controllers;

namespace Users.Commands.UserCommands
{
    public class ChangeRolesCommand : BaseCommand<int>
    {
        public int UserId { set; get; }
        public List<int> RoleIds { set; get; }

        public ChangeRolesCommand(int userId, List<int> roleIds)
        {
            UserId = userId;
            RoleIds = roleIds;
        }
    }
}