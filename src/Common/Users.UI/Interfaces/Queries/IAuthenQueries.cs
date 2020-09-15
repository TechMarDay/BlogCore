using Common.Interfaces;
using Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Users.UI.Interfaces.Queries
{
    public interface IAuthenQueries : IBaseQueries
    {
        Task<IEnumerable<UserSession>> Gets();

        Task<UserSession> Get(string accessToken);
    }
}