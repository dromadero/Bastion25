
using FxApi.Data;
using FxApi.DataSamples;
using FxApi.Mapping;
using FxApi.Model;
using FxApi.Model.Fcs;
using Microsoft.AspNetCore.WebUtilities;
using System.Buffers.Text;
using System.Data.SqlTypes;
using System.Net.Http;
using System.Text.Json;

namespace FxApi.Services;

public class FcsService : IFcsService
{
    private IMarketContext _marketContext;
    private HttpClient _httpClient;
    private readonly string _accessKey = "ANzQKlhhTwE54JivUK9JeqZtS5HKsb";

    string _baseUrl = "https://fcsapi.com";

    public FcsService(IMarketContext marketContext)
    {
        _marketContext = marketContext;
        _httpClient = new HttpClient();
        InitServices();
    }

    private void InitServices()
    {
        _httpClient.Timeout = TimeSpan.FromSeconds(20);
        _httpClient.BaseAddress = new Uri(_baseUrl);
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }

    public async Task TryCollectData(string symbol, string period)
    {
        if (await IsHistoryRequired(symbol, period))
        {
            await AddHistory(symbol, period);
        }
        await AddCurrentData(symbol, period);
    }

    public async Task<List<Candle>> Get(string symbol, string period)
    {
        return await _marketContext.GetAsync(symbol, period);
    }

    private async Task AddHistory(string symbol, string period)
    {
        var queryParams = new Dictionary<string, string?>
            {
                { "period", "1h" },
                { "access_key", _accessKey },
                { "id", "1"}
            };

        string ApiURL_WithQuery = QueryHelpers.AddQueryString($"{_baseUrl}/api-v3/forex/history", queryParams);

        //var response = await httpClient.GetAsync(ApiURL_WithQuery);
        //if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
        //{
        //string responseJsonString = await response.Content.ReadAsStringAsync();
        string responseJsonString = FcsDataSeed.HistoryEurUsd1h;

        var data = JsonSerializer.Deserialize<HistoryData>(responseJsonString);

        if (data is not null && data.response.Count > 0)
        {
            var list = data.response.Values.ToList();
            await _marketContext.AddRange(symbol, period, list.MapToCandleList());
        }
    }

    private async Task AddCurrentData(string symbol, string period)
    {

        var queryParams = new Dictionary<string, string?>
            {
                { "period", GetPeriodValue(period) },
                { "access_key", _accessKey },
                { "id", GetSymbolValue(symbol)}
            };
        string apiUrl_WithQuery = QueryHelpers.AddQueryString($"{_baseUrl}/api-v3/forex/candle", queryParams);

        //var response = await _httpClient.GetAsync(apiUrl_WithQuery);
        //if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
        //{
        //    string responseJsonString = await response.Content.ReadAsStringAsync();
        //}
        string responseJsonString = FcsDataSeed.CandleMultipleSymbols1h;

        var data = JsonSerializer.Deserialize<CandleData>(responseJsonString);

        if (data is not null && data.response.Count > 0)
        {
            var allowedSymbol = GetSymbolValue(symbol);

            var filteredList = data.response
                .Where(c => c.id == allowedSymbol)
                .ToList()
                .MapToCandleList();

            await _marketContext.AddRange(symbol, period, filteredList);
        }
    }

    /// <summary>
    /// check if history is necessary to be collected
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="period"></param>
    /// <returns></returns>
    private async Task<bool> IsHistoryRequired(string symbol, string period) => await _marketContext.IsHistoryRequired(symbol, period);


    Task<bool> IFcsService.IsHistoryRequired(string symbol, string period)
    {
        return IsHistoryRequired(symbol, period);
    }



    private string GetPeriodValue(string period)
    {
        switch (period)
        {
            case "1h":
                return "1h";
            default:
                return "1h";
        }
    }

    private string GetSymbolValue(string symbol)
    {
        switch (symbol)
        {
            case "1":
                return "1";
            default:
                return "1";
        }
    }

}
