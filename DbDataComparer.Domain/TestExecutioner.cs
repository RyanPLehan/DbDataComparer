using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using DbDataComparer.Domain.Configuration;
using DbDataComparer.Domain.Models;

namespace DbDataComparer.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class TestExecutioner : ITestExecutioner
    {
        private readonly IDatabase Database;

        public TestExecutioner(IDatabase database)
        {
            this.Database = database;
        }

        /// <summary>
        /// This will execute all the tests that are defined within the TestDefinition
        /// </summary>
        /// <param name="testDefinition"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TestExecutionResult>> Execute(TestDefinition testDefinition,
                                                                    CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Execute(testDefinition, null, cancellationToken);
        }


        /// <summary>
        /// This will execute all the tests that are defined within the TestDefinition
        /// </summary>
        /// <param name="testDefinition"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TestExecutionResult>> Execute(TestDefinition testDefinition,
                                                                    IProgress<string> progress,
                                                                    CancellationToken cancellationToken)
        {
            IList<TestExecutionResult> results = new List<TestExecutionResult>();
            Stopwatch sw = new Stopwatch();

            // Iterate through Test
            foreach (Test test in testDefinition.Tests)
            {
                if (progress != null)
                    progress.Report(String.Format("Executing test: {0}", test.Name));

                cancellationToken.ThrowIfCancellationRequested();

                sw.Restart();
                TestExecutionResult result = await ExecuteTest(testDefinition.Source, testDefinition.Target, test);
                sw.Stop();

                result.ExecutionTime = sw.Elapsed;
                results.Add(result);
            }

            return results;
        }


        private async Task<TestExecutionResult> ExecuteTest(ExecutionDefinition source, ExecutionDefinition target, Test test)
        {
            // Execute commands in parallel
            var srcExeTask = this.Database.Execute(source.ConnectionString,
                                                   source,
                                                   test.SourceTestValues);

            var tgtExeTask = this.Database.Execute(target.ConnectionString,
                                                   target,
                                                   test.TargetTestValues);

            // Wait until both exeuctions have completed
            await Task.WhenAll(srcExeTask, tgtExeTask);

            return new TestExecutionResult()
            {
                TestName = test.Name,
                Source = await srcExeTask,
                Target = await tgtExeTask,
            };
        }

    }
}
