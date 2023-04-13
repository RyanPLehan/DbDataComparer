using System;
using System.Data;
using System.Data.Common;

namespace DbDataComparer.Domain.Models
{
    public class Parameter
    {
        /// <summary>
        /// Parameter Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Parameter's data type
        /// </summary>
        /// <see cref="https://learn.microsoft.com/en-us/dotnet/api/system.data.sqldbtype?view=net-7.0"/>
        public SqlDbType DataType { get; set; }
        public string DataTypeDescription { get => DataType.ToString(); }

        public int CharacterMaxLength { get; set; }

        public ParameterDirection Direction { get; set; }
        public string DirectionDescription { get => Direction.ToString(); }

        public bool IsNullable { get; set; }

        /// <summary>
        /// Name of User Defined Type
        /// </summary>
        public UserDefinedType UserDefinedType { get; set; }
    }
}
