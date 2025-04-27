using System.Numerics;

namespace FxApi.Model;

public record  Candle(
    double o,
    double h,
    double l,
    double c,
    string v,
    int t,
    string tm
    );

 


