using Common.Interfaces;
using System.Threading.Tasks;
using Users.UI.Models;

namespace Users.UI.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository
    {
        Task<int> Add(UserAccount user, int? userRegister = null);

        Task<int> Update(UserAccount user);
    }
}