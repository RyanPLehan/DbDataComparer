using System;

namespace DbDataComparer.Domain
{
    public class TestDefinitionBuilderOptions
    {
        public class DatabaseOptions
        {
            public string ConnectionString { get; set; }
            public string DatabaseObjectName { get; set; }
        }

        public string Name { get; set; }
        public DatabaseOptions Source { get; set; }
        public DatabaseOptions Target { get; set; }
    }
}
