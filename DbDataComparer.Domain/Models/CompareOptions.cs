using System;

namespace DbDataComparer.Domain.Models
{
    public class CompareOptions
    {
        public bool ParameterOutput { get; set; } = true;
        public bool ParameterReturn { get; set; } = true;
        public bool ResultSetMetaData { get; set; } = true;
        public bool ResultSetData { get; set; } = true;
        public bool DataTypeName { get; set; } = true;
        public bool DataTypeLength { get; set; } = true;
        public bool OrdinalPosition { get; set; } = true;
        public bool Nullablity { get; set; } = true;
        public bool TrailingWhiteSpace { get; set; } = true;
    }
}
