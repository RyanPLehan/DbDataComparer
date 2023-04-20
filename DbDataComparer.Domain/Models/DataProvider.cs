using System;

namespace DbDataComparer.Domain.Models
{
    public class DataProvider
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string ShortDisplayName { get; set; }
        public string Description { get; set; }
        public Type ConnectionType { get; set; }
    }
}
