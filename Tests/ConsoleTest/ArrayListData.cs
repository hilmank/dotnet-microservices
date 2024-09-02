using System;
using Npgsql;
using Dapper;
namespace ConsoleTest;

public class ArrayListData
{
    readonly string connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=db_salam";
    public List<string> GetList()
    {
        using var connection = new NpgsqlConnection(connectionString);
        // Retrieve all rows
        return connection.Query<string>("SELECT username FROM tb_user WHERE username='112'").ToList();
    }
    public IEnumerable<string> GetEnums()
    {
        using var connection = new NpgsqlConnection(connectionString);
        // Retrieve all rows
        return connection.Query<string>("SELECT username FROM tb_user WHERE username='112'");
    }
}
