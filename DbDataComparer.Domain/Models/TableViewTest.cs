using System;

namespace DbDataComparer.Domain.Models
{
    public class TableViewTest : Test
    {
        /// <summary>
        /// List of Test values for Source command
        /// </summary>
        public string SourceSql { get; set; }

        /// <summary>
        /// List of Test values for Target command
        /// </summary>
        public string TargetSql { get; set; }
    }
}
