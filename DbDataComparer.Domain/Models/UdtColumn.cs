using System;
using System.Data;
using System.Data.Common;

namespace DbDataComparer.Domain.Models
{
    public class UdtColumn
    {
        public string Name { get; set; }
        public SqlDbType DataType { get; set; }
        public string DataTypeDescription { get => DataType.ToString(); }
    }
}
