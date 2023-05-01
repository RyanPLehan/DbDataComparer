using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbDataComparer.Domain.Configuration;

namespace DbDataComparer.UI.Models
{
    public class ConfigurationQueryRequestedEventArgs : EventArgs
    {
        // Response Values
        public ConfigurationSettings ConfigurationSettings { get; set; }
    }
}
