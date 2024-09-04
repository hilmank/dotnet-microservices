using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Student.Application.Settings;

namespace Student.Infrastructure.Extensions;

public static class DbExtension
{
    public static IHost MigrateDatabase<TContext>(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<TContext>>();
            try
            {
                logger.LogInformation("User DB Migration Started");
                ApplyMigrations();
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
    private static void ApplyMigrations()
    {
        using var connection = new NpgsqlConnection(DatabaseSettings.ConnectionString);
        connection.Open();
        using var tx = connection.BeginTransaction();
        //initial database
        string sql = "SELECT schema_name FROM information_schema.schemata WHERE schema_name = 'std';";
        var result = connection.QueryFirstOrDefault<string>(sql);
        if (result is null)
        {
            var initDb = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DbSql", "std-init-db.sql");
            string sqlInit = File.ReadAllText(initDb);
            connection.ExecuteScalar(sqlInit);
        }
        //update database
        string[] files = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DbSql"), "std-updatedb*.sql");
        foreach (string file in files)
        {
            sql = $"SELECT filename FROM database_change_log WHERE filename = '{Path.GetFileName(file)}'";
            result = connection.QueryFirstOrDefault<string>(sql);
            if (result is null)
            {
                string sqlUpdate = File.ReadAllText(file);
                connection.ExecuteScalar(sqlUpdate);
            }
        }
        tx.Commit();
    }
}

