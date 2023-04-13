using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace DbDataComparer.Domain.Configuration
{
    public static class Initialize
    {
        public static IConfiguration LoadConfiguration()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            LoadCustomSettings(builder);
            return builder.Build();
        }

        public static ConfigurationSettings GetConfigurationSettings(IConfiguration configuration)
        {
            ConfigurationSettings settings = new ConfigurationSettings()
            {
                Database = configuration.GetSection("Database").Get<DatabaseSettings>() ??
                            new DatabaseSettings(),
                Location = configuration.GetSection("Location").Get<LocationSettings>() ??
                            new LocationSettings(),
            };

            return settings;
        }

        public static void BuildDirectoryStructure(LocationSettings settings)
        {
            CreateDirectory(settings.TestDefinitionsPath);
            CreateDirectory(settings.ComparisonResultsPath);
            CreateDirectory(settings.ComparisonErrorsPath);
        }

        private static void LoadCustomSettings(IConfigurationBuilder builder)
        {
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var file = $"appsettings.json";

            builder.AddJsonFile(Path.Combine(path, file), true, true);
        }

        private static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}
