using FxApi.Model;

namespace FxApi.Services;



public interface IFcsService
{
    public Task TryCollectData(string symbol, string period);

    public Task<bool> IsHistoryRequired(string symbol, string period);

    public Task<List<Candle>> Get(string symbol, string period);
}