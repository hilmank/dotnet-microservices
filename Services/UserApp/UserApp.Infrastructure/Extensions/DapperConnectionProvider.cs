using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using UserApp.Application.Settings;

namespace UserApp.Infrastructure.Extensions;

public static class DapperConnectionProvider
{
    public static async Task<NpgsqlConnection> ConnectionAsync()
    {
        SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
        return await Task.FromResult(new NpgsqlConnection(DatabaseSettings.ConnectionString));
    }
}
