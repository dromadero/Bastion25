namespace FxApi.Model.Fcs;

public record HistoryData(
    bool status,
    int code,
    string msg,
    InfoData info,
    Dictionary<string, FcsHistoryData> response
    ) : RootData(status, code, msg, info);




