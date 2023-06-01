using DbDataComparer.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data;

namespace DbDataComparer.Domain.Models
{
    public class ExecutionDefinition
    {
        /// <summary>
        /// Connection String to Database
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Command Text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Command Type
        /// </summary>
        public DatabaseObjectTypeEnum Type { get; set; }
        public string TypeDescription { get => Type.ToString(); }

        /// <summary>
        /// Timeout
        /// </summary>
        public int ExecutionTimeoutInSeconds { get; set; } = 30;

        /// <summary>
        /// Parameters if command type is StoredProcedure
        /// </summary>
        public IEnumerable<Parameter> Parameters { get; set; }
    }
}
