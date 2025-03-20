using System.Text.RegularExpressions;

namespace Weather.Api.Models;

public partial class  StacjaMeteo
{
    public required Guid Id { get; init; }
    public required int BusinessId { get; init; }
    public required string Name { get; set; }
    public required DateOnly DataPomiaru { get; set; }
    public required int GodzinaPomiaru { get; set; }
    public required int Temperatura { get; set; }
    public required int PredkoscWiatru { get; set; }
    public required int KierunekWiatru { get; set; }
    public required Single WilgotnoscWzgledna { get; set; }
    public required Single SumaOpadu { get; set; }
    public required Single Cisnienie { get; set; }

    public string Slug => GenerateSlug();


    private string GenerateSlug()
    {
        var slugValue = SlugRegex().Replace(Name, string.Empty).ToLower().Replace(" ", "-");
        return $"{slugValue}-{BusinessId}";
    }


    [GeneratedRegex("[^0-9A-Za-z _-]", RegexOptions.NonBacktracking, 5)]
    private static partial Regex SlugRegex();

}
