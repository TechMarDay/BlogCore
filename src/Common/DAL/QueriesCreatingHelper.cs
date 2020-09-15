using Common.Attributes;
using Common.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DAL
{
    public static class QueriesCreatingHelper
    {
        public static string CreateQueryInsert<T>(T obj)
        {
            var tableName = typeof(T).Name;

            var props = typeof(T).GetProperties().ToList();
            string columnNames = "";
            string values = "";
            for (int i = 0; i < props.Count; i++)
            {
                if (props[i].CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(IdentityAttribute)) != null)
                {
                    continue;
                }

                var columnName = props[i].Name;

                columnNames = $"{columnNames}[{columnName}]";
                values = $"{values}{GetSQLValue(props[i].GetValue(obj))}";
                if (i != props.Count - 1)
                {
                    columnNames = $"{columnNames},";
                    values = $"{values},";
                }
            }
            string sql = $"INSERT INTO [{tableName}]({columnNames}) VALUES ({values})";
            sql = $"{sql};SELECT SCOPE_IDENTITY();";

            return sql;
        }

        public static string CreateQueryDelete<T>(string conditions)
        {
            var tableName = typeof(T).Name;
            if (typeof(T).BaseType == typeof(BaseModel))
            {
                string sql = $"UPDATE FROM [{tableName}] SET IsDeleted = 1 WHERE 1=1";
                if (!string.IsNullOrEmpty(conditions))
                {
                    sql = $"{sql} AND {conditions}";
                }

                return sql;
            }
            else
            {
                string sql = $"DELETE FROM [{tableName}] WHERE 1=1";
                if (!string.IsNullOrEmpty(conditions))
                {
                    sql = $"{sql} AND {conditions}";
                }

                return sql;
            }
        }

        public static string CreateQuerySelect<T>(string condition = "")
        {
            var tableName = typeof(T).Name;

            if (string.IsNullOrWhiteSpace(condition))
            {
                condition = "WHERE 1=1";
            }
            else
            {
                condition = $"WHERE {condition}";
            }

            if (typeof(T).BaseType != null && typeof(T).BaseType == typeof(BaseModel))
            {
                condition = $"{condition} AND IsDeleted = 0";
            }

            var props = typeof(T).GetProperties().ToList();
            string columnNames = "";
            for (int i = 0; i < props.Count; i++)
            {
                var columnName = props[i].Name;

                columnNames = $"{columnNames}[{columnName}]";
                if (i != props.Count - 1)
                {
                    columnNames = $"{columnNames},";
                }
            }
            string sql = $"SELECT {columnNames} FROM [{tableName}] {condition}";
            return sql;
        }

        public static string CreateQueryUpdate<T>(T obj)
        {
            var tableName = typeof(T).Name;

            var props = typeof(T).GetProperties().ToList();

            string columnValues = "";
            string condition = "WHERE 1=1";
            for (int i = 0; i < props.Count; i++)
            {
                var columnName = props[i].Name;

                // build column values string
                if (!props[i].CustomAttributes.Any(a => a.AttributeType == typeof(KeyAttribute)))
                {
                    columnValues = $"{columnValues}[{columnName}]= {GetSQLValue(props[i].GetValue(obj))}";
                    if (i != props.Count - 1)
                    {
                        columnValues = $"{columnValues},";
                    }
                }

                // build condition string
                else
                {
                    condition = $"{condition} AND [{columnName}]= {GetSQLValue(props[i].GetValue(obj))}";
                }
            }

            string sql = $"UPDATE [{tableName}] SET {columnValues} {condition}";
            return sql;
        }

        public static string CreateQueryUpdateWithCondition<T>(T obj, string condition)
        {
            var tableName = typeof(T).Name;

            var props = typeof(T).GetProperties().ToList();

            string columnValues = "";

            if (string.IsNullOrWhiteSpace(condition))
            {
                condition = "WHERE 1=1";
            }
            else
            {
                condition = $"WHERE {condition}";
            }

            // build column values string
            for (int i = 0; i < props.Count; i++)
            {
                var columnName = props[i].Name;

                columnValues = $"{columnValues}[{columnName}]= {GetSQLValue(props[i].GetValue(obj))}";
                if (i != props.Count - 1)
                {
                    columnValues = $"{columnValues},";
                }
            }

            return $"UPDATE [{tableName}] SET {columnValues} {condition}";
        }

        public static string GetSQLValue(this object valueObj)
        {
            if (valueObj == null)
            {
                return "NULL";
            }
            if (valueObj is bool)
            {
                return (bool)valueObj ? "1" : "0";
            }
            else if (valueObj is DateTime)
            {
                return $"'{((DateTime)valueObj).ToString("yyyy-MM-dd HH:mm:ss")}'";
            }
            else
            {
                return $"N'{valueObj.ToString().Replace("'", "''")}'";
            }
        }
    }
}