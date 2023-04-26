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
        public bool SuccessfullyLoaded { get; set; } = false;
    }
}
