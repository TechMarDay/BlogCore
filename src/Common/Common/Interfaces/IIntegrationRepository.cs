using Common.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IIntegrationRepository<T> where T : class
    {
        Task<int> Write(UserSession user, T value, IDbConnection conn = null, IDbTransaction trans = null);

        Task<int> WriteArray(UserSession user, IEnumerable<T> value, IDbConnection conn = null, IDbTransaction trans = null);

        Task<int> Update(UserSession user, T value, IDbConnection conn = null, IDbTransaction trans = null);

        Task<int> Delete(UserSession user, T value, IDbConnection conn = null, IDbTransaction trans = null);
    }
}