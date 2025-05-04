namespace FxApi.Model.Fcs;

public record FcsCandleData(
    string id,
    string o,
    string h,
    string l,
    string c,
    string t,
    string up,
    string ch,
    string cp,
    string s,
    string tm
    ) : FcsBaseCandleData(o, h, l, c, int.Parse(t), tm);

