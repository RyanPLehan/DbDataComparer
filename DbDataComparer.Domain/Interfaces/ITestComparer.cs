using DbDataComparer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;


namespace DbDataComparer.Domain
{
    public interface ITestComparer
    {
        TestComparisonResult CompareExecution(ExecutionResult source, ExecutionResult target);

        TestComparisonResult CompareParameterOutput(ExecutionResult source, ExecutionResult target);
        TestComparisonResult CompareParameterReturn(ExecutionResult source, ExecutionResult target);

        TestComparisonResult CompareResultSetMetaData(ResultSet source, ResultSet target);
        TestComparisonResult CompareResultSetData(ResultSet source, ResultSet target);

        IDictionary<int, TestComparisonResult> CompareResultSetMetaData(ExecutionResult source, ExecutionResult target);
        IDictionary<int, TestComparisonResult> CompareResultSetData(ExecutionResult source, ExecutionResult target);
    }
}
