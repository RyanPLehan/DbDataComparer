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
        private static string ProcessDateTime { get; set; }
        private static string ResultsPathName { get; set; }

        public static void Main(string[] args)
        {
            try
            {
                InitializeSettings();

                if (args == null || args.Length == 0)
                    Execute().GetAwaiter().GetResult();
                else
                    DisplayArgs();
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
            ProcessDateTime = DateTime.Now.ToString("yyy-MM-dd HH-mm-ss");
            string resultsFileName = String.Format("Results [{0}].txt", ProcessDateTime);
            ResultsPathName = Path.Combine(Settings.Location.ComparisonResultsPath, resultsFileName);
            await ProcessDirectory(Settings.Location.TestDefinitionsPath);
        }
        

        private static async Task ProcessDirectory(string directory)
        {
            Console.WriteLine("Processing Directory: {0}", directory);
            Console.WriteLine();

            // Process all files first, then the directories
            var pathNames = Directory.GetFiles(directory, TestDefinitionIO.SEARCH_PATTERN);
            if (pathNames != null && pathNames.Length > 0)
                await ProcessFiles(pathNames);

            // Use recursion to process directories
            var directories = Directory.GetDirectories(directory);
            foreach (string dir in directories)
                await ProcessDirectory(dir);
        }

        private static async Task ProcessFiles(string[] pathNames)
        {
            ITestExecutioner testExecutioner = new TestExecutioner(new SqlDatabase());

            // Iterate through files, continue even there is an error            
            foreach (string pathName in pathNames)
            {
                string parsedFileName = Path.GetFileNameWithoutExtension(pathName);
                string errorFileName = String.Format("{0} [{1}].txt", parsedFileName, ProcessDateTime);
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
                    await WriteOverallResults(ResultsPathName, testDefinition, comparisonResults);

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