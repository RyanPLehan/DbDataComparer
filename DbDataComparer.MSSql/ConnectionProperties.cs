using System;
using System.ComponentModel;
using System.Data.Common;
using Microsoft.Data.Sql;
using Microsoft.Data.SqlClient;
using DbDataComparer.Domain;
using DbDataComparer.Domain.Models;

namespace DbDataComparer.MSSql
{
    internal class ConnectionProperties : IConnectionProperties
    {
        private const int SqlError_CannotOpenDatabase = 4060;
        private const string SqlError_CannotTestNonExistentDatabase = "This connection cannot be tested because the specified database does not exist or is not visible to the specified user.";
        private const string SqlError_UnsupportedSqlVersion = "This server version is not supported.  You must have Microsoft SQL Server 2005 or later.";

        private readonly DataProvider SqlDataProvider;
        private readonly SqlConnectionStringBuilder SqlConnectionStringBuilder;


        public ConnectionProperties(DataProvider dataProvider,
                                    SqlConnectionStringBuilder sqlConnectionStringBuilder)
        {
            this.SqlDataProvider = dataProvider ??
                throw new ArgumentNullException(nameof(dataProvider));

            this.SqlConnectionStringBuilder = sqlConnectionStringBuilder ??
                throw new ArgumentNullException(nameof(sqlConnectionStringBuilder));
        }

        public string ConnectionString { get => this.SqlConnectionStringBuilder.ConnectionString; }

        public DataProvider DataProvider 
        {
            get => new DataProvider()
            {
                Name = this.SqlDataProvider.Name,
                DisplayName = this.SqlDataProvider.DisplayName,
                ShortDisplayName = this.SqlDataProvider.ShortDisplayName,
                ConnectionType = this.SqlDataProvider.ConnectionType,
            };
        }

        public ConnectionBuilderOptions ConnectionBuilderOptions
        {
            get => new ConnectionBuilderOptions()
            {
                Server = this.SqlConnectionStringBuilder.DataSource,
                Database = this.SqlConnectionStringBuilder.InitialCatalog,
                UseWindowsAuthentication = this.SqlConnectionStringBuilder.IntegratedSecurity,
                UserId = this.SqlConnectionStringBuilder.UserID,
                Password = this.SqlConnectionStringBuilder.Password,
            };
        }

        public void Test()
        {
            // Try to open it
            var connection = new SqlConnection(CreateTestConnectionString());
            try
            {
                connection.Open();
                Inspect(connection);
            }

            catch (SqlException e)
            {
                if (e.Number == SqlError_CannotOpenDatabase)
                    throw new InvalidOperationException(SqlError_CannotTestNonExistentDatabase);
                else
                    throw;
            }

            finally
            {
                connection.Dispose();
            }
        }


        public async Task TestAsync()
        {
            // Try to open it
            var connection = new SqlConnection(CreateTestConnectionString());
            try
            {
                await connection.OpenAsync();
                Inspect(connection);
            }

            catch (SqlException e)
            {
                if (e.Number == SqlError_CannotOpenDatabase)
                    throw new InvalidOperationException(SqlError_CannotTestNonExistentDatabase);
                else
                    throw;
            }

            finally
            {
                await connection.DisposeAsync();
            }
        }


        private string CreateTestConnectionString()
        {
            // Looking for Pooling, temporarily remove it when building the connection string
            bool savedPooling = this.SqlConnectionStringBuilder.Pooling;
            this.SqlConnectionStringBuilder.Pooling = false;
            var testConnectionString = this.SqlConnectionStringBuilder.ConnectionString;
            this.SqlConnectionStringBuilder.Pooling = savedPooling;

            return testConnectionString;
        }

        private void Inspect(SqlConnection connection)
        {
            if (connection.ServerVersion.StartsWith("07", StringComparison.Ordinal) ||
                connection.ServerVersion.StartsWith("08", StringComparison.Ordinal))
            {
                throw new NotSupportedException(SqlError_UnsupportedSqlVersion);
            }
        }
    }

}
