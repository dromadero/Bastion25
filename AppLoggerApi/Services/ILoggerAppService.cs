using AppLoggerApi.Model;

namespace AppLoggerApi.Services;

public interface ILoggerAppService
{
    public Task<bool> CreateAsync(LogItem item, CancellationToken token);

    public Task<IEnumerable<LogItem>> GetAllAsync();

    public Task<IEnumerable<string>> GetAllApps();

    public Task<IEnumerable<LogItem>> GetByAppIdAsync(string searchTerm); 

    public Task<bool> DeleteAsync(int id);
}
