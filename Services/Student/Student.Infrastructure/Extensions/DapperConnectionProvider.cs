using Dapper;
using Npgsql;
using Student.Application.Settings;

namespace Student.Infrastructure.Extensions;

public static class DapperConnectionProvider
{
    public static async Task<NpgsqlConnection> ConnectionAsync()
    {
        SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
        return await Task.FromResult(new NpgsqlConnection(DatabaseSettings.ConnectionString));
    }
}
