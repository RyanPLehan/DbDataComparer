using DbDataComparer.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DbDataComparer.Domain
{
    public static class ApplicationIO
    {
        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }


        public static string GetCurrentPath()
        {
            return Directory.GetCurrentDirectory();
        }

        public static string GetExecutablePath()
        {
            return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        }

        public static string GetTestDefinitionPath(LocationSettings settings)
        {
            return Path.Combine(GetExecutablePath(), settings.TestDefinitionsPath);
        }

        public static string GetComparisonResultPath(LocationSettings settings)
        {
            return Path.Combine(GetExecutablePath(), settings.ComparisonResultsPath);
        }

        public static string GetComparisonErrorPath(LocationSettings settings)
        {
            return Path.Combine(GetExecutablePath(), settings.ComparisonErrorsPath);
        }

    }
}
