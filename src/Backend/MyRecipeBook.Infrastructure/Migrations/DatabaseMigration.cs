using Dapper;
using MyRecipeBook.Domain.Enums;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Infrastructure.Migrations
{
    public static class DatabaseMigration
    {
        public static void Migrate(DatabaseType databaseType, string connectionString)
        {
            EnsureDatabaseCreated_MySql(connectionString); 
        }
        private static void EnsureDatabaseCreated_MySql(string connectionString)
        {
            var connectionStringBuilder = new MySqlConnectionStringBuilder(connectionString);

            var databaseName = connectionStringBuilder.Database;

            connectionStringBuilder.Remove("Database");

            using var dbConnection = new MySqlConnection(connectionStringBuilder.ConnectionString);

            var parameters = new DynamicParameters();

            parameters.Add("name", databaseName);

            var records = dbConnection.Query("SELECT * FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @name", parameters);
            if (records.Any() == false)
            
                dbConnection.Execute($"CREATE DATABASE {databaseName}");
            
        }
    }
}
