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
        public TestComparisonResult CompareParameterOutput(ExecutionResult source, ExecutionResult target);
        public TestComparisonResult CompareParameterReturn(ExecutionResult source, ExecutionResult target);

        public TestComparisonResult CompareResultSetMetaData(ResultSet source, ResultSet target);
        public TestComparisonResult CompareResultSetData(ResultSet source, ResultSet target);

        public IDictionary<int, TestComparisonResult> CompareResultSetMetaData(ExecutionResult source, ExecutionResult target);
        public IDictionary<int, TestComparisonResult> CompareResultSetData(ExecutionResult source, ExecutionResult target);
    }
}
