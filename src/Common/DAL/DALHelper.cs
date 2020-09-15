extern alias MySqlData;

using Common.Attributes;
using Common.Exceptions;

using DAL.Models;
using Dapper;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DAL
{
    public static class DalHelper
    {
        public static string ConnectionString => null;

        #region Get Connection

        public static IDbConnection GetConnection(string connectionString = "", SqlType type = SqlType.MySql)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = ConnectionString;
            }

            switch (type)
            {
                case SqlType.SQLServer:
                    return new SqlConnection(connectionString);

                case SqlType.MySql:
                    return new MySqlData.MySql.Data.MySqlClient.MySqlConnection(connectionString);

                default:
                    throw new ArgumentWrongException("Wrong type");
            }
        }

        #endregion Get Connection

        #region Relationship Sql

        #region Excute text

        public static async Task<IEnumerable<T>> Query<T>(string sql, DynamicParameters param = null, IDbTransaction dbTransaction = null, IDbConnection connection = null, SqlType type = SqlType.MySql)
        {
            if (connection != null)
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                param = param ?? new DynamicParameters();
                return await connection.QueryAsync<T>(sql, param, dbTransaction);
            }
            else
            {
                switch (type)
                {
                    case SqlType.SQLServer:
                        using (IDbConnection conn = new SqlConnection(ConnectionString))
                        {
                            conn.Open();
                            param = param ?? new DynamicParameters();
                            return await conn.QueryAsync<T>(sql, param);
                        }
                    case SqlType.MySql:
                        using (IDbConnection conn = new MySqlData.MySql.Data.MySqlClient.MySqlConnection(ConnectionString))
                        {
                            conn.Open();
                            param = param ?? new DynamicParameters();
                            return await conn.QueryAsync<T>(sql, param);
                        }
                    default:
                        throw new ArgumentWrongException("Wrong type");
                }
            }
        }

        public static async Task<IEnumerable<T>> ExecuteQuery<T>(string sql, DynamicParameters param = null, IDbTransaction dbTransaction = null, IDbConnection connection = null, SqlType type = SqlType.MySql)
        {
            if (connection != null)
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                param = param ?? new DynamicParameters();
                return await connection.QueryAsync<T>(sql, param, commandType: CommandType.Text, transaction: dbTransaction);
            }
            else
            {
                switch (type)
                {
                    case SqlType.SQLServer:
                        using (IDbConnection conn = new SqlConnection(ConnectionString))
                        {
                            conn.Open();
                            param = param ?? new DynamicParameters();
                            return await conn.QueryAsync<T>(sql, param, commandType: CommandType.Text);
                        }
                    case SqlType.MySql:
                        using (IDbConnection conn = new MySqlData.MySql.Data.MySqlClient.MySqlConnection(ConnectionString))
                        {
                            conn.Open();
                            param = param ?? new DynamicParameters();
                            return await conn.QueryAsync<T>(sql, param, commandType: CommandType.Text);
                        }
                    default:
                        throw new ArgumentWrongException("Wrong type");
                }
            }
        }

        public static async Task<T> ExecuteScadar<T>(string sql, DynamicParameters param = null, IDbTransaction dbTransaction = null, IDbConnection connection = null, SqlType type = SqlType.MySql)
        {
            if (connection != null)
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                param = param ?? new DynamicParameters();
                return await connection.ExecuteScalarAsync<T>(sql, param, commandType: CommandType.Text, transaction: dbTransaction);
            }
            else
            {
                switch (type)
                {
                    case SqlType.SQLServer:
                        using (IDbConnection conn = new SqlConnection(ConnectionString))
                        {
                            conn.Open();
                            param = param ?? new DynamicParameters();
                            return await conn.ExecuteScalarAsync<T>(sql, param, commandType: CommandType.Text);
                        }
                    case SqlType.MySql:
                        using (IDbConnection conn = new MySqlData.MySql.Data.MySqlClient.MySqlConnection(ConnectionString))
                        {
                            conn.Open();
                            param = param ?? new DynamicParameters();
                            return await conn.ExecuteScalarAsync<T>(sql, param, commandType: CommandType.Text);
                        }
                    default:
                        throw new ArgumentWrongException("Wrong type");
                }
            }
        }

        public static async Task<int> Execute(string sql, DynamicParameters param = null, IDbTransaction dbTransaction = null, IDbConnection connection = null, SqlType type = SqlType.MySql)
        {
            if (connection != null)
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                param = param ?? new DynamicParameters();
                return await connection.ExecuteAsync(sql, param, commandType: CommandType.Text, transaction: dbTransaction);
            }
            else
            {
                switch (type)
                {
                    case SqlType.SQLServer:
                        using (IDbConnection conn = new SqlConnection(ConnectionString))
                        {
                            conn.Open();
                            param = param ?? new DynamicParameters();
                            return await conn.ExecuteAsync(sql, param, commandType: CommandType.Text);
                        }
                    case SqlType.MySql:
                        using (IDbConnection conn = new MySqlData.MySql.Data.MySqlClient.MySqlConnection(ConnectionString))
                        {
                            conn.Open();
                            param = param ?? new DynamicParameters();
                            return await conn.ExecuteAsync(sql, param, commandType: CommandType.Text);
                        }
                    default:
                        throw new ArgumentWrongException("Wrong type");
                }
            }
        }

        public static async Task<IDictionary<string, object>> ReturnExecute(string sql, string[] outParamsName, DynamicParameters param = null, IDbTransaction dbTransaction = null, IDbConnection connection = null, SqlType type = SqlType.MySql)
        {
            if (connection != null)
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                param = param ?? new DynamicParameters();
                await connection.ExecuteAsync(sql, param, commandType: CommandType.Text, transaction: dbTransaction);

                Dictionary<string, object> result = new Dictionary<string, object>();
                foreach (var item in outParamsName)
                {
                    result.Add(item, param.Get<object>(item));
                }

                return result;
            }
            else
            {
                switch (type)
                {
                    case SqlType.SQLServer:
                        using (IDbConnection conn = new SqlConnection(ConnectionString))
                        {
                            conn.Open();
                            param = param ?? new DynamicParameters();
                            await conn.ExecuteAsync(sql, param, commandType: CommandType.Text);

                            Dictionary<string, object> result = new Dictionary<string, object>();
                            foreach (var item in outParamsName)
                            {
                                result.Add(item, param.Get<object>(item));
                            }

                            return result;
                        }
                    case SqlType.MySql:
                        using (IDbConnection conn = new MySqlData.MySql.Data.MySqlClient.MySqlConnection(ConnectionString))
                        {
                            conn.Open();
                            param = param ?? new DynamicParameters();
                            await conn.ExecuteAsync(sql, param, commandType: CommandType.Text);

                            Dictionary<string, object> result = new Dictionary<string, object>();
                            foreach (var item in outParamsName)
                            {
                                result.Add(item, param.Get<object>(item));
                            }

                            return result;
                        }
                    default:
                        throw new ArgumentWrongException("Wrong type");
                }
            }
        }

        #endregion Excute text

        #region Excute SP

        public static async Task<IEnumerable<T>> SPExecuteQuery<T>(string sql, DynamicParameters param = null, IDbTransaction dbTransaction = null, IDbConnection connection = null, SqlType type = SqlType.MySql)
        {
            if (connection != null)
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                param = param ?? new DynamicParameters();
                return await connection.QueryAsync<T>(sql, param, commandType: CommandType.StoredProcedure, transaction: dbTransaction);
            }
            else
            {
                switch (type)
                {
                    case SqlType.SQLServer:
                        using (IDbConnection conn = new SqlConnection(ConnectionString))
                        {
                            conn.Open();
                            param = param ?? new DynamicParameters();
                            return await conn.QueryAsync<T>(sql, param, commandType: CommandType.StoredProcedure);
                        }
                    case SqlType.MySql:
                        using (IDbConnection conn = new MySqlData.MySql.Data.MySqlClient.MySqlConnection(ConnectionString))
                        {
                            conn.Open();
                            param = param ?? new DynamicParameters();
                            return await conn.QueryAsync<T>(sql, param, commandType: CommandType.StoredProcedure);
                        }
                    default:
                        throw new ArgumentWrongException("Wrong type");
                }
            }
        }

        public static async Task<T> SPExecuteScadar<T>(string sql, DynamicParameters param = null, IDbTransaction dbTransaction = null, IDbConnection connection = null, SqlType type = SqlType.MySql)
        {
            if (connection != null)
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                param = param ?? new DynamicParameters();
                return await connection.ExecuteScalarAsync<T>(sql, param, commandType: CommandType.StoredProcedure, transaction: dbTransaction);
            }
            else
            {
                switch (type)
                {
                    case SqlType.SQLServer:
                        using (IDbConnection conn = new SqlConnection(ConnectionString))
                        {
                            conn.Open();
                            param = param ?? new DynamicParameters();
                            return await conn.ExecuteScalarAsync<T>(sql, param, commandType: CommandType.StoredProcedure);
                        }
                    case SqlType.MySql:
                        using (IDbConnection conn = new MySqlData.MySql.Data.MySqlClient.MySqlConnection(ConnectionString))
                        {
                            conn.Open();
                            param = param ?? new DynamicParameters();
                            return await conn.ExecuteScalarAsync<T>(sql, param, commandType: CommandType.StoredProcedure);
                        }
                    default:
                        throw new ArgumentWrongException("Wrong type");
                }
            }
        }

        public static async Task<int> SPExecute(string sql, DynamicParameters param = null, IDbTransaction dbTransaction = null, IDbConnection connection = null, SqlType type = SqlType.MySql)
        {
            if (connection != null)
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                param = param ?? new DynamicParameters();
                return await connection.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure, transaction: dbTransaction);
            }
            else
            {
                switch (type)
                {
                    case SqlType.SQLServer:
                        using (IDbConnection conn = new SqlConnection(ConnectionString))
                        {
                            conn.Open();
                            param = param ?? new DynamicParameters();
                            return await conn.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure);
                        }
                    case SqlType.MySql:
                        using (IDbConnection conn = new MySqlData.MySql.Data.MySqlClient.MySqlConnection(ConnectionString))
                        {
                            conn.Open();
                            param = param ?? new DynamicParameters();
                            return await conn.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure);
                        }
                    default:
                        throw new ArgumentWrongException("Wrong type");
                }
            }
        }

        public static async Task<IDictionary<string, object>> SPReturnExecute(string sql, string[] outParamsName, DynamicParameters param = null, IDbTransaction dbTransaction = null, IDbConnection connection = null, SqlType type = SqlType.MySql)
        {
            if (connection != null)
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                param = param ?? new DynamicParameters();
                await connection.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure, transaction: dbTransaction);

                Dictionary<string, object> result = new Dictionary<string, object>();
                foreach (var item in outParamsName)
                {
                    result.Add(item, param.Get<object>(item));
                }

                return result;
            }
            else
            {
                switch (type)
                {
                    case SqlType.SQLServer:
                        using (IDbConnection conn = new SqlConnection(ConnectionString))
                        {
                            conn.Open();
                            param = param ?? new DynamicParameters();
                            await conn.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure);

                            Dictionary<string, object> result = new Dictionary<string, object>();
                            foreach (var item in outParamsName)
                            {
                                result.Add(item, param.Get<object>(item));
                            }

                            return result;
                        }
                    case SqlType.MySql:
                        using (IDbConnection conn = new MySqlData.MySql.Data.MySqlClient.MySqlConnection(ConnectionString))
                        {
                            conn.Open();
                            param = param ?? new DynamicParameters();
                            await conn.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure);

                            Dictionary<string, object> result = new Dictionary<string, object>();
                            foreach (var item in outParamsName)
                            {
                                result.Add(item, param.Get<object>(item));
                            }

                            return result;
                        }
                    default:
                        throw new ArgumentWrongException("Wrong type");
                }
            }
        }

        #endregion Excute SP

        #endregion Relationship Sql

        #region Helper

        public static DynamicParameters ParamsWrapperWithSpInfor<T>(string spName, T obj, out string[] outputList)
        {
            var result = new DynamicParameters();
            if (obj == null)
            {
                outputList = new string[0];
                return result;
            }
            List<string> output = new List<string>();
            SqlRepository sqlRepository = new SqlRepository();
            IEnumerable<SpParameter> spParameterList = new List<SpParameter>();
            IEnumerable<DefinedTableType> definedTableTypeList = new List<DefinedTableType>();
            spParameterList = sqlRepository.GetSqlParameters().Result.Where(x => x.ParamName == spName);
            if (spParameterList == null)
            {
                outputList = new string[0];
                return result;
            }
            else
            {
                foreach (var param in spParameterList)
                {
                    string properityName = param.ParamName.Replace("@", "");
                    PropertyInfo propertyInfo = typeof(T).GetProperty(properityName);
                    if (propertyInfo != null && !propertyInfo.CustomAttributes.Any(y => y.AttributeType == typeof(NotMappingAttribute)))
                    {
                        if (param.IsTableType)
                        {
                            Type tableType = null;
                            MethodInfo methodInfo = typeof(string).GetMethod("ToDataTableWithTableColumnInfor");
                            var assemblyNames = AppDomain.CurrentDomain.GetAssemblies();
                            foreach (var item in assemblyNames)
                            {
                                tableType = item.GetTypes().Where(t => t.FullName.EndsWith(param.TypeName)).FirstOrDefault();
                                if (tableType != null)
                                {
                                    break;
                                }
                            }
                            if (tableType != null && methodInfo != null)
                            {
                                definedTableTypeList = sqlRepository.GetDefinedTableType().Result.Where(z => z.TableName.Equals(param.TypeName));
                                methodInfo = methodInfo.MakeGenericMethod(tableType);
                                var objTable = propertyInfo.GetValue(obj);
                                result.Add(param.ParamName,
                                       methodInfo.Invoke(null, new object[] { objTable, definedTableTypeList.Select(z1 => z1.ColumnName).ToList() }),
                                       DbType.Object,
                                       param.IsOutput ? ParameterDirection.Output : ParameterDirection.Input,
                                       param.ParamLength > 0 ? (int?)param.ParamLength : null);
                            }
                        }
                        else
                        {
                            result.Add(param.ParamName,
                                       propertyInfo.GetValue(obj),
                                       GetDBTypeByName(param.TypeName),
                                       param.IsOutput ? ParameterDirection.Output : ParameterDirection.Input,
                                       param.ParamLength > 0 ? (int?)param.ParamLength : null);
                        }
                    }
                }
                outputList = output.ToArray();
                return result;
            }
        }

        public static DbType GetDBTypeByName(string name)
        {
            if (name.Contains("char"))
            {
                return DbType.String;
            }
            else if (name.Equals("int"))
            {
                return DbType.Int32;
            }
            else if (name.Equals("bigint"))
            {
                return DbType.Int64;
            }
            else if (name.Equals("smallint"))
            {
                return DbType.Int16;
            }
            else if (name.Equals("uniqueidentifier"))
            {
                return DbType.Guid;
            }
            else if (name.Equals("date"))
            {
                return DbType.Date;
            }
            else if (name.Equals("datetime"))
            {
                return DbType.DateTime;
            }
            else if (name.Equals("datetime2"))
            {
                return DbType.DateTime2;
            }
            else if (name.Equals("bit"))
            {
                return DbType.Boolean;
            }
            else if (name.Equals("float"))
            {
                return DbType.Double;
            }
            else if (name.Equals("decimal"))
            {
                return DbType.Decimal;
            }
            else
            {
                return DbType.Object;
            }
        }

        #endregion Helper
    }
}