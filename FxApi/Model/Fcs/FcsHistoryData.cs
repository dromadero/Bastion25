
namespace FxApi.Model.Fcs;

public record FcsHistoryData(
    string o,
    string h,
    string l,
    string c,
    string v,
    int t,
    string tm
    ) : FcsBaseCandleData(o, h, l, c, t, tm);


