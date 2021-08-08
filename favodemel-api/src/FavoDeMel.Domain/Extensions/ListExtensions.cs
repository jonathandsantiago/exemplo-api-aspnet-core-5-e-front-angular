using System;
using System.Collections.Generic;
using System.Linq;

namespace FavoDeMel.Domain.Extensions
{
    public static class ListExtensions
    {
        public static IList<TResult> SelectList<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            return source.Select(selector).ToList();
        }

        public static TResult[] SelectArray<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            return source.Select(selector).ToArray();
        }

        public static T GetSetting<T>(this IDictionary<string, object> keys)
        {
            return (T)keys[typeof(T).Name];
        }
    }
}
