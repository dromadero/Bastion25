
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

    public async Task<bool> CreateAsync(StacjaMeteo item)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        using var transaction = connection.BeginTransaction();

        var result = await connection.ExecuteAsync(new CommandDefinition("""
            insert into stacjameteo(id)
            values (@Id)
            """, item));


        if(result > 0)
        {

        }
        transaction.Commit();

        return result > 0;

    }

    public Task<bool> DeleteByIdAsync(Guid id)
    {
        var counter = _items.RemoveAll(x => x.Id == id);
        var result = counter > 0;
        return Task.FromResult(result);
    }

    public Task<bool> ExistsById(Guid item)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<StacjaMeteo>> GetAllAsync()
    {
        return Task.FromResult(_items.AsEnumerable());
    }

    public Task<StacjaMeteo> GetByIdAsync(Guid id)
    {
        var item = _items.SingleOrDefault(x => x.Id == id);
        return Task.FromResult(item);
    }

    public Task<bool> UpdateAsync(StacjaMeteo item)
    {
        var itemIndex = _items.FindIndex(x => x.Id == item.Id);
        if (itemIndex == -1)
        {
            return Task.FromResult(false);
        }
        _items[itemIndex] = item;
        return Task.FromResult(true);
    }
}
