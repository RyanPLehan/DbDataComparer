using System;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DbDataComparer.Domain.Models;
using DbDataComparer.Domain;
using DbDataComparer.MSSql;

namespace DbDataComparer.UI
{
    internal static class IOUtility
    {
        /// <summary>
        /// PathName is made up of base path + Source Database Name + Name of Test Definition 
        /// </summary>
        /// <param name="testDefinition"></param>
        /// <returns></returns>
        public static string CreatePathName(string rootPath, TestDefinition testDefinition)
        {
            if (testDefinition == null)
                throw new ArgumentNullException(nameof(testDefinition));

            // To get Source Database name, use Connection Properies Builder to parse
            IConnectionProperties connectionProperties = ConnectionPropertiesBuilder.Parse(testDefinition.Source.ConnectionString);
            var options = connectionProperties.ConnectionBuilderOptions;

            string path = Path.Combine(rootPath, options.Database);
            CreateDirectory(path);

            return Path.Combine(path, TestDefinitionIO.CreateFileName(testDefinition));
        }

        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public static string ExecutablePath()
        {
            return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        }
    }
}
