using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbDataComparer.Domain.Models
{
    public class ResultSet
    {
        public IEnumerable<ResultSetMetaData> MetaData { get; set; } = Enumerable.Empty<ResultSetMetaData>();

        /// <summary>
        /// Values will be stored in an Arrays of Arrays (ie Jagged array)
        /// </summary>
        /// <remarks>
        /// According to MS, Jaggard arrays (object[][]) are faster and more efficient than Multidimensional arrays (object[,])
        /// </remarks>
        public object[][] Values { get; set; }
    }
}
