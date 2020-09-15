using System.Collections.Generic;
using Users.UI.Models;

namespace User.UI.ViewModels
{
    public class RoleViewModel : Role
    {
        public int TotalRecord { get; set; }
        public List<RolePermission> RolePermissions { set; get; } = new List<RolePermission>();
    }
}