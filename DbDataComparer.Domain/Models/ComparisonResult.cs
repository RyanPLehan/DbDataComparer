using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DbDataComparer.Domain.Enums;

namespace DbDataComparer.Domain.Models
{
    public class ComparisonResult
    {
        public TestExecutionResult TestResult { get; init; }        
        public TimeSpan ComparisonTime { get; set; }

        public TestComparisonResult ParameterReturnResult { get; set; }
        public TestComparisonResult ParameterOutputResult { get; set; }

        public IDictionary<int, TestComparisonResult> ResultsetMetaDataResults { get; set; }
        public IDictionary<int, TestComparisonResult> ResultsetDataResults { get; set; }


        public ComparisonResult(TestExecutionResult testResult)
        {
            this.TestResult = testResult ??
                throw new ArgumentNullException(nameof(testResult));

            this.Initialize();
        }

        private void Initialize()
        {
            this.ParameterReturnResult = new TestComparisonResult();
            this.ParameterOutputResult = new TestComparisonResult();

            this.ResultsetMetaDataResults = new Dictionary<int, TestComparisonResult>();
            this.ResultsetDataResults = new Dictionary<int, TestComparisonResult>();

            foreach (int key in this.TestResult.Source.ResultSets.Keys)
            {
                this.ResultsetMetaDataResults.Add(key, new TestComparisonResult());
                this.ResultsetDataResults.Add(key, new TestComparisonResult());
            }
        }
    }
}
