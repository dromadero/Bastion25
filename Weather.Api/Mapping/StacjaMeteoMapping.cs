using Weather.Api.Models;
using Weather.Api.Requests;

namespace Weather.Api.Mapping;

public static class StacjaMeteoMapping
{

    public static StacjaMeteo MapToStacjaMeteo(this CreateStacjaMeteoRequest request)
    {
        return new StacjaMeteo
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            BusinessId = request.Id,
            DataPomiaru = request.DataPomiaru,
            GodzinaPomiaru = request.GodzinaPomiaru,
            Temperatura = request.Temperatura,
            PredkoscWiatru = request.PredkoscWiatru,
            KierunekWiatru = request.KierunekWiatru,
            WilgotnoscWzgledna = request.WilgotnoscWzgledna,
            SumaOpadu = request.SumaOpadu,
            Cisnienie = request.Cisnienie
        };
    }

    public static StacjaMeteo MapToStacjaMeteo(this UpdateStacjaMeteoRequest request, Guid id)
    {
        return new StacjaMeteo
        {
            Id = id,
            Name = request.Name,
            BusinessId = request.IdStacji,
            DataPomiaru = request.DataPomiaru,
            GodzinaPomiaru = request.GodzinaPomiaru,
            Temperatura = request.Temperatura,
            PredkoscWiatru = request.PredkoscWiatru,
            KierunekWiatru = request.KierunekWiatru,
            WilgotnoscWzgledna = request.WilgotnoscWzgledna,
            SumaOpadu = request.SumaOpadu,
            Cisnienie = request.Cisnienie
        };
    }



    public static StacjaMeteoResponse MapToResponse(this StacjaMeteo model)
    {
        return new StacjaMeteoResponse
        {
            Id = model.Id,
            Name = model.Name,
            IdStacji = model.BusinessId,
            DataPomiaru = model.DataPomiaru,
            GodzinaPomiaru = model.GodzinaPomiaru,
            Temperatura = model.Temperatura,
            PredkoscWiatru = model.PredkoscWiatru,
            KierunekWiatru = model.KierunekWiatru,
            WilgotnoscWzgledna = model.WilgotnoscWzgledna,
            SumaOpadu = model.SumaOpadu,
            Cisnienie = model.Cisnienie,
            Slug = model.Slug,            
        };
    }

    public static StacjaMeteosResponse MapToResponse(this IEnumerable<StacjaMeteo> items)
    {
        return new StacjaMeteosResponse
        {
            Items = items.Select(MapToResponse)
        };
    }


}