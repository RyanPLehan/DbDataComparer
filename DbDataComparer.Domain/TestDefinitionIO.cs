using System;
using System.Threading.Tasks;
using DbDataComparer.Domain.Formatters;
using DbDataComparer.Domain.Models;

namespace DbDataComparer.Domain
{
    public static class TestDefinitionIO
    {
        public const string FILE_EXTENSION = ".td";
        public const string SEARCH_PATTERN = "*" + FILE_EXTENSION;


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

    }
}
