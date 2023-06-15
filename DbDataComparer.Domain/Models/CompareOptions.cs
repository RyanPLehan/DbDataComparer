using System;

namespace DbDataComparer.Domain.Models
{
    public class CompareOptions
    {
        public bool ParameterOutput { get; set; } = true;
        public bool ParameterReturn { get; set; } = true;
        public bool ResultSetMetaData { get; set; } = true;
        public bool ResultSetData { get; set; } = true;

        public IDictionary<GranularMetaDataOptions, bool> GranularMetaData { get; set; } =
            Enum.GetValues<GranularMetaDataOptions>().ToDictionary(e => e, e => true);

        public IDictionary<GranularDataOptions, bool> GranularData { get; set; } =
            Enum.GetValues<GranularDataOptions>().ToDictionary(e => e, e => true);
    }

    public enum GranularMetaDataOptions
    {
        DataTypeName,
        DataTypeLength, 
        OrdinalPosition, 
        Nullablity
    }

    public enum GranularDataOptions
    {
        TrailingWhiteSpace
    }

}
