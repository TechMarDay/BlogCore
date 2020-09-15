using Common.Interfaces;
using Common.Models;
using System.Threading.Tasks;
using Users.UI.ViewModels;

namespace Users.UI.Interfaces.Repositories
{
    public interface IAuthenRepository : IBaseRepository
    {
        Task<UserSession> Login(UserAccountViewModel user, AccessTokenModel token);

        Task<UserSession> LoginAnotherUser(long sessionId, UserAccountViewModel user, AccessTokenModel token);

        Task Logout(UserSession userSession);

        Task<int> ActiveUser(int userId, string newPassword);

        Task<int> RegisterResetPassword(string userName, string pinCode);

        Task<int> ResetPassword(int userId, string password);

        Task<int> ChangePassword(int userId, string password);
    }
}