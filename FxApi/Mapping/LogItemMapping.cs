using FxApi.Model;
using FxApi.Model.Fcs;
using System.Globalization;

namespace FxApi.Mapping;

public static class LogItemMapping
{
    public static Candle MapToCandle(this FcsCandle fcs)
    {
        return new Candle(
            Double.Parse(fcs.o, CultureInfo.InvariantCulture), 
            Double.Parse(fcs.h, CultureInfo.InvariantCulture),
            Double.Parse(fcs.l, CultureInfo.InvariantCulture),  
            Double.Parse(fcs.c, CultureInfo.InvariantCulture), 
            fcs.v, 
            fcs.t, 
            fcs.tm
            );

            //o = Double.Parse(fcs.o),
            //h = Double.Parse(fcs.h),
            //l = Double.Parse(fcs.l),
            //c = Double.Parse(fcs.c),
            //t = fcs.t,
            //tm = fcs.tm,
            //v = fcs.v 
    }

}