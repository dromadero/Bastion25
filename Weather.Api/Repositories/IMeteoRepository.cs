﻿using Weather.Api.Models;

namespace Weather.Api.Repositories;

public interface IMeteoRepository
{

    Task<bool> CreateAsync(StacjaMeteo item, CancellationToken token = default);

    Task<StacjaMeteo?> GetByIdAsync(Guid id, CancellationToken token = default);

    Task<StacjaMeteo?> GetByBusinessIdAsync(int id, CancellationToken token = default);

    Task<IEnumerable<StacjaMeteo>> GetAllAsync(CancellationToken token = default);

    Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default);

    Task<bool> UpdateAsync(StacjaMeteo item, CancellationToken token = default);

    Task<bool> ExistsByIdAsync(Guid? id, int? businessId, CancellationToken token = default);
}
