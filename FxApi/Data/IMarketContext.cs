using FxApi.Model;
using FxApi.Model.Fcs;

namespace FxApi.Data;

public interface IMarketContext
{
    Task AddRange(string symbol, string period, List<Candle> data);
    Task<bool> AddRecord(string symbol, string period, Candle data);
    void InitMarketSymbol(string symbol, string period);
    Task<bool> IsHistoryRequired(string symbol, string period);
    Task<List<Candle>> GetAsync(string symbol, string period);
}