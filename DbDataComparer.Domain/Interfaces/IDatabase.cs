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
        Task<ExecutionResult> Execute(string connectionString, 
                                      Command command, 
                                      IEnumerable<ParameterTestValue> testValues);

        Task<Command> Explore(string connectionString, 
                              string databaseObject);
    }
}
