using FxApi.Model;
using FxApi.Model.Fcs;
using System.Collections.Generic;
using System.Globalization;

namespace FxApi.Mapping;

public static class LogItemMapping
{
    public static Candle MapToCandle(this FcsCandleData fcs)
    {
        return new Candle(
            Double.Parse(fcs.o, CultureInfo.InvariantCulture),
            Double.Parse(fcs.h, CultureInfo.InvariantCulture),
            Double.Parse(fcs.l, CultureInfo.InvariantCulture),
            Double.Parse(fcs.c, CultureInfo.InvariantCulture),
            string.Empty,
            fcs.tt,
            fcs.tm
            );
    }

    public static Candle MapToCandle(this FcsHistoryData fcs)
    {
        return new Candle(
            Double.Parse(fcs.o, CultureInfo.InvariantCulture),
            Double.Parse(fcs.h, CultureInfo.InvariantCulture),
            Double.Parse(fcs.l, CultureInfo.InvariantCulture),
            Double.Parse(fcs.c, CultureInfo.InvariantCulture),
            string.Empty,
            fcs.tt,
            fcs.tm
            );
    }


    public static List<Candle> MapToCandleList(this List<FcsHistoryData> list)
    {
        List<Candle> result = new List<Candle>();
        result = list.Select(c => c.MapToCandle()).ToList();
        return result; 
    }

    public static List<Candle> MapToCandleList(this List<FcsCandleData> list)
    {
        List<Candle> result = new List<Candle>();
        result = list.Select(c => c.MapToCandle()).ToList();
        return result;
    }
}