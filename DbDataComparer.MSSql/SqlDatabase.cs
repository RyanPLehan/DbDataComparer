using System.Data;
using System.Data.Common;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using DbDataComparer.Domain;
using DbDataComparer.Domain.Models;
using System.Linq.Expressions;

namespace DbDataComparer.MSSql
{
    public class SqlDatabase : IDatabase
    {
        public async Task<object[][]> Execute(string connectionString,                                               
                                              string sql)
        {
            object[][] result = null;
            Executioner commandExecutioner = new Executioner();

            using (SqlConnection sqlConn = CreateConnection(connectionString))
            {
                await sqlConn.OpenAsync();
                result = await commandExecutioner.Execute(sqlConn, sql);
            }

            return result;
        }


        public async Task<ExecutionResult> Execute(string connectionString,
                                                   ExecutionDefinition command,
                                                   string sql)
        {
            Stopwatch sw = new Stopwatch();
            ExecutionResult result = null;
            Executioner commandExecutioner = new Executioner();

            sw.Start();
            using (SqlConnection sqlConn = CreateConnection(connectionString))
            {
                try
                {
                    await sqlConn.OpenAsync();
                    result = await commandExecutioner.Execute(sqlConn, command, sql);
                }
                catch (Exception ex)
                {
                    result = new ExecutionResult(command) { Exception = ex };
                }
            }
            sw.Stop();
            result.ExecutionTime = sw.Elapsed;

            return result;
        }


        public async Task<ExecutionResult> Execute(string connectionString, 
                                                   ExecutionDefinition command, 
                                                   IEnumerable<ParameterTestValue> testValues)
        {
            Stopwatch sw = new Stopwatch();
            ExecutionResult result = null;
            Executioner commandExecutioner = new Executioner();

            sw.Start();
            using (SqlConnection sqlConn = CreateConnection(connectionString))
            {
                try
                {
                    await sqlConn.OpenAsync();
                    result = await commandExecutioner.Execute(sqlConn, command, testValues);
                }
                catch (Exception ex)
                {
                    result = new ExecutionResult(command) { Exception = ex };
                }
            }
            sw.Stop();
            result.ExecutionTime = sw.Elapsed;

            return result;
        }

        public async Task<T> ExecuteScalar<T>(string connectionString,
                                              string sql)
        {
            T result = default(T);
            Executioner commandExecutioner = new Executioner();

            using (SqlConnection sqlConn = CreateConnection(connectionString))
            {
                await sqlConn.OpenAsync();
                result = await commandExecutioner.ExecuteScalar<T>(sqlConn, sql);
            }

            return result;
        }


        public async Task ExecuteNonQuery(string connectionString,
                                          string sql)
        {
            Executioner commandExecutioner = new Executioner();

            using (SqlConnection sqlConn = CreateConnection(connectionString))
            {
                await sqlConn.OpenAsync();
                await commandExecutioner.ExecuteNonQuery(sqlConn, sql);
            }
        }


        public async Task<ExecutionDefinition> Explore(string connectionString, 
                                                       string databaseObject)
        {
            ExecutionDefinition exeDefinition = null;            
            Explorer explorer = new Explorer();

            using (SqlConnection sqlConn = CreateConnection(connectionString))
            {
                await sqlConn.OpenAsync();
                exeDefinition = await explorer.Explore(sqlConn, databaseObject);
            }

            return exeDefinition;
        }


        public async Task<IEnumerable<string>> GetStoredProcedureNames(string connectionString)
        {
            IEnumerable<string> ret = Enumerable.Empty<string>();
            Viewer viewer = new Viewer();

            using (SqlConnection sqlConn = CreateConnection(connectionString))
            {
                await sqlConn.OpenAsync();
                ret = await viewer.GetNames(sqlConn, Viewer.LookupTypeEnum.StoredProcedure);
            }

            return ret;
        }


        public async Task<IEnumerable<string>> GetTableNames(string connectionString)
        {
            IEnumerable<string> ret = Enumerable.Empty<string>();
            Viewer viewer = new Viewer();

            using (SqlConnection sqlConn = CreateConnection(connectionString))
            {
                await sqlConn.OpenAsync();
                ret = await viewer.GetNames(sqlConn, Viewer.LookupTypeEnum.Table);
            }

            return ret;
        }


        public async Task<IEnumerable<string>> GetViewNames(string connectionString)
        {
            IEnumerable<string> ret = Enumerable.Empty<string>();
            Viewer viewer = new Viewer();

            using (SqlConnection sqlConn = CreateConnection(connectionString))
            {
                await sqlConn.OpenAsync();
                ret = await viewer.GetNames(sqlConn, Viewer.LookupTypeEnum.View);
            }

            return ret;
        }

        private SqlConnection CreateConnection(string connectionString) => new SqlConnection(connectionString);
    }
}