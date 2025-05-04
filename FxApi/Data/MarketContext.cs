using FxApi.Model;
using FxApi.Model.Fcs;
using System.Threading.Tasks;

namespace FxApi.Data;

public class MarketContext : IMarketContext
{
    Dictionary<(string, string), SymbolContext> marketDictionary = new Dictionary<(string, string), SymbolContext>();

    public MarketContext() { }

    public void InitMarketSymbol(string symbol, string period)
    {
        EnsureDictionary(symbol, period);
    }

    public async Task AddRange(string symbol, string period, List<Candle> data)
    {
        EnsureDictionary(symbol, period);
        marketDictionary[(symbol, period)].AddRange(data);
    }

    public async Task<bool> AddRecord(string symbol, string period, Candle data)
    {
        EnsureDictionary(symbol, period);
        if (!await IsHistoryRequired(symbol, period))
        {
            return marketDictionary[(symbol, period)].AddValue(data);
        }
        return false;
    }

    public async Task<bool> IsHistoryRequired(string symbol, string period)
    {
        var result = marketDictionary.TryGetValue((symbol, period), out var symbolValue);

        if(result  && symbolValue is not null)
        {
            return symbolValue.Data.Count > 0;
        }
        return false;
        //return marketDictionary[(symbol, period)].Data.Count > 40;
    }

    private void EnsureDictionary(string symbol, string period)
    {
        if (!marketDictionary.TryGetValue((symbol, period), out _))
        {
            marketDictionary.Add(new(symbol, period), new SymbolContext(symbol, period));
        }
    }

 
}

 