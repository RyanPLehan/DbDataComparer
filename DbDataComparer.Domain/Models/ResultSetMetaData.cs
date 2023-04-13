using System;
using System.Data;
using System.Data.Common;

namespace DbDataComparer.Domain.Models
{
    public class ResultSetMetaData
    {
        public string Name { get; set; }
        public int OrdinalPosition { get; set; }
        public string DataTypeName { get; set; }
        public int Length { get; set; } = 0;
        public bool IsNullable { get; set; } = false;
    }
}
