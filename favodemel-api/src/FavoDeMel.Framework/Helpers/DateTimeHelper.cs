using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FavoDeMel.Framework.Helpers
{
    public static class DateTimeHelper
    {
        public static string TimeToString(TimeSpan timeSpan)
        {
            return timeSpan.ToString(@"hh\:mm\:ss", new CultureInfo("pT-BR"));
        }

        public static TimeSpan SumTime<TSource>(this IEnumerable<TSource> source, Func<TSource, TimeSpan> selector)
        {
            return source.Select(selector).Aggregate(TimeSpan.Zero, (t1, t2) => t1 + t2);
        }

        public static bool DataValida(params DateTime?[] datas)
        {
            return datas.All(data => data != null && data != DateTime.MinValue);
        }
    }
}
