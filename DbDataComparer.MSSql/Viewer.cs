using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace DbDataComparer.MSSql
{
    internal class Viewer
    {
        public enum LookupTypeEnum : int
        {
            StoredProcedure,
            Table,
            View
        }

        public async Task<IEnumerable<string>> GetNames(SqlConnection connection, LookupTypeEnum lookupType)
        {
            IList<string> ret = new List<string>();

            var sql = GenerateSql(lookupType);
            var cmd = CreateCommand(connection, sql);
            var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                ret.Add(String.Format("{0}.{1}", reader.GetString(0), reader.GetString(1)));
            }

            await reader.CloseAsync();

            return ret;
        }

        private string GenerateSql(LookupTypeEnum lookupType)
        {
            string sql = null;

            switch (lookupType)
            {
                case LookupTypeEnum.StoredProcedure:
                    sql = GenerateStoredProcedureSql();
                    break;

                case LookupTypeEnum.Table:
                    sql = GenerateTableSql();
                    break;

                case LookupTypeEnum.View:
                    sql = GenerateViewSql();
                    break;
            }

            return sql;
        }

        private string GenerateStoredProcedureSql()
        {
            var select = "SELECT ROUTINE_SCHEMA AS [Schema], ROUTINE_NAME AS [Name]";
            var from = "FROM INFORMATION_SCHEMA.ROUTINES";
            var where = "WHERE ROUTINE_TYPE = 'PROCEDURE'";
            var orderBy = "ORDER BY ROUTINE_SCHEMA ASC, ROUTINE_NAME ASC";

            return $"{select} {from} {where} {orderBy}";
        }

        private string GenerateTableSql()
        {
            var select = "SELECT TABLE_SCHEMA AS [Schema], TABLE_NAME AS [Name]";
            var from = "FROM INFORMATION_SCHEMA.TABLES";
            var where = "WHERE TABLE_TYPE = 'BASE TABLE'";
            var orderBy = "ORDER BY TABLE_SCHEMA ASC, TABLE_NAME ASC";

            return $"{select} {from} {where} {orderBy}";
        }

        private string GenerateViewSql()
        {
            var select = "SELECT TABLE_SCHEMA AS [Schema], TABLE_NAME AS [Name]";
            var from = "FROM INFORMATION_SCHEMA.TABLES";
            var where = "WHERE TABLE_TYPE = 'VIEW'";
            var orderBy = "ORDER BY TABLE_SCHEMA ASC, TABLE_NAME ASC";

            return $"{select} {from} {where} {orderBy}";
        }

        private SqlCommand CreateCommand(SqlConnection connection, string commandText)
        {
            return new SqlCommand()
            {
                Connection = connection,
                CommandText = commandText,
                CommandType = CommandType.Text,
            };
        }
    }
}
