using System.Threading.Tasks;
using DbDataComparer.Domain.Models;

namespace DbDataComparer.Domain
{
    public interface ITestExecutioner
    {
        Task<IEnumerable<TestExecutionResult>> Execute(TestDefinition testDefinition,
                                                       CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<TestExecutionResult>> Execute(TestDefinition testDefinition,
                                                       IProgress<string> progress,
                                                       CancellationToken cancellationToken);

    }
}
