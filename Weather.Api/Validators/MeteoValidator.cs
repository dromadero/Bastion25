using FluentValidation;
using Weather.Api.Models;
using Weather.Api.Repositories;

namespace Weather.Api.Validators;

public class MeteoValidator : AbstractValidator<StacjaMeteo>
{

    private readonly IMeteoRepository _meteoRepository;

    public MeteoValidator(IMeteoRepository meteoRepository)
    {
        _meteoRepository = meteoRepository;


        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.BusinessId)
            .NotEmpty();

        RuleFor(x => x.SumaOpadu)
            .NotEmpty();

    }

}
