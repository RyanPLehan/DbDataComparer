using DbDataComparer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbDataComparer.Domain
{
    public interface ICustomLogger
    {
        Task Log(TestDefinition testDefinition, IEnumerable<ComparisonResult> comparisonResults);
        Task Open();
        Task Close();
    }
}
