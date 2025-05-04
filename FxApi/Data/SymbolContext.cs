using FxApi.Mapping;
using FxApi.Model;
using FxApi.Model.Fcs;

namespace FxApi.Data;

public class SymbolContext
{
    public string Period { get; init; }
    public string Symbol { get; init; }

    public Dictionary<int, Candle> DataDictionary
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

    public bool AddValue(Candle candle)
    {
        return _dataDictionary.TryAdd(candle.t, candle);
    }

    public void AddRange(List<Candle> data)
    {
        foreach (var item in data)
        {
            AddValue(item);
        }
    }

    //public void AddRange(Dictionary<string, FcsHistoryData> data)
    //{
    //    foreach (var item in data)
    //    {
    //        AddValue(item.Value);
    //    }
    //}

}
