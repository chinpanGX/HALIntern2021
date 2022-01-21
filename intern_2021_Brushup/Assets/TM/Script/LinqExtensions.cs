using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.TM.Script
{
    public static class LinqExtensions
    {
        public static T Random<T>(this IEnumerable<T> enumerable)
        {
            Debug.Assert(enumerable != null);

            if (!enumerable.Any()) return default(T);

            var list = enumerable as IList<T> ?? enumerable.ToList();
            return list.Count == 0 ? default(T) : list[UnityEngine.Random.Range(0, list.Count)];
        }
    }
}