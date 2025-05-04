
namespace FxApi.Model.Fcs;

public record FcsLatestData(
    string id,
    string o,
    string h,
    string l,
    string c,
    string up,
    string ch,
    string cp,
    int t,
    string s,
    string tm
    ) : FcsBaseCandleData(o, h, l, c, t, tm);


