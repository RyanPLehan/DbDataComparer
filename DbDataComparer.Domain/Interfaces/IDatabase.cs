using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using DbDataComparer.Domain.Models;

namespace DbDataComparer.Domain
{
    public interface IDatabase
    {
        Task<object[][]> Execute(string connectionString,
                                 string sql);

        Task<ExecutionResult> Execute(string connectionString,
                                      ExecutionDefinition executionDefinition,
                                      string sql);

        Task<ExecutionResult> Execute(string connectionString, 
                                      ExecutionDefinition executionDefinition, 
                                      IEnumerable<ParameterTestValue> testValues);

        Task<ExecutionDefinition> Explore(string connectionString, 
                                          string databaseObject);
        Task<T> ExecuteScalar<T>(string connection,
                                 string sql);
        Task ExecuteNonQuery(string connectionString,
                             string sql);

        Task<IEnumerable<string>> GetStoredProcedureNames(string connectionString);

        Task<IEnumerable<string>> GetTableNames(string connectionString);

        Task<IEnumerable<string>> GetViewNames(string connectionString);
    }
}
