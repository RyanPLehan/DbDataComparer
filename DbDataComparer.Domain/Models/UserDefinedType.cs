using System;
using System.Collections.Generic;


namespace DbDataComparer.Domain.Models
{
    public class UserDefinedType
    {
        public string Name { get; set; }
        public string Schema { get; set; }
        public IEnumerable<UdtColumn> Columns { get; set; }
    }
}
