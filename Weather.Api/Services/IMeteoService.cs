using Weather.Api.Models;

namespace Weather.Api.Services;

public interface IMeteoService
{
    Task<bool> CreateAsync(StacjaMeteo item, CancellationToken token = default);

    Task<StacjaMeteo?> GetByIdAsync(Guid id, CancellationToken token = default);

    Task<StacjaMeteo?> GetByBusinessIdAsync(int id, CancellationToken token = default);

    Task<IEnumerable<StacjaMeteo>> GetAllAsync(CancellationToken token = default);

    Task<StacjaMeteo?> UpdateAsync(StacjaMeteo item, CancellationToken token = default);

    Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default);
}
