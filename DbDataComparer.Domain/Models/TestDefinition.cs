using System;
using System.Collections.Generic;

namespace DbDataComparer.Domain.Models
{
    public class TestDefinition
    {
        /// <summary>
        /// Name of Test Definition
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Options to turn on/off comparison routines
        /// </summary>
        public CompareOptions CompareOptions { get; set; } = new CompareOptions();

        /// <summary>
        /// Options to notify if there is a failure
        /// </summary>
        public NotificationOptions NotificationOptions { get; set; } = new NotificationOptions();

        /// <summary>
        /// Source command defintion
        /// </summary>
        public ExecutionDefinition Source { get; set; }

        /// <summary>
        /// Target command defintion
        /// </summary>
        public ExecutionDefinition Target { get; set; }

        /// <summary>
        /// One or more tests for comparison
        /// </summary>
        public IEnumerable<StoredProcedureTest> StoredProcedureTests { get; set; }

        /// <summary>
        /// One or more tests for comparison
        /// </summary>
        public IEnumerable<TableViewTest> TableViewTests { get; set; }
    }
}
