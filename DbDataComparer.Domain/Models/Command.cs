using System;
using System.Collections.Generic;
using System.Data;

namespace DbDataComparer.Domain.Models
{
    public class Command
    {
        /// <summary>
        /// Command Text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Command Type
        /// </summary>
        /// <see cref="https://learn.microsoft.com/en-us/dotnet/api/system.data.commandtype?view=net-7.0"/>
        public CommandType Type { get; set; }
        public string TypeDescription { get => Type.ToString(); }

        /// <summary>
        /// Timeout
        /// </summary>
        public int TimeoutInSeconds { get; set; } = 30;

        /// <summary>
        /// Parameters if command type is StoredProcedure
        /// </summary>
        public IEnumerable<Parameter> Parameters { get; set; }
    }
}
