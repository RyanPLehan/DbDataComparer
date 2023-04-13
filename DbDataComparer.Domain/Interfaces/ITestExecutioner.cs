using System.Threading.Tasks;
using DbDataComparer.Domain.Models;

namespace DbDataComparer.Domain
{
    public interface ITestExecutioner
    {
        Task<IEnumerable<TestExecutionResult>> Execute(TestDefinition testDefinition);
    }
}
