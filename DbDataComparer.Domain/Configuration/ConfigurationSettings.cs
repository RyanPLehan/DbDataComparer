using System;

namespace DbDataComparer.Domain.Configuration
{
    public class ConfigurationSettings
    {
        public LocationSettings Location { get; set; }
        public DatabaseSettings Database { get; set; }
        public NotificationSettings Notification { get; set; }
        public LogSettings Log { get; set; }
    }
}
