using Common.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using User.UI.ViewModels;

namespace Users.UI.Interfaces.Queries
{
    public interface IRoleQueries : IBaseQueries
    {
        Task<IEnumerable<RoleViewModel>> Gets(string condition = "");

        Task<RoleViewModel> Get(int id);

        Task<RoleViewModel> GetByName(string name);

        Task<IEnumerable<RoleViewModel>> List(int InputCurrentPage, int InputPageSize, string SearchText, int InputViewMode);
    }
}