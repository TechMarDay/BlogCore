using Common.Interfaces;
using System.Threading.Tasks;
using Users.UI.Models;

namespace Users.UI.Interfaces.Repositories
{
    public interface IRolePermissionRepository : IBaseRepository
    {
        Task<int> AddOrUpdate(RolePermission rolePermission);

        Task<int> Delete(int rolePermissionId);

        Task<int> DeleteByRole(int roleId);
    }
}