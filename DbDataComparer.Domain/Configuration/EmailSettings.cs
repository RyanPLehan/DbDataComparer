using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbDataComparer.Domain.Configuration
{
    public class EmailSettings
    {
        public string Server { get; set; } = "Appmail.tql.com";
        public string Subject { get; set; } = "ERP Data Comparer";
        public string To { get; set; }
        public string From { get; set; } = "noreply@tql.com";
        public string CC { get; set; }
    }
}
