using System;
using Npgsql;

class Program
{
    static void Main()
    {
        // Test connection string - convert PostgreSQL URI to Npgsql format
        string postgresUri = "postgresql://postgres_user:postgres_password@dgsxinepfct8vokhwt2cl5sm-student-db-1761365366716:5432/student_db";
        string connectionString = ConvertPostgresUriToConnectionString(postgresUri);
        Console.WriteLine("Testing database connection...");
        Console.WriteLine($"PostgreSQL URI: {postgresUri}");
        Console.WriteLine($"Npgsql Connection String: {connectionString}");
        Console.WriteLine();

        // Also test with localhost (if you have local PostgreSQL)
        Console.WriteLine("Testing with localhost connection...");
        string localConnectionString = "Host=localhost;Port=5432;Database=testdb;Username=postgres;Password=password;";
        Console.WriteLine($"Local Connection String: {localConnectionString}");
        Console.WriteLine();

        try
        {
            Console.WriteLine("=== Testing Remote Database ===");
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("✅ Remote database connection successful!");

            // Test a simple query
            using var cmd = new NpgsqlCommand("SELECT version();", connection);
            var version = cmd.ExecuteScalar();
            Console.WriteLine($"PostgreSQL Version: {version}");

            // Check if Users table exists
            using var tableCmd = new NpgsqlCommand(
                "SELECT EXISTS (SELECT FROM information_schema.tables WHERE table_name = 'Users');",
                connection
            );
            var tableExists = (bool)tableCmd.ExecuteScalar();
            Console.WriteLine($"Users table exists: {tableExists}");

            if (tableExists)
            {
                // Count records in Users table
                using var countCmd = new NpgsqlCommand(
                    "SELECT COUNT(*) FROM \"Users\";",
                    connection
                );
                var count = countCmd.ExecuteScalar();
                Console.WriteLine($"Users table record count: {count}");
            }

            // Check migrations table
            using var migrationCmd = new NpgsqlCommand(
                "SELECT EXISTS (SELECT FROM information_schema.tables WHERE table_name = '__EFMigrationsHistory');",
                connection
            );
            var migrationTableExists = (bool)migrationCmd.ExecuteScalar();
            Console.WriteLine($"Migrations table exists: {migrationTableExists}");

            if (migrationTableExists)
            {
                using var migrationListCmd = new NpgsqlCommand(
                    "SELECT \"MigrationId\" FROM \"__EFMigrationsHistory\" ORDER BY \"MigrationId\";",
                    connection
                );
                using var reader = migrationListCmd.ExecuteReader();
                Console.WriteLine("Applied migrations:");
                while (reader.Read())
                {
                    Console.WriteLine($"  - {reader.GetString(0)}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Remote database connection failed: {ex.Message}");
            Console.WriteLine($"Exception Type: {ex.GetType().Name}");
        }

        Console.WriteLine();
        Console.WriteLine("=== Testing Local Database ===");
        try
        {
            using var localConnection = new NpgsqlConnection(localConnectionString);
            localConnection.Open();
            Console.WriteLine("✅ Local database connection successful!");

            // Test a simple query
            using var cmd = new NpgsqlCommand("SELECT version();", localConnection);
            var version = cmd.ExecuteScalar();
            Console.WriteLine($"PostgreSQL Version: {version}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Local database connection failed: {ex.Message}");
            Console.WriteLine($"Exception Type: {ex.GetType().Name}");
            Console.WriteLine("This is expected if you don't have PostgreSQL installed locally.");
        }
    }

    // Helper method to convert PostgreSQL URI to Npgsql connection string
    static string ConvertPostgresUriToConnectionString(string postgresUri)
    {
        try
        {
            var uri = new Uri(postgresUri);
            var userInfo = uri.UserInfo.Split(':');
            var username = userInfo.Length > 0 ? userInfo[0] : "postgres";
            var password = userInfo.Length > 1 ? userInfo[1] : "";
            var host = uri.Host;
            var port = uri.Port > 0 ? uri.Port : 5432;
            var database = uri.AbsolutePath.TrimStart('/');

            return $"Host={host};Port={port};Database={database};Username={username};Password={password};";
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(
                $"Invalid PostgreSQL URI format. Expected: postgresql://user:password@host:port/database. Error: {ex.Message}"
            );
        }
    }
}