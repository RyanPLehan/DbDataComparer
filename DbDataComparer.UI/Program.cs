using DbDataComparer.Domain.Configuration;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;

namespace DbDataComparer.UI
{
    internal static class Program
    {
        private static ConfigurationSettings Settings;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            InitializeSettings();


            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Main(Settings));
        }

        private static void InitializeSettings()
        {
            IConfiguration configuration = Initialize.LoadConfiguration();
            Settings = Initialize.GetConfigurationSettings(configuration);
            Initialize.BuildDirectoryStructure(Settings.Location);                      // Make sure Directories exist
        }

    }
}