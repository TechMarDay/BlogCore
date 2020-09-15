using System.Data;

namespace Common.Interfaces
{
    public interface IBaseRepository
    {
        void JoinTransaction(IDbConnection conn, IDbTransaction trans);
    }
}