using Common.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Users.UI.Models;

namespace Users.UI.Interfaces.Queries
{
    public interface IRolePermissionQueries : IBaseQueries
    {
        Task<RolePermission> Get(int id);

        Task<IEnumerable<RolePermission>> Gets(int roleId);

        Task<IEnumerable<RolePermission>> Gets(string condition = "");
    }
}