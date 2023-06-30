using System;

namespace DbDataComparer.Logger.Models
{
    internal class TestDefinitionEntity
    {
        public int Id { get; set; }
        public string SourceServer { get; set; }
        public string SourceDatabase { get; set; }
        public string SourceSchema { get; set; }
        public string SourceObject { get; set; }
        public string TargetServer { get; set; }
        public string TargetDatabase { get; set; }
        public string TargetSchema { get; set; }
        public string TargetObject { get; set; }
        public bool IsTable { get; set; } = false;
    }
}
