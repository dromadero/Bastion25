using FluentValidation;
using Weather.Api.Models;
using Weather.Api.Repositories;

namespace Weather.Api.Services;

public class MeteoService : IMeteoService
{
    private readonly IMeteoRepository _meteoRepository;
    private readonly IValidator<StacjaMeteo> _validator;

    public MeteoService(IMeteoRepository meteoRepository, IValidator<StacjaMeteo> validator)
    {
        _meteoRepository = meteoRepository;
        _validator = validator;
    }

    public async Task<bool> CreateAsync(StacjaMeteo item, CancellationToken token = default)
    {
        await _validator.ValidateAndThrowAsync(item, token);
        return await _meteoRepository.CreateAsync(item, token);
    }

    public Task<StacjaMeteo?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return _meteoRepository.GetByIdAsync(id, token);
    }

    public Task<StacjaMeteo?> GetByBusinessIdAsync(int id, CancellationToken token = default)
    {
        return _meteoRepository.GetByBusinessIdAsync(id, token);
    }

    public Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
    {
        return _meteoRepository.DeleteByIdAsync(id, token);
    }

    public Task<IEnumerable<StacjaMeteo>> GetAllAsync(CancellationToken token = default)
    {
        return _meteoRepository.GetAllAsync(token);
    }

    public async Task<StacjaMeteo?> UpdateAsync(StacjaMeteo item, CancellationToken token = default)
    {
        await _validator.ValidateAndThrowAsync(item, token);

        var itemExists = await _meteoRepository.ExistsByIdAsync(item.Id, item.BusinessId, token);

        if (!itemExists)
        {
            return null;
        }

        await _meteoRepository.UpdateAsync(item, token);
        return item;
    }
}
