using System;
using System.Collections.Generic;
using System.Linq;

namespace FavoDeMel.Framework.Extensions
{
    public static class ListExtension
    {
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> items)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            if (items == null) throw new ArgumentNullException(nameof(items));

            if (list is List<T> asList)
            {
                asList.AddRange(items);
            }
            else
            {
                foreach (var item in items)
                {
                    list.Add(item);
                }
            }
        }

        public static void RemoveAll<T>(this IList<T> list, Predicate<T> predicate)
        {
            int i = list.IndexOf(predicate);

            while (i >= 0)
            {
                list.RemoveAt(i);
                i = list.IndexOf(predicate);
            }
        }

        public static int IndexOf<T>(this IEnumerable<T> list, Predicate<T> predicate)
        {
            int i = -1;

            if (list == null)
            {
                return i;
            }

            return list.Any(x =>
            {
                i++;
                return predicate(x);
            }
            ) ? i : -1;
        }
    }
}
