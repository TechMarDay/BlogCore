using DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL
{
    public class SqlRepository
    {
        private const string SP_GET_DEFINED_TABLE_TYPE = "sp_SqlRepository_Get_DefinedTableType";
        private const string SP_GET_PARAMETERS = "sp_SqlRepository_Get_Parameters";

        public async Task<IEnumerable<DefinedTableType>> GetDefinedTableType()
        {
            return await DalHelper.Query<DefinedTableType>(SP_GET_DEFINED_TABLE_TYPE);
        }

        public async Task<IEnumerable<SpParameter>> GetSqlParameters()
        {
            return await DalHelper.Query<SpParameter>(SP_GET_PARAMETERS);
        }
    }
}