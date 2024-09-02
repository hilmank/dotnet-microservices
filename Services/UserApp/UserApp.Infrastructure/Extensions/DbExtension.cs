using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace UserApp.Infrastructure.Extensions;

public static class DbExtension
{
    public static IHost MigrateDatabase<TContext>(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var config = services.GetRequiredService<IConfiguration>();
            var logger = services.GetRequiredService<ILogger<TContext>>();
            try
            {
                logger.LogInformation("User DB Migration Started");
                ApplyMigrations(config);
                logger.LogInformation("User DB Migration Completed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrating the database.");
                throw;
            }
        }
        return host;
    }

    private static void ApplyMigrations(IConfiguration config)
    {
        var retry = 5;
        while (retry > 0)
        {
            try
            {
                using var connection = new NpgsqlConnection(config.GetValue<string>("DatabaseSettings:ConnectionString"));
                connection.Open();
                using var cmd = new NpgsqlCommand
                {
                    Connection = connection
                };
                // Check if the Coupon table exists
                using var checkCmd = new NpgsqlCommand
                {
                    Connection = connection,
                    CommandText = "SELECT schema_name FROM information_schema.schemata WHERE schema_name = 'usr';"
                };

                var result = checkCmd.ExecuteScalar();
                if (result != DBNull.Value && result != null)
                {
                    // The table exists, so skip migration
                    Console.WriteLine("usr schema already exists, skipping migration.");
                    return;
                }
                var relativePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DbSql", "initDb.sql");
                cmd.CommandText = File.ReadAllText(relativePath);
                cmd.ExecuteNonQuery();
                // Exit loop if successful
                break;
            }
            catch (Exception)
            {
                retry--;
                if (retry == 0)
                {
                    throw;
                }
                //Wait for 2 seconds
                Thread.Sleep(2000);
            }
        }
    }
}

