using Common.Interfaces;
using System.Threading.Tasks;
using Users.UI.Models;

namespace Users.UI.Interfaces.Repositories
{
    public interface IRoleRepository : IBaseRepository
    {
        Task<int> Add(Role role);

        Task<int> Update(Role role);
    }
}