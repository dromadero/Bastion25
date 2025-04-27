
using FxApi.Data;
using FxApi.DataSamples;
using FxApi.Model.Fcs;
using Microsoft.AspNetCore.WebUtilities;
using System.Buffers.Text;
using System.Text.Json;

namespace FxApi.Services;

public class FcsService : IFcsService
{
    IMarketContext _marketContext;

    public FcsService(IMarketContext marketContext)
    {
        _marketContext = marketContext;
    }


    public async Task Run()
    {

        string baseUrl = "https://fcsapi.com";


        HttpClient httpClient = new HttpClient();
        httpClient.Timeout = TimeSpan.FromSeconds(20);
        httpClient.BaseAddress = new Uri(baseUrl);
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

        var queryParams = new Dictionary<string, string?>
            {
                { "period", "1h" },
                { "access_key", "ANzQKlhhTwE54JivUK9JeqZtS5HKsb" },
                { "id", "1"}
            };


        string ApiURL_WithQuery = QueryHelpers.AddQueryString($"{baseUrl}/api-v3/forex/history", queryParams);

        //var response = await httpClient.GetAsync(ApiURL_WithQuery);
        //if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
        //{
        //string responseJsonString = await response.Content.ReadAsStringAsync();
        string responseJsonString = FcsDataSeed.HistoryEurUsd1h;

        var data = JsonSerializer.Deserialize<Root>(responseJsonString);

 
        _marketContext.AddRange(data.info.symbol, data.info.period, data.response);


        httpClient.Dispose();
        //}
        //else
        //{
        //    throw new Exception("IsSuccessStatusCode false");
        //}


    }
}
