using DbDataComparer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbDataComparer.UI.Models
{
    public class TestDefinitionSaveRequestedEventArgs : EventArgs
    {
        // Request values
        public TestDefinition TestDefinition { get; set; }
        public bool ForceOverwrite { get; set; } = false;

        // Response values
        public string PathName { get; set; }
        public bool SuccessfullySaved { get; set; } = false;
    }
}
