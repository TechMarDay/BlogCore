using System.Collections.Generic;
using Users.UI.Models;
using Web.Controllers;

namespace Users.Commands.RolePermissionCommands
{
    public class UpdateCommand : BaseCommand<int>
    {
        public int RoleId { set; get; }
        public List<RolePermission> Permissions { set; get; }

        public UpdateCommand(int roleId, List<RolePermission> permissions)
        {
            RoleId = roleId;
            Permissions = permissions;
        }
    }
}