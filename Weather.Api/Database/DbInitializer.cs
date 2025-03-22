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
            create table if not exists meteos (
            Id UUID primary key,
            BusinessId integer not null,
            Name TEXT not null,
            DataPomiaru date not null,
            GodzinaPomiaru integer not null,
            Temperatura integer not null,
            PredkoscWiatru integer not null,
            KierunekWiatru integer not null,
            WilgotnoscWzgledna decimal not null,
            SumaOpadu decimal not null,
            Cisnienie decimal not null 
            )
            """);

        await connection.ExecuteAsync("""
            create unique index concurrently if not exists meteos_BusinessId_idx
            on meteos 
            using btree(BusinessId)
            """);

    }

}
