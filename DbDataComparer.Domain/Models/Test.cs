using System;

namespace DbDataComparer.Domain.Models
{
    public class Test
    {
        /// <summary>
        /// Test name
        /// </summary>
        public string Name { get; set; }

        public override string ToString() => this.Name;
    }
}
