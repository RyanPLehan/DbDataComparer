using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DbDataComparer.Domain.Configuration;
using DbDataComparer.Domain.Formatters;
using DbDataComparer.Domain.Models;

namespace DbDataComparer.Domain
{
    public static class TestDefinitionIO
    {
        public const string FILE_EXTENSION = ".td";
        public const string SEARCH_PATTERN = "*" + FILE_EXTENSION;

        private static string[] DatabaseSearchTerms = new string[] { "Initial Catalog", "Database" };

        public static string CreateFileName(TestDefinition testDefinition)
        {
            if (testDefinition == null)
                throw new ArgumentNullException(nameof(testDefinition));

            string fileName = testDefinition.Name.Replace(@"\", "_").Replace(@"/", "_").Replace(":", "_");
            if (!testDefinition.Name.EndsWith(FILE_EXTENSION, StringComparison.OrdinalIgnoreCase))
                fileName = fileName + FILE_EXTENSION;

            return fileName;
        }


        /// <summary>
        /// PathName is made up of base path + Source Database Name + Name of Test Definition 
        /// </summary>
        /// <param name="testDefinition"></param>
        /// <returns></returns>
        public static string CreatePathName(string path, TestDefinition testDefinition)
        {
            if (String.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path));

            if (testDefinition == null)
                throw new ArgumentNullException(nameof(testDefinition));

            string database = SearchConnectionString(testDefinition.Source.ConnectionString, DatabaseSearchTerms);
            string databasePath = Path.Combine(path, database);
            ApplicationIO.CreateDirectory(databasePath);

            return Path.Combine(databasePath, TestDefinitionIO.CreateFileName(testDefinition));
        }

        
        /// <summary>
        /// Create Test Definition File stored in the given path 
        /// </summary>
        /// <returns></returns>
        public static void CreateFile(string pathName, TestDefinition testDefinition)
        {
            using (FileStream fs = new FileStream(pathName, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(NSJson.Serialize(testDefinition));
                }
            }
        }


        /// <summary>
        /// Create Test Definition File stored in the given path 
        /// </summary>
        /// <returns></returns>
        public static async Task CreateFileAsync(string pathName, TestDefinition testDefinition)
        {
            using (FileStream fs = new FileStream(pathName, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    await sw.WriteLineAsync(NSJson.Serialize(testDefinition));
                }
            }
        }


        public static TestDefinition LoadFile(string pathName)
        {
            TestDefinition td = null;

            using (FileStream fs = new FileStream(pathName, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string json = sr.ReadToEnd();
                    td = NSJson.Deserialize<TestDefinition>(json);
                }
            }

            return td;
        }

        public static async Task<TestDefinition> LoadFileAsync(string pathName)
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


        private static string SearchConnectionString(string connectionString, IEnumerable<string> searchTerms)
        {
            string[] tokens = connectionString.Split(';');

            foreach(string token in tokens)
            {
                // need to split by 2nd delimiter to get true item to search by
                string[] keyValue = token.Split('=');

                if (keyValue.Length == 2)
                {
                    foreach (string term in searchTerms)
                    {
                        if (keyValue[0].Trim().Equals(term, StringComparison.OrdinalIgnoreCase))
                        {
                            return keyValue[1].Trim();
                        }
                    }
                }
            }

            return null;
        }
    }
}
