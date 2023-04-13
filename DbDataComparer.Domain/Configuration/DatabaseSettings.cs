using System;

namespace DbDataComparer.Domain.Configuration
{
    public class DatabaseSettings
    {
        public string SourceConnection { get; set; }
        public string TargetConnection { get; set; }
    }
}
