using System;

namespace DbDataComparer.Domain.Models
{
    public class ConnectionBuilderOptions
    {
        public string Server { get; set; }                          // Server
        public string Database { get; set; }                        // Database

        public bool UseWindowsAuthentication { get; set; } = true;  // Use Windows Auth
        public string UserId { get; set; }                          // Use User/Password Auth
        public string Password { get; set; }
    }
}
