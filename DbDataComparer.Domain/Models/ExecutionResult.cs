using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace DbDataComparer.Domain.Models
{
    public class ExecutionResult
    {
        public Command Command { get; init; }
        
        public TimeSpan ExecutionTime { get; set; }

        public Exception Exception { get; set; }

        public int ReturnResult { get; set; } = 0;
        public IDictionary<string, object?> OutputParameterResults { get; set; } = new Dictionary<string, object?>();
        public IDictionary<int, ResultSet> ResultSets { get; set; } = new Dictionary<int, ResultSet>();

        public ExecutionResult(Command command)
        {
            this.Command = command;
        }
    }
}
