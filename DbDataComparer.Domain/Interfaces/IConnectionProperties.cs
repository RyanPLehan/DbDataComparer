using DbDataComparer.Domain.Models;
using System;
using System.Threading.Tasks;

namespace DbDataComparer.Domain
{
    public interface IConnectionProperties
    {
        string ConnectionString { get; }
        ConnectionBuilderOptions ConnectionBuilderOptions { get; }

        void Test();
        Task TestAsync();
    }
}
