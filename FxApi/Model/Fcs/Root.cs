namespace FxApi.Model.Fcs;

public record Root(
    bool status,
    int code,
    string msg,
    Dictionary<string, FcsCandle> response,
    Info info
    );




