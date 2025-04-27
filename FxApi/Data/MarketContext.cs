using FxApi.Model.Fcs;

namespace FxApi.Data;

public class MarketContext : IMarketContext
{
    Dictionary<(string, string), SymbolContext> marketDictionary = new Dictionary<(string, string), SymbolContext>();

    public MarketContext() { }

    public void InitMarketSymbol(string symbol, string period)
    {
        EnsureDictionary(symbol, period);
    }

    public void AddRange(string symbol, string period, Dictionary<string, FcsCandle> data)
    {
        EnsureDictionary(symbol, period);
        marketDictionary[(symbol, period)].AddRange(data);
    }

    public bool AddRecord(string symbol, string period, FcsCandle data)
    {
        EnsureDictionary(symbol, period);
        if (!IsHistoryRequired(symbol, period))
        {
            marketDictionary[(symbol, period)].AddValue(data);
            return true;
        }
        return false;
    }

    public bool IsHistoryRequired(string symbol, string period)
    {
        return marketDictionary[(symbol, period)].Data.Count > 40;
    }

    private void EnsureDictionary(string symbol, string period)
    {
        if (!marketDictionary.TryGetValue((symbol, period), out _))
        {
            marketDictionary.Add(new(symbol, period), new SymbolContext(symbol, period));
        }
    }

}

 