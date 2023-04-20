﻿using System;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Configuration;
using DbDataComparer.Domain;
using DbDataComparer.Domain.Configuration;
using DbDataComparer.Domain.Enums;
using DbDataComparer.Domain.Formatters;
using DbDataComparer.Domain.Models;
using DbDataComparer.MSSql;

namespace DbDataComparer.Comparer
{
    public class Program
    {
        private const string FILE_EXTENSION = ".td";
        private const string SEARCH_PATTERN = "*" + FILE_EXTENSION;

        private static ConfigurationSettings Settings;

        public static void Main(string[] args)
        {
            try
            {
                InitializeSettings();

                switch (args.Length)
                {
                    case 0:
                        Execute().GetAwaiter().GetResult();
                        break;

                    case 1:
                        if (args[0].StartsWith('/') || args[0].StartsWith('\\') || args[0].StartsWith('-') || args[0].StartsWith('?'))
                            DisplayArgs();
                        else
                            Execute(args[0].Trim()).GetAwaiter().GetResult();
                        break;

                    default:
                        DisplayArgs();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("An error occurred while performing operation.  See error message below");
                Console.WriteLine(ex.Message);
            }
        }


        private static void DisplayArgs()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Comparer will load in all the Test Definition files from the location specified in the settings file.");
            sb.AppendLine("It will iterate through each test and execute the database objects with their respective test values.");
            sb.AppendLine("The results from each test will then be compared and saved to a file in the location specified in the settings file.");
            sb.AppendLine();

            sb.AppendLine("Usage: Comparer [fileName]");
            sb.AppendLine("\tfileName\tFile name of Test Definition.  This option allows for a single comparison.");
            sb.AppendLine("\t\t\tNote: The file must reside in the the locations that is specified in the settings file.");
            sb.AppendLine();

            Console.WriteLine(sb.ToString());
        }

        private static void InitializeSettings()
        {
            IConfiguration configuration = Initialize.LoadConfiguration();
            Settings = Initialize.GetConfigurationSettings(configuration);
            Initialize.BuildDirectoryStructure(Settings.Location);                      // Make sure Directories exist
        }

        private static async Task Execute(string fileName = null)
        {
            string now = DateTime.Now.ToString("yyy-MM-dd HH-mm-ss");
            string resultsFileName = String.Format("Results [{0}].txt", now);
            string resultsPathName = Path.Combine(Settings.Location.ComparisonResultsPath, resultsFileName);

            ITestExecutioner testExecutioner = new TestExecutioner(Settings.Database, new SqlDatabase());
            IEnumerable<string> pathNames = GetTestDefinitionFiles(fileName);

            // Iterate through files, continue even there is an error            
            foreach(string pathName in pathNames)
            {
                string parsedFileName = Path.GetFileNameWithoutExtension(pathName);
                string errorFileName = String.Format("{0} [{1}].txt", parsedFileName, now);
                string errorPathName = Path.Combine(Settings.Location.ComparisonErrorsPath, errorFileName);

                Console.WriteLine("Processing File: {0}{1}", parsedFileName, FILE_EXTENSION);

                try
                {
                    Console.WriteLine("\tLoading Test Definition");
                    TestDefinition testDefinition = await LoadTestDefinition(pathName);

                    Console.WriteLine("\tExecuting Tests");
                    IEnumerable<TestExecutionResult> testResults = await testExecutioner.Execute(testDefinition);

                    Console.WriteLine("\tComparing Test Results");
                    IEnumerable<ComparisonResult> comparisonResults = Compare(testDefinition, testResults);

                    Console.WriteLine("\tWriting Test Results");
                    await WriteOverallResults(resultsPathName, testDefinition, comparisonResults);

                    if (IsAny(comparisonResults, ComparisonResultTypeEnum.Failed))
                        await WriteDetailResults(errorPathName, testDefinition, comparisonResults);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\t***Error Occurred ***");
                    await WriteError(errorPathName, ex);
                }

                Console.WriteLine("\tDone!");
                Console.WriteLine();
            }

        }
        

        /// <summary>
        /// Create Test Definition File stored in the path defined in the Location Settings
        /// </summary>
        /// <returns></returns>
        private static async Task<TestDefinition> LoadTestDefinition(string pathName)
        {
            TestDefinition td = null;

            using (FileStream fs = new FileStream(pathName, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string json = await sr.ReadToEndAsync();
                    td = NSJson.Deserialize<TestDefinition>(json);
                }
            }

            return td;
        }

        private static IEnumerable<string> GetTestDefinitionFiles(string fileName = null)
        {
            string searchPattern = SEARCH_PATTERN;

            if (!String.IsNullOrWhiteSpace(fileName))
                searchPattern = String.Format("{0}{1}", Path.GetFileNameWithoutExtension(fileName), FILE_EXTENSION);

            return Directory.GetFiles(Settings.Location.TestDefinitionsPath, searchPattern);
        }

        private static IEnumerable<ComparisonResult> Compare(TestDefinition testDefinition, IEnumerable<TestExecutionResult> testResults)
        {
            Stopwatch sw = new Stopwatch();
            ITestComparer testComparer = new TestComparer();
            IList<ComparisonResult> comparisonResults = new List<ComparisonResult>();

            foreach (TestExecutionResult testResult in testResults)
            {
                ComparisonResult comparisonResult = new ComparisonResult(testResult);
                sw.Restart();

                // Check each compare option and perform test
                if (testDefinition.CompareOptions.ParameterReturn)
                    comparisonResult.ParameterReturnResult = testComparer.CompareParameterReturn(testResult.Source, testResult.Target);

                if (testDefinition.CompareOptions.ParameterOutput)
                    comparisonResult.ParameterOutputResult = testComparer.CompareParameterOutput(testResult.Source, testResult.Target);

                if (testDefinition.CompareOptions.ResultSetMetaData)
                    comparisonResult.ResultsetMetaDataResults = testComparer.CompareResultSetMetaData(testResult.Source, testResult.Target);

                if (testDefinition.CompareOptions.ResultSetData)
                    comparisonResult.ResultsetDataResults = testComparer.CompareResultSetData(testResult.Source, testResult.Target);

                sw.Stop();
                comparisonResult.ComparisonTime = sw.Elapsed;

                comparisonResults.Add(comparisonResult);
            }

            return comparisonResults;
        }

        private static async Task WriteOverallResults(string pathName, TestDefinition testDefinition, IEnumerable<ComparisonResult> comparisonResults)
        {
            using (FileStream fs = new FileStream(pathName, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    await sw.WriteLineAsync(String.Format("{0} Results:", testDefinition.Name));
                    await sw.WriteAsync(Text.IndentChars);
                    await sw.WriteLineAsync(String.Format("Source Command: {0}", testDefinition.Source.Text));
                    await sw.WriteAsync(Text.IndentChars);
                    await sw.WriteLineAsync(String.Format("Target Command: {0}", testDefinition.Target.Text));
                    await sw.WriteLineAsync();

                    foreach (ComparisonResult cr in comparisonResults)
                    {
                        await sw.WriteAsync(Text.IndentChars);
                        await sw.WriteLineAsync(String.Format("Test: {0} ", cr.TestResult.TestName));

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
                        await sw.WriteLineAsync(String.Format("Parameter Return: {0}", cr.ParameterReturnResult.Result.ToString()));
                        await sw.WriteAsync(Text.IndentChars + Text.IndentChars);
                        await sw.WriteLineAsync(String.Format("Parameter Output: {0}:", cr.ParameterOutputResult.Result.ToString()));

                        foreach(KeyValuePair<int, TestComparisonResult> kvp in cr.ResultsetMetaDataResults)
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
            }
        }

        private static async Task WriteDetailResults(string pathName, TestDefinition testDefinition, IEnumerable<ComparisonResult> comparisonResults)
        {
            using (FileStream fs = new FileStream(pathName, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    await sw.WriteLineAsync(String.Format("{0} Error Descriptions:", testDefinition.Name));
                    foreach (ComparisonResult cr in comparisonResults)
                    {
                        if (!IsAny(cr, ComparisonResultTypeEnum.Failed))
                            continue;

                        await sw.WriteAsync(Text.IndentChars);
                        await sw.WriteLineAsync(String.Format("Test: {0} ", cr.TestResult.TestName));

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
            }
        }

        private static async Task WriteError(string pathName, Exception ex)
        {
            using (FileStream fs = new FileStream(pathName, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    await sw.WriteLineAsync();
                    await sw.WriteLineAsync("An error occurred while processing comparison routines");
                    await sw.WriteLineAsync(ex.Message);
                }
            }
        }

        private static bool IsAny(IEnumerable<ComparisonResult> comparisonResults, ComparisonResultTypeEnum result)
        {
            bool ret = false;

            foreach(ComparisonResult cr in comparisonResults)
                ret = ret || IsAny(cr, result);

            return ret;
        }

        private static bool IsAny(ComparisonResult comparisonResult, ComparisonResultTypeEnum result)
        {
            bool ret = false;

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


        private static string FormatTimeSpan(TimeSpan ts)
        {
            return String.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours,
                                                                 ts.Minutes,
                                                                 ts.Seconds,
                                                                 ts.Milliseconds);
        }

    }
}