using System;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using DbDataComparer.Domain;
using DbDataComparer.Domain.Models;


namespace DbDataComparer.MSSql
{
    public class ConnectionPropertiesBuilder
    {
        public static IConnectionProperties Build(ConnectionBuilderOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            ValidateOptions(options);
            return new ConnectionProperties(CreateConnectionStringBuilder(options));
        }

        public static IEnumerable<ConnectionDataSource> EnumerateServers()
        {
            IList<ConnectionDataSource> dataSources = new List<ConnectionDataSource>();

            DataTable dataTable = Microsoft.Data.Sql.SqlDataSourceEnumerator.Instance.GetDataSources();

            // Create the object array of server names (with instances appended)
            int rowCnt = dataTable.Rows.Count;
            for (int i = 0; i < rowCnt; i++)
            {
                ConnectionDataSource dataSource = new ConnectionDataSource()
                {
                    ServerVersion = dataTable.Rows[i]["Version"].ToString(),
                };

                string name = dataTable.Rows[i]["ServerName"].ToString();
                string instance = dataTable.Rows[i]["InstanceName"].ToString();

                if (String.IsNullOrWhiteSpace(instance))
                    dataSource.ServerName = name;
                else
                    dataSource.ServerName = String.Format(@"{0}\{1}", name, instance);

                dataSources.Add(dataSource);
            }

            return dataSources.OrderBy(x => x.ServerName);
        }


        public static IEnumerable<string> EnumerateDatabases(ConnectionBuilderOptions options)
        {
            ValidateMinimalOptions(options);
            IList<string> databases = new List<string>();
            SqlConnectionStringBuilder connStrBuilder = CreateConnectionStringBuilder(options);
            SqlConnection connection = new SqlConnection(connStrBuilder.ConnectionString);
            SqlCommand command = connection.CreateCommand();
            SqlDataReader reader = null;

            try
            {
                connection.Open();

                // SQL AZure doesn't support HAS_DBACCESS at this moment.
                // Change the command text to get database names accordingly
                string sql;
                if (IsAzureServer(connection))
                    sql = "SELECT name FROM master.dbo.sysdatabases ORDER BY name";
                else
                    sql = "SELECT name FROM master.dbo.sysdatabases WHERE HAS_DBACCESS(name) = 1 ORDER BY name";

                command.CommandText = sql;

                // Execute the command
                reader = command.ExecuteReader();
                while (reader.Read())
                    databases.Add(reader.GetString(0));
            }
            finally
            {
                if (reader != null)
                    reader.Dispose();

                if (connection != null)
                    connection.Dispose();
            }

            return databases;
        }


        public static IConnectionProperties Parse(string connectionString)
        {
            if (String.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException(nameof(connectionString));

            return new ConnectionProperties(CreateConnectionStringBuilder(connectionString));
        }


        private static void ValidateOptions(ConnectionBuilderOptions options)
        {
            ValidateMinimalOptions(options);

            if (String.IsNullOrWhiteSpace(options.Database))
                throw new ArgumentException("Must supply Database name");
        }

        private static void ValidateMinimalOptions(ConnectionBuilderOptions options)
        {
            if (String.IsNullOrWhiteSpace(options.Server))
                throw new ArgumentException("Must supply Data Source / Server");

            if (!options.UseWindowsAuthentication &&
                (String.IsNullOrWhiteSpace(options.UserId) || String.IsNullOrWhiteSpace(options.Password)))
                throw new ArgumentException("Must either enable Windows authentication or supply Sql Server credentials");
        }

        private static SqlConnectionStringBuilder CreateConnectionStringBuilder(ConnectionBuilderOptions options)
        {
            var connStrBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = options.Server,
                InitialCatalog = options.Database,
                IntegratedSecurity = options.UseWindowsAuthentication,
                ApplicationName = ConnectionPropertiesBuilder.ApplicationName,
                TrustServerCertificate = true,
            };

            if (!options.UseWindowsAuthentication)
            {
                connStrBuilder.UserID = options.UserId;
                connStrBuilder.Password = options.Password;
            }

            return connStrBuilder;
        }


        private static bool IsAzureServer(SqlConnection connection)
        {
            // Create a command to check if the database is on SQL AZure.
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT CASE WHEN SERVERPROPERTY(N'EDITION') = 'SQL Data Services' OR SERVERPROPERTY(N'EDITION') = 'SQL Azure' THEN 1 ELSE 0 END";
            return (Int32)(command.ExecuteScalar()) == 1;
        }

        private static SqlConnectionStringBuilder CreateConnectionStringBuilder(string connectionString)
        {
            return new SqlConnectionStringBuilder(connectionString);
        }


        private static string ApplicationName { get => "DbDataComparer"; }
    }
}
