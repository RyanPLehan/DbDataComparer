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
        public async Task<ExecutionResult> Execute(string connectionString, 
                                                   Command command, 
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


        public async Task<Command> Explore(string connectionString, 
                                           string databaseObject)
        {
            Command command = null;            
            Explorer explorer = new Explorer();

            using (SqlConnection sqlConn = CreateConnection(connectionString))
            {
                await sqlConn.OpenAsync();
                command = await explorer.Explore(sqlConn, databaseObject);
            }

            return command;
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