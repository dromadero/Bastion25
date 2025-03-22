namespace Weather.Api.Requests;

public class CreateStacjaMeteoRequest
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required DateTime DataPomiaru { get; init; }
    public required int GodzinaPomiaru { get; init; }
    public required int Temperatura { get; init; }
    public required int PredkoscWiatru { get; init; }
    public required int KierunekWiatru { get; init; }
    public required Single WilgotnoscWzgledna { get; init; }
    public required Single SumaOpadu { get; init; }
    public required Single Cisnienie { get; init; }

}
