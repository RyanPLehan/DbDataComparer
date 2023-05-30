using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbDataComparer.Domain.Enums;
using DbDataComparer.Domain.Formatters;
using DbDataComparer.Domain.Models;

namespace DbDataComparer.Domain
{
    public static class TestDefinitionComparer
    {
        public static IEnumerable<ComparisonResult> Compare(TestDefinition testDefinition, IEnumerable<TestExecutionResult> testResults)
        {
            Stopwatch sw = new Stopwatch();
            ITestComparer testComparer = new TestComparer();
            IList<ComparisonResult> comparisonResults = new List<ComparisonResult>();

            foreach (TestExecutionResult testResult in testResults)
            {
                ComparisonResult comparisonResult = new ComparisonResult(testResult);
                sw.Restart();

                // Check each compare option and perform test
                comparisonResult.ExecutionResult = testComparer.CompareExecution(testResult.Source, testResult.Target);
                
                if (comparisonResult.ExecutionResult.Result == ComparisonResultTypeEnum.Failed)
                {
                    comparisonResult.ParameterReturnResult = new TestComparisonResult();
                    comparisonResult.ParameterOutputResult = new TestComparisonResult();
                    comparisonResult.ResultsetMetaDataResults = CreateFailedList();
                    comparisonResult.ResultsetDataResults = CreateFailedList();
                }
                else
                {
                    if (testDefinition.CompareOptions.ParameterReturn)
                        comparisonResult.ParameterReturnResult = testComparer.CompareParameterReturn(testResult.Source, testResult.Target);

                    if (testDefinition.CompareOptions.ParameterOutput)
                        comparisonResult.ParameterOutputResult = testComparer.CompareParameterOutput(testResult.Source, testResult.Target);

                    if (testDefinition.CompareOptions.ResultSetMetaData)
                        comparisonResult.ResultsetMetaDataResults = testComparer.CompareResultSetMetaData(testResult.Source, testResult.Target);

                    if (testDefinition.CompareOptions.ResultSetData)
                        comparisonResult.ResultsetDataResults = testComparer.CompareResultSetData(testResult.Source, testResult.Target);
                }

                sw.Stop();
                comparisonResult.ComparisonTime = sw.Elapsed;

                comparisonResults.Add(comparisonResult);
            }

            return comparisonResults;
        }

        public static async Task WriteOverallResults(StreamWriter sw, TestDefinition testDefinition, IEnumerable<ComparisonResult> comparisonResults)
        {
            await sw.WriteLineAsync(String.Format("Results: {0}", testDefinition.Name));
            await sw.WriteAsync(Text.IndentChars);
            await sw.WriteLineAsync(String.Format("Source Command: {0}", testDefinition.Source.Text));
            await sw.WriteAsync(Text.IndentChars);
            await sw.WriteLineAsync(String.Format("Target Command: {0}", testDefinition.Target.Text));
            await sw.WriteLineAsync();

            foreach (ComparisonResult cr in comparisonResults)
            {
                await sw.WriteAsync(Text.IndentChars);
                await sw.WriteLineAsync(String.Format("Test: {0}", cr.TestResult.TestName));

                await sw.WriteAsync(Text.IndentChars);
                await sw.WriteLineAsync("Statistics:");
                //await sw.WriteLineAsync(String.Format("\t\tOverall Execution Time: {0}", FormatTimeSpan(cr.TestResult.ExecutionTime)));
                //await sw.WriteLineAsync(String.Format("\t\tOverall Comparison Time: {0}", FormatTimeSpan(cr.ComparisonTime)));
                await sw.WriteAsync(Text.IndentChars + Text.IndentChars);
                await sw.WriteLineAsync(String.Format("Source Execution Time: {0}", FormatTimeSpan(cr.TestResult.Source.ExecutionTime)));
                await sw.WriteAsync(Text.IndentChars + Text.IndentChars);
                await sw.WriteLineAsync(String.Format("Target Execution Time: {0}", FormatTimeSpan(cr.TestResult.Target.ExecutionTime)));

                TimeSpan execTimeDiff = cr.TestResult.Target.ExecutionTime - cr.TestResult.Source.ExecutionTime;
                await sw.WriteAsync(Text.IndentChars + Text.IndentChars);
                await sw.WriteLineAsync(String.Format("Execution Time Difference (Target - Source): {0}", FormatTimeSpan(execTimeDiff)));
                //await sw.WriteLineAsync();

                await sw.WriteAsync(Text.IndentChars);
                await sw.WriteLineAsync("Comparison Results:");
                await sw.WriteAsync(Text.IndentChars + Text.IndentChars);
                await sw.WriteLineAsync(String.Format("Execution: {0}", cr.ExecutionResult.Result.ToString()));

                await sw.WriteAsync(Text.IndentChars + Text.IndentChars);
                await sw.WriteLineAsync(String.Format("Parameter Return: {0}", cr.ParameterReturnResult.Result.ToString()));
                await sw.WriteAsync(Text.IndentChars + Text.IndentChars);
                await sw.WriteLineAsync(String.Format("Parameter Output: {0}", cr.ParameterOutputResult.Result.ToString()));

                foreach (KeyValuePair<int, TestComparisonResult> kvp in cr.ResultsetMetaDataResults)
                {
                    await sw.WriteAsync(Text.IndentChars + Text.IndentChars);
                    await sw.WriteLineAsync(String.Format("Metadata Result Set #{0} - {1}", kvp.Key + 1, kvp.Value.Result.ToString()));
                }

                foreach (KeyValuePair<int, TestComparisonResult> kvp in cr.ResultsetDataResults)
                {
                    await sw.WriteAsync(Text.IndentChars + Text.IndentChars);
                    await sw.WriteLineAsync(String.Format("Actual Data Result Set #{0} - {1}", kvp.Key + 1, kvp.Value.Result.ToString()));
                }

                await sw.WriteLineAsync();
            }

            await sw.WriteLineAsync();
        }

        public static async Task WriteDetailResults(StreamWriter sw, TestDefinition testDefinition, IEnumerable<ComparisonResult> comparisonResults)
        {
            await sw.WriteLineAsync(String.Format("Error Descriptions: {0}", testDefinition.Name));
            foreach (ComparisonResult cr in comparisonResults)
            {
                if (!IsAny(cr, ComparisonResultTypeEnum.Failed))
                    continue;

                await sw.WriteAsync(Text.IndentChars);
                await sw.WriteLineAsync(String.Format("Test: {0} ", cr.TestResult.TestName));

                if (cr.ExecutionResult.Result == ComparisonResultTypeEnum.Failed)
                {
                    await sw.WriteAsync(Text.IndentChars + Text.IndentChars);
                    await sw.WriteLineAsync("Execution Result");
                    await sw.WriteLineAsync(Text.Indent(cr.ExecutionResult.ResultDescription, Text.IndentChars + Text.IndentChars));
                    await sw.WriteLineAsync();
                }

                if (cr.ParameterReturnResult.Result == ComparisonResultTypeEnum.Failed)
                {
                    await sw.WriteAsync(Text.IndentChars + Text.IndentChars);
                    await sw.WriteLineAsync("Stored Procedure Return Value");
                    await sw.WriteLineAsync(Text.Indent(cr.ParameterReturnResult.ResultDescription, Text.IndentChars + Text.IndentChars));
                    await sw.WriteLineAsync();
                }

                if (cr.ParameterOutputResult.Result == ComparisonResultTypeEnum.Failed)
                {
                    await sw.WriteAsync(Text.IndentChars + Text.IndentChars);
                    await sw.WriteLineAsync("Stored Procedure Parameter Output Values");
                    await sw.WriteLineAsync(Text.Indent(cr.ParameterOutputResult.ResultDescription, Text.IndentChars + Text.IndentChars));
                    await sw.WriteLineAsync();
                }

                foreach (KeyValuePair<int, TestComparisonResult> kvp in cr.ResultsetMetaDataResults)
                {
                    if (kvp.Value.Result == ComparisonResultTypeEnum.Failed)
                    {
                        await sw.WriteAsync(Text.IndentChars);
                        await sw.WriteLineAsync(String.Format("Metadata from Result Set #{0}", kvp.Key + 1));
                        await sw.WriteLineAsync(Text.Indent(kvp.Value.ResultDescription, Text.IndentChars + Text.IndentChars));
                        await sw.WriteLineAsync();
                    }
                }

                foreach (KeyValuePair<int, TestComparisonResult> kvp in cr.ResultsetDataResults)
                {
                    if (kvp.Value.Result == ComparisonResultTypeEnum.Failed)
                    {
                        await sw.WriteAsync(Text.IndentChars);
                        await sw.WriteLineAsync(String.Format("Actual data from Result Set #{0}", kvp.Key + 1));
                        await sw.WriteLineAsync(Text.Indent(kvp.Value.ResultDescription, Text.IndentChars + Text.IndentChars));
                        await sw.WriteLineAsync();
                    }
                }
            }
        }

        public static async Task WriteError(StreamWriter sw, Exception ex)
        {
            await sw.WriteLineAsync();
            await sw.WriteLineAsync("An error occurred while processing comparison routines");
            await sw.WriteLineAsync(ex.Message);
        }

        public static bool IsAny(IEnumerable<ComparisonResult> comparisonResults, ComparisonResultTypeEnum result)
        {
            bool ret = false;

            foreach (ComparisonResult cr in comparisonResults)
                ret = ret || IsAny(cr, result);

            return ret;
        }

        public static bool IsAny(ComparisonResult comparisonResult, ComparisonResultTypeEnum result)
        {
            bool ret = false;

            ret = ret || (comparisonResult.ExecutionResult.Result == result);

            ret = ret || (comparisonResult.ParameterReturnResult.Result == result);
            ret = ret || (comparisonResult.ParameterOutputResult.Result == result);

            foreach (KeyValuePair<int, TestComparisonResult> kvp in comparisonResult.ResultsetMetaDataResults)
            {
                ret = ret || (kvp.Value.Result == result);
            }

            foreach (KeyValuePair<int, TestComparisonResult> kvp in comparisonResult.ResultsetDataResults)
            {
                ret = ret || (kvp.Value.Result == result);
            }

            return ret;
        }


        public static string FormatTimeSpan(TimeSpan ts)
        {
            return String.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours,
                                                                 ts.Minutes,
                                                                 ts.Seconds,
                                                                 ts.Milliseconds);
        }


        private static IDictionary<int, TestComparisonResult> CreateFailedList()
        {
            var ret = new Dictionary<int, TestComparisonResult>();
            ret.Add(0, new TestComparisonResult());

            return ret;
        }
    }
}
