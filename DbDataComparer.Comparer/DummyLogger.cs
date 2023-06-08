using System;
using System.Threading.Tasks;
using DbDataComparer.Domain;
using DbDataComparer.Domain.Models;

namespace DbDataComparer.Comparer
{
    internal class DummyLogger : ICustomLogger
    {
        public Task Close()
        {
            return Task.CompletedTask;
        }

        public Task Log(TestDefinition testDefinition, IEnumerable<ComparisonResult> comparisonResults)
        {
            return Task.CompletedTask;
        }

        public Task Open()
        {
            return Task.CompletedTask;
        }
    }
}
