using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Extensions
{
    public static class ListExtension
    {
        public static IList<TSource> RemoveAndGet<TSource>(this IList<TSource> list, Func<TSource, bool> predicate)
        {
            lock (list)
            {
                List<TSource> result = new List<TSource>();
                int length = list.Count();
                for (int i = 0; i < length; i++)
                {
                    var item = list.ElementAt(i);
                    if (predicate(item))
                    {
                        result.Add(item);
                        list.RemoveAt(i);
                        i--;
                        length--;
                    }
                }
                return result;
            }
        }
    }
}