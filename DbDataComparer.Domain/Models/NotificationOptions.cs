using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbDataComparer.Domain.Models
{
    public class NotificationOptions
    {
        public bool NotifyOnFailure { get; set; } = false;
        public string Email { get; set; } = String.Empty;
    }
}
