using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DbDataComparer.Domain.Configuration;
using System.Security.AccessControl;
using System.Security.Principal;

namespace DbDataComparer.Domain
{
    public static class ApplicationIO
    {
        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                DirectoryInfo dInfo = new DirectoryInfo(path);
                DirectorySecurity dSecurity = dInfo.GetAccessControl();
                dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                dInfo.SetAccessControl(dSecurity);
            }
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
