using Microsoft.AspNetCore.Connections;
using Dapper;
using System.Formats.Asn1;

namespace AppLoggerApi.Data;

public class DatabaseInitializer
{
    private readonly IDbConnectionFactory _connectionFactory;

    public DatabaseInitializer(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task InitializeAsync()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        await connection.ExecuteAsync(
            @"CREATE TABLE IF NOT EXISTS AppLogs (
            Id INTEGER PRIMARY KEY NOT NULL,
            BusinessId TEXT NOT NULL,
            Message TEXT NOT NULL,
            CreatedDate TEXT NOT NULL)
            "

            //"ALTER TABLE AppLogs ADD COLUMN Id BIGSERIAL PRIMARY KEY;"
            );
    }
}
 