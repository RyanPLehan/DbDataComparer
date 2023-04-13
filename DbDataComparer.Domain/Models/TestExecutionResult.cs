using System;

namespace DbDataComparer.Domain.Models
{
    public class TestExecutionResult
    {
        public string TestName { get; set; }

        public ExecutionResult Source { get; set; }
        public ExecutionResult Target { get; set; }

        public TimeSpan ExecutionTime { get; set; }
    }
}
