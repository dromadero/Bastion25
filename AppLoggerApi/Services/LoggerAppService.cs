using AppLoggerApi.Data;
using AppLoggerApi.Model;
using Dapper;

namespace AppLoggerApi.Services;

public class LoggerAppService : ILoggerAppService
{

    private readonly IDbConnectionFactory _connectionFactory;

    public LoggerAppService(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<bool> CreateAsync(LogItem item, CancellationToken token = default)
    {
        //var existingBook = await GetByIsbnAsync(book.Isbn);
        //if (existingBook is not null)
        //{
        //    return false;
        //}
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(new CommandDefinition(
            """
            INSERT INTO AppLogs (BusinessId, Message, CreatedDate) 
            VALUES (@BusinessId, @Message, @CreatedDate)
            """,
            new { item.BusinessId, item.Message, item.CreatedDate }, cancellationToken: token));
        return result > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync("DELETE FROM AppLogs WHERE Id = @Id", new {Id = id }  );
        return result > 0;
    }

    public async Task<IEnumerable<string>> GetAllApps()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        return await connection.QueryAsync<string>(new CommandDefinition(
            """
            SELECT DISTINCT BusinessId FROM AppLogs
            ORDER BY BusinessId ASC;
            """
            ));
    }

    public async Task<IEnumerable<LogItem>> GetAllAsync()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        return await connection.QueryAsync<LogItem>(new CommandDefinition(
            """
            SELECT * FROM AppLogs
            ORDER BY CreatedDate DESC;
            """
            ));
    }

    public async Task<IEnumerable<LogItem>> GetByAppIdAsync(string appBusinnessId)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        return await connection.QueryAsync<LogItem>(new CommandDefinition(
            """
            SELECT * FROM AppLogs WHERE BusinessId LIKE '%' || @SearchTerm || '%'
            """, new { SearchTerm = appBusinnessId }));
    }
}
