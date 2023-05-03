using System;

namespace DbDataComparer.Domain.Models
{
    public class StoredProcedureTest : Test
    {
        /// <summary>
        /// List of Test values for Source command
        /// </summary>
        public IEnumerable<ParameterTestValue> SourceTestValues { get; set; }

        /// <summary>
        /// List of Test values for Target command
        /// </summary>
        public IEnumerable<ParameterTestValue> TargetTestValues { get; set; }
    }
}
