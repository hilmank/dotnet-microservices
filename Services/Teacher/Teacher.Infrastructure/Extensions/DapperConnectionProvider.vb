Imports Dapper
Imports Npgsql
Imports Teacher.Application
Public Module DapperConnectionProvider
    Public Async Function ConnectionAsync() As Task(Of NpgsqlConnection)
        SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL)
        Return Await Task.FromResult(New NpgsqlConnection(DatabaseSettings.ConnectionString))
    End Function
End Module
