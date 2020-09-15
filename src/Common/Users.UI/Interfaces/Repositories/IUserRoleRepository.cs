using Common.Interfaces;
using System.Threading.Tasks;
using Users.UI.Models;

namespace Users.UI.Interfaces.Repositories
{
    public interface IUserRoleRepository : IBaseRepository
    {
        Task<int> Add(UserAccountRole accountRole);

        Task<int> DeleteRole(int userId);
    }
}