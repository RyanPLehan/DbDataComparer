using DbDataComparer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbDataComparer.UI.Models
{
    public class TestDefinitionQueryRequestedEventArgs : EventArgs
    {
        // Response Values
        public string PathName { get; set; }
        public TestDefinition TestDefinition { get; set; }
    }
}
