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
                Notification = configuration.GetSection("Notification").Get<NotificationSettings>() ??
                            new NotificationSettings(),
                Log = configuration.GetSection("Log").Get<LogSettings>() ??
                            new LogSettings(),
            };

            return settings;
        }

        public static void BuildDirectoryStructure(LocationSettings settings)
        {
            ApplicationIO.CreateDirectory(ApplicationIO.GetTestDefinitionPath(settings));
            ApplicationIO.CreateDirectory(ApplicationIO.GetComparisonResultPath(settings));
            ApplicationIO.CreateDirectory(ApplicationIO.GetComparisonErrorPath(settings));
        }

        private static void LoadCustomSettings(IConfigurationBuilder builder)
        {
            var path = ApplicationIO.GetExecutablePath();
            var file = $"appsettings.json";

            builder.AddJsonFile(Path.Combine(path, file), true, true);
        }

    }
}
