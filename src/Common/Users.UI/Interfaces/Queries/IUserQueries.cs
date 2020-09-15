using Common.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Users.UI.ViewModels;

namespace Users.UI.Interfaces.Queries
{
    public interface IUserQueries : IBaseQueries
    {
        Task<IEnumerable<UserAccountViewModel>> Gets(string condition = "");

        Task<UserAccountViewModel> Get(string userName);

        Task<UserAccountViewModel> GetWithEmail(string email);

        Task<UserAccountViewModel> Get(int userId);

        Task<UserAccountViewModel> GetUserWithRole(string userName);

        Task<UserAccountViewModel> GetUserWithRole(int userId);

        Task<IEnumerable<UserAccountViewModel>> GetUsersWithRole(string condition = "");

        Task<IEnumerable<UserAccountViewModel>> GetUsersNotAssignBy(bool isExternalUser, string roleName = "");

        Task<IEnumerable<UserAccountViewModel>> GetsPage(int CurrentPage, int PageSize, string SearchText, int InputViewMode);
    }
}