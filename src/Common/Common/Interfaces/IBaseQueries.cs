using Common.Models;
using System.Data;

namespace Common.Interfaces
{
    public interface IBaseQueries
    {
        UserSession LoginSession { set; get; }

        void JoinTransaction(IDbConnection conn, IDbTransaction trans);
    }
}