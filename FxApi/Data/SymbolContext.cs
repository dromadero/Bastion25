using FxApi.Mapping;
using FxApi.Model;
using FxApi.Model.Fcs;

namespace FxApi.Data;

public class SymbolContext
{
    public string Period { get; init; }
    public string Symbol { get; init; }

    public Dictionary<int, Candle> Data
    {
        get
        {
            return _dataDictionary;
        }
    }

    Dictionary<int, Candle> _dataDictionary = new Dictionary<int, Candle>();

    public SymbolContext(string period, string symbol)
    {
        Period = period;
        Symbol = symbol;
    }

    public void AddValue(FcsCandle candle)
    {
        _dataDictionary.TryAdd(candle.t, candle.MapToCandle());
    }

    public void AddRange(Dictionary<string, FcsCandle> data)
    {
        foreach (var item in data)
        {
            AddValue(item.Value);
        }
    }


}
