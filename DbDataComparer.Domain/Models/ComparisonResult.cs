using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbDataComparer.Domain.Enums;

namespace DbDataComparer.Domain.Models
{
    public class ComparisonResult
    {
        public TestExecutionResult TestResult { get; set; }        
        public TimeSpan ComparisonTime { get; set; }

        public TestComparisonResult ParameterReturnResult { get; set; }
        public TestComparisonResult ParameterOutputResult { get; set; }
        public IDictionary<int, TestComparisonResult> ResultsetMetaDataResults { get; set; } = new Dictionary<int, TestComparisonResult>();
        public IDictionary<int, TestComparisonResult> ResultsetDataResults { get; set; } = new Dictionary<int, TestComparisonResult>();
    }
}
