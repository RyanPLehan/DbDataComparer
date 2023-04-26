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
            IEnumerable<TestExecutionResult> results = Enumerable.Empty<TestExecutionResult>();

            // Look at test type to determine which tests should be executed
            if (testDefinition.StoredProcedureTests != null &&
                     testDefinition.StoredProcedureTests.Any())
            {
                results = await ExecuteStoredProcedureTests(testDefinition, cancellationToken);
            }
            else if (testDefinition.TableViewTests != null &&
                     testDefinition.TableViewTests.Any())
            {
                results = await ExecuteTableViewTests(testDefinition, cancellationToken);
            }

            return results;
        }



        /// <summary>
        /// This will execute all the tests that are defined within the TestDefinition
        /// </summary>
        /// <param name="testDefinition"></param>
        /// <returns></returns>
        private async Task<IEnumerable<TestExecutionResult>> ExecuteStoredProcedureTests(TestDefinition testDefinition,
                                                                                         CancellationToken cancellationToken)
        {
            IList<TestExecutionResult> results = new List<TestExecutionResult>();
            Stopwatch sw = new Stopwatch();

            // Iterate through Test
            foreach (StoredProcedureTest test in testDefinition.StoredProcedureTests)
            {
                cancellationToken.ThrowIfCancellationRequested();

                sw.Restart();
                TestExecutionResult result = await ExecuteTest(testDefinition.Source, testDefinition.Target, test);
                sw.Stop();

                result.ExecutionTime = sw.Elapsed;
                results.Add(result);
            }

            return results;
        }


        /// <summary>
        /// This will execute all the tests that are defined within the TestDefinition
        /// </summary>
        /// <param name="testDefinition"></param>
        /// <returns></returns>
        private async Task<IEnumerable<TestExecutionResult>> ExecuteTableViewTests(TestDefinition testDefinition,
                                                                                   CancellationToken cancellationToken)
        {
            IList<TestExecutionResult> results = new List<TestExecutionResult>();
            Stopwatch sw = new Stopwatch();

            // Iterate through Test
            foreach (TableViewTest test in testDefinition.TableViewTests)
            {
                cancellationToken.ThrowIfCancellationRequested();

                sw.Restart();
                TestExecutionResult result = await ExecuteTest(testDefinition.Source, testDefinition.Target, test);
                sw.Stop();

                result.ExecutionTime = sw.Elapsed;
                results.Add(result);
            }

            return results;
        }


        private async Task<TestExecutionResult> ExecuteTest(ExecutionDefinition source, ExecutionDefinition target, StoredProcedureTest test)
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

        private async Task<TestExecutionResult> ExecuteTest(ExecutionDefinition source, ExecutionDefinition target, TableViewTest test)
        {
            // Execute commands in parallel
            var srcExeTask = this.Database.Execute(source.ConnectionString,
                                                   source,
                                                   test.SourceSql);

            var tgtExeTask = this.Database.Execute(target.ConnectionString,
                                                   target,
                                                   test.TargetSql);

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
