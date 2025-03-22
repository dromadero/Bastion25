
using System.Xml.Linq;
using Dapper;
using Weather.Api.Database;
using Weather.Api.Models;

namespace Weather.Api.Repositories;

public class MeteoRepository : IMeteoRepository
{
    private readonly List<StacjaMeteo> _items = new();
    private readonly IDbConnectionFactory _connectionFactory;

    public MeteoRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _connectionFactory = dbConnectionFactory;
    }

    public async Task<bool> CreateAsync(StacjaMeteo item, CancellationToken token = default)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();

        var result = await connection.ExecuteAsync(new CommandDefinition("""
            insert into meteos(Id, BusinessId, Name, DataPomiaru, GodzinaPomiaru, Temperatura, PredkoscWiatru, KierunekWiatru, WilgotnoscWzgledna, SumaOpadu, Cisnienie )
            values (@Id,@BusinessId,@Name,@DataPomiaru,@GodzinaPomiaru,@Temperatura,@PredkoscWiatru,@KierunekWiatru,@WilgotnoscWzgledna,@SumaOpadu,@Cisnienie)
            """, item, cancellationToken: token));


        if (result > 0)
        {

        }
        transaction.Commit();

        return result > 0;

    }

    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();

        var result = await connection.ExecuteAsync(new CommandDefinition("""
            delete from meteos where id = @id
            """, new { id }, cancellationToken: token)
         );

        transaction.Commit();
        return result > 0;
        //var counter = _items.RemoveAll(x => x.Id == id);
        //var result = counter > 0;
        //return Task.FromResult(result);
    }

    public async Task<bool> ExistsByIdAsync(Guid? id, int? businessId, CancellationToken token = default)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync(token);
        return await connection.ExecuteScalarAsync<bool>(
            new CommandDefinition("""
                select count(1) from meteos where id = @id or BusinessId = @businessId
                """, new { id, businessId }, cancellationToken: token)
            );
    }


    public async Task<IEnumerable<StacjaMeteo>> GetAllAsync(CancellationToken token = default)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync(token);
        var items = await connection.QueryAsync<StacjaMeteo>(
             new CommandDefinition("""
                select * from meteos
                """, cancellationToken: token)
            );

        return items;
    }

    public async Task<StacjaMeteo> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync(token);
        var item = await connection.QuerySingleOrDefaultAsync<StacjaMeteo>(
            new CommandDefinition("""
                select * from meteos where id = @id
                """, new { id }, cancellationToken: token)
            );

        if (item is null)
        {
            return null;
        }

        //var item = _items.SingleOrDefault(x => x.Id == id);
        return item;
    }

    public async Task<StacjaMeteo> GetByBusinessIdAsync(int id, CancellationToken token = default)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync(token);
        var item = await connection.QuerySingleOrDefaultAsync<StacjaMeteo>(
            new CommandDefinition("""
                select * from meteos where BusinessId = @id
                """, new { id }, cancellationToken: token)
            );

        if (item is null)
        {
            return null;
        }

        return item;
    }

    public async Task<bool> UpdateAsync(StacjaMeteo item, CancellationToken token = default)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync(token);
        using var transaction = connection.BeginTransaction();

        var result = await connection.ExecuteAsync(
            new CommandDefinition("""
                update meteos set 
                BusinessId = @BusinessId,
                Name = @Name,
                DataPomiaru = @DataPomiaru,
                GodzinaPomiaru = @GodzinaPomiaru,
                Temperatura = @Temperatura,
                PredkoscWiatru = @PredkoscWiatru,
                KierunekWiatru = @KierunekWiatru,
                WilgotnoscWzgledna = @WilgotnoscWzgledna,
                SumaOpadu = @SumaOpadu,
                Cisnienie = @Cisnienie
                where id = @id
                """, item, cancellationToken: token)
            );

        transaction.Commit();
        return result > 0;
        //var itemIndex = _items.FindIndex(x => x.Id == item.Id);
        //if (itemIndex == -1)
        //{
        //    return Task.FromResult(false);
        //}
        //_items[itemIndex] = item;
        //return Task.FromResult(true);
    }
}
