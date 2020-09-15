using Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Interfaces
{
    public interface ICheckPermissionQueries
    {
        Task<bool> CheckPermission(List<int> roleIds, string featureName, Permissions permission);
    }
}