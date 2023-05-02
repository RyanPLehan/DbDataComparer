using System;

namespace DbDataComparer.Domain.Models
{
    public class StoredProcedureTest
    {
        /// <summary>
        /// Test name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of Test values for Source command
        /// </summary>
        public IEnumerable<ParameterTestValue> SourceTestValues { get; set; }

        /// <summary>
        /// List of Test values for Target command
        /// </summary>
        public IEnumerable<ParameterTestValue> TargetTestValues { get; set; }

        public override string ToString() => this.Name;
    }
}
