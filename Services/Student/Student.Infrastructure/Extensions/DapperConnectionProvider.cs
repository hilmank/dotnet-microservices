using Dapper;
using Npgsql;
using Student.Infrastructure.Settings;

namespace Student.Infrastructure.Extensions;

public class DapperConnectionProvider
{

    public DapperConnectionProvider()
    {
    }

    public static async Task<NpgsqlConnection> ConnectionAsync()
    {
        SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
        return await Task.FromResult(new NpgsqlConnection($"Server={DbSetting.Server};Port={DbSetting.Port};User Id={DbSetting.UserId};Password={DbSetting.Password};Database={DbSetting.Database};CommandTimeout={DbSetting.CommandTimeout};Pooling=false"));
    }
}
