using System;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Configuration;
using DbDataComparer.Domain;
using DbDataComparer.Domain.Configuration;
using DbDataComparer.Domain.Formatters;
using DbDataComparer.Domain.Models;
using DbDataComparer.MSSql;

namespace DbDataComparer.Explorer
{
    public class Program
    {
        private const string FILE_EXTENSION = ".td";
        private static ConfigurationSettings Settings;

        public static void Main(string[] args)
        {
            try
            {
                InitializeSettings();

                if (args.Length != 3)
                    DisplayArgs();
                else
                    Execute(args).GetAwaiter().GetResult();
            }
            catch(Exception ex)
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

            sb.AppendLine("Explorer will build a Test Definition file by exploring the given MS SQL Server database objects.");
            sb.AppendLine("This will also create 3 sample tests and example values.");
            sb.AppendLine();

            sb.AppendLine("Usage: Explorer name source target");
            sb.AppendLine("\tname\t\tName of Test Definition.  This will also be the name of the Test Definition file");
            sb.AppendLine("\tsource\t\tName of the original database object that will be explored");
            sb.AppendLine("\ttarget\t\tName of the new database object that will be explored");
            sb.AppendLine();
            sb.AppendLine("Example: Explorer FCM_GetLinkedCarriers dbo.spFCM_GetLinkedCarriers fin.spFCM_GetLinkedCarriers");
            sb.AppendLine();

            Console.WriteLine(sb.ToString());
        }


        private static void InitializeSettings()
        {
            IConfiguration configuration = Initialize.LoadConfiguration();
            Settings = Initialize.GetConfigurationSettings(configuration);
            Initialize.BuildDirectoryStructure(Settings.Location);                      // Make sure Directories exist
        }

        private static async Task Execute(string[] args)
        {
            Console.WriteLine("Exploring {0} and {1}", args[1], args[2]);
            TestDefinitionBuilderOptions options = CreateOptions(args[0], args[1], args[2]);
            TestDefinition td = await CreateTestDefinition(options);

            string pathName = CreatePathName(td.Name);
            string fileName = Path.GetFileName(pathName);
            Console.WriteLine("Creating file: {0}", fileName);

            if (ContinueFileCreation(pathName))
            {
                Console.Write("Writing...");
                await CreateFile(td, pathName);
                Console.WriteLine("Done!");
            }
            else
            {
                Console.WriteLine("Aborting!");
            }
        }

        private static TestDefinitionBuilderOptions CreateOptions(string name, 
                                                                  string sourceDbObjectName, 
                                                                  string targetDbObjectName)
        {
            return new TestDefinitionBuilderOptions()
            {
                Name = name,
                Source = new TestDefinitionBuilderOptions.DatabaseOptions()
                {
                    ConnectionString = Settings.Database.SourceConnection,
                    DatabaseObjectName = sourceDbObjectName,
                },
                Target = new TestDefinitionBuilderOptions.DatabaseOptions()
                {
                    ConnectionString = Settings.Database.TargetConnection,
                    DatabaseObjectName = targetDbObjectName,
                },
            };
        }

        /// <summary>
        /// Create Test Definition by exploring the MS database objects and creating sample test values
        /// </summary>
        /// <param name="name"></param>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private static async Task<TestDefinition> CreateTestDefinition(TestDefinitionBuilderOptions options)
        {
            var builder = new TestDefinitionBuilder(new SqlDatabase());
            return await builder.Build(options);
        }

        /// <summary>
        /// Clean, standardize file name and prepend directory path
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static string CreatePathName(string fileName)
        {
            string cleansed = fileName.Replace(@"\", "_").Replace(@"/", "_").Replace(":", "_");
            if (!fileName.EndsWith(FILE_EXTENSION, StringComparison.OrdinalIgnoreCase))
                cleansed = cleansed + FILE_EXTENSION;

            return Path.Combine(Settings.Location.TestDefinitionsPath, cleansed);
        }

        private static bool ContinueFileCreation(string pathName)
        {
            char response = 'Y';
            string fileName = Path.GetFileName(pathName);

            if (File.Exists(pathName))
            {
                Console.Write("{0} already exists.  Overwrite Y/N? ", fileName);
                response = (char)Console.Read();
            }

            return (response == 'Y' || response == 'y');
        }


        /// <summary>
        /// Create Test Definition File stored in the path defined in the Location Settings
        /// </summary>
        /// <returns></returns>
        private static async Task CreateFile(TestDefinition testDefinition, string pathName)
        {
            using (FileStream fs = new FileStream(pathName, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    try
                    {
                        await sw.WriteLineAsync(NSJson.Serialize(testDefinition));
                    }
                    catch (Exception ex)
                    {
                        await sw.WriteLineAsync("Error occurred while creating the Test Definition");
                        await sw.WriteLineAsync();
                        await sw.WriteLineAsync("Exception Message below");
                        await sw.WriteLineAsync(ex.Message);
                    }
                }
            }
        }
    }
}