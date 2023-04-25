using DbDataComparer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbDataComparer.UI.Models
{
    public class TestDefinitionLoadRequestedEventArgs : EventArgs
    {
        // Response Values
        public TestDefinition TestDefinition { get; set; }
        public string PathName { get; set; }
        public bool SuccessfullyLoaded { get; set; } = false;
    }
}
