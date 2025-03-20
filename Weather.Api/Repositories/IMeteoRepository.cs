using Weather.Api.Models;

namespace Weather.Api.Repositories;

public interface IMeteoRepository
{

    Task<bool> CreateAsync(StacjaMeteo item);

    Task<StacjaMeteo?> GetByIdAsync(Guid id);

    Task<IEnumerable<StacjaMeteo>> GetAllAsync();

    Task<bool> DeleteByIdAsync(Guid id);

    Task<bool> UpdateAsync(StacjaMeteo item);

    Task<bool> ExistsById(Guid item);
}
