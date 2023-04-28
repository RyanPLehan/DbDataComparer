using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbDataComparer.Domain.Interfaces
{
    public interface IEmailNotification
    {
        Task Send(string toAddress, string fromAddress, string ccAddress, string subject, string body, bool isBodyHtml = false);
        Task Send(IEnumerable<string> toAddresses, string fromAddress, IEnumerable<string> ccAddresses, string subject, string body, bool isBodyHtml = false);
    }
}
