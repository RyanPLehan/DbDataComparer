using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbDataComparer.UI.Models
{
    public class DataExplorerDataSourceChangedEventArgs : EventArgs
    {
        public string DataSource { get; set; }
    }
}
