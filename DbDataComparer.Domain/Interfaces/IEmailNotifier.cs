using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbDataComparer.Domain.Models;

namespace DbDataComparer.Domain.Interfaces
{
    public interface IEmailNotifier
    {
        bool IsNotificationEnabled(TestDefinition testDefinition, IEnumerable<ComparisonResult> comparisonResults);
        Task AddNotification(TestDefinition testDefinition, IEnumerable<ComparisonResult> comparisonResults);
        Task SendNotification(string subject);
        Task SendNotification(string subject, string body);
    }
}
