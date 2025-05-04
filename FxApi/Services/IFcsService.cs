namespace FxApi.Services;



public interface IFcsService
{
    public Task TryCollectData(string symbol, string period);

    public Task<bool> IsHistoryRequired(string symbol, string period);
}