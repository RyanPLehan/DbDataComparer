using System;

namespace DbDataComparer.Domain.Models
{
    public class CompareOptions
    {
        public bool ParameterOutput { get; set; } = true;
        public bool ParameterReturn { get; set; } = true;
        public bool ResultSetMetaData { get; set; } = true;
        public bool ResultSetData { get; set; } = true;
    }
}
