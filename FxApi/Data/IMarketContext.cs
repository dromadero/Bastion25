using FxApi.Model.Fcs;

namespace FxApi.Data;

public interface IMarketContext
{
    void AddRange(string symbol, string period, Dictionary<string, FcsCandle> data);
    bool AddRecord(string symbol, string period, FcsCandle data);
    void InitMarketSymbol(string symbol, string period);
    bool IsHistoryRequired(string symbol, string period);
}