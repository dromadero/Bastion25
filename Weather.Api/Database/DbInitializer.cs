using System.Data.Common;
using Dapper;

namespace Weather.Api.Database;

public class DbInitializer
{

    private readonly IDbConnectionFactory _connectionFactory;


    public DbInitializer(IDbConnectionFactory dbConnectionFactory)
    {
        _connectionFactory = dbConnectionFactory;

    }


    public async Task InitializeAsync()
    {
        using var connection  = await _connectionFactory.CreateConnectionAsync();
        await connection.ExecuteAsync("""
            create table if not exists stacjaMeteos (
            id UUID primary key,
            ll TEXT not null,
            dd TEXT not null,
            dfkjrk integer not null
            )
            """);

        await connection.ExecuteAsync("""
            create unique index concurrently if not exists meteo_idx
            on stacjaMeteos 
            using btree(ll)
            """);

    }

}
