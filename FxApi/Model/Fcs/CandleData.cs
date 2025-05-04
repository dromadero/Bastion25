namespace FxApi.Model.Fcs;

public record CandleData (
    bool status,
    int code,
    string msg,
    InfoData info,
    List<FcsCandleData> response
    ) : RootData(status, code, msg, info) ;




 