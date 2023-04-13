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
        private readonly DatabaseSettings Settings;

        public TestExecutioner(DatabaseSettings databaseSettings,
                               IDatabase database)
        {
            this.Database = database;
            this.Settings = databaseSettings;
        }

        /// <summary>
        /// This will execute all the tests that are defined within the TestDefinition
        /// </summary>
        /// <param name="testDefinition"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TestExecutionResult>> Execute(TestDefinition testDefinition)
        {
            IList<TestExecutionResult> results = new List<TestExecutionResult>();
            Stopwatch sw = new Stopwatch();

            // Iterate through Test
            foreach(Test test in testDefinition.Tests)
            {
                sw.Restart();
                TestExecutionResult result = await ExecuteTest(testDefinition.Source, testDefinition.Target, test);
                sw.Stop();

                result.ExecutionTime = sw.Elapsed;
                results.Add(result);
            }

            return results;
        }

        private async Task<TestExecutionResult> ExecuteTest(Command source, Command target, Test test)
        {
            // Execute commands in parallel
            var srcExeTask = this.Database.Execute(this.Settings.SourceConnection,
                                                   source,
                                                   test.SourceTestValues);

            var tgtExeTask = this.Database.Execute(this.Settings.TargetConnection,
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
