namespace Weather.Api.Requests;

public class StacjaMeteosResponse
{
    public required IEnumerable<StacjaMeteoResponse>  Items { get; init; } = Enumerable.Empty<StacjaMeteoResponse>();
  

}
