using System;

namespace DbDataComparer.Domain.Models
{
    public class ParameterTestValue
    {
        public string ParameterName { get; set; }

        /// <summary>
        /// Test value for standard parameters
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// List of table type test values
        /// </summary>
        /// <remarks>
        /// The dictionary represents a record with each dictionary entry is a key/value pair that represents a column/value entry
        /// </remarks>
        public IEnumerable<IDictionary<string, object>> Values { get; set; }
    }
}
