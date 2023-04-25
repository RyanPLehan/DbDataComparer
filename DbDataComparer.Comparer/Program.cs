using System;
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

            ITestExecutioner testExecutioner = new TestExecutioner(new SqlDatabase());
            IEnumerable<string> pathNames = GetTestDefinitionFiles(fileName);

            // Iterate through files, continue even there is an error            
            foreach(string pathName in pathNames)
            {
                string parsedFileName = Path.GetFileNameWithoutExtension(pathName);
                string errorFileName = String.Format("{0} [{1}].txt", parsedFileName, now);
                string errorPathName = Path.Combine(Settings.Location.ComparisonErrorsPath, errorFileName);

                Console.WriteLine("Processing File: {0}{1}", parsedFileName, TestDefinitionIO.FILE_EXTENSION);

                try
                {
                    Console.WriteLine("\tLoading Test Definition");
                    TestDefinition testDefinition = await TestDefinitionIO.LoadFileAsync(pathName);

                    Console.WriteLine("\tExecuting Tests");
                    IEnumerable<TestExecutionResult> testResults = await testExecutioner.Execute(testDefinition);

                    Console.WriteLine("\tComparing Test Results");
                    IEnumerable<ComparisonResult> comparisonResults = TestDefinitionComparer.Compare(testDefinition, testResults);

                    Console.WriteLine("\tWriting Test Results");
                    await WriteOverallResults(resultsPathName, testDefinition, comparisonResults);

                    if (TestDefinitionComparer.IsAny(comparisonResults, ComparisonResultTypeEnum.Failed))
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
        


        private static IEnumerable<string> GetTestDefinitionFiles(string fileName = null)
        {
            string searchPattern = TestDefinitionIO.SEARCH_PATTERN;

            if (!String.IsNullOrWhiteSpace(fileName))
                searchPattern = String.Format("{0}{1}", Path.GetFileNameWithoutExtension(fileName), TestDefinitionIO.FILE_EXTENSION);

            return Directory.GetFiles(Settings.Location.TestDefinitionsPath, searchPattern);
        }

        private static async Task WriteOverallResults(string pathName, TestDefinition testDefinition, IEnumerable<ComparisonResult> comparisonResults)
        {
            using (FileStream fs = new FileStream(pathName, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    await TestDefinitionComparer.WriteOverallResults(sw, testDefinition, comparisonResults);
                }
            }
        }

        private static async Task WriteDetailResults(string pathName, TestDefinition testDefinition, IEnumerable<ComparisonResult> comparisonResults)
        {
            using (FileStream fs = new FileStream(pathName, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    await TestDefinitionComparer.WriteDetailResults(sw, testDefinition, comparisonResults);
                }
            }
        }

        private static async Task WriteError(string pathName, Exception ex)
        {
            using (FileStream fs = new FileStream(pathName, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    await TestDefinitionComparer.WriteError(sw, ex);
                }
            }
        }

    }
}