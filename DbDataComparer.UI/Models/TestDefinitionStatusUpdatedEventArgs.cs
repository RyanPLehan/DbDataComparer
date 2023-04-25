using DbDataComparer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbDataComparer.UI.Models
{
    public class TestDefinitionStatusUpdatedEventArgs : EventArgs
    {
        // Request Values
        public string Status { get; set; }
    }
}
