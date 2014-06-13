using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Tretton37.Knowabunga.Core.Extensions
{
    public static class EnumerableExtensions
    {
        private static readonly ThreadLocal<Random> Random = new ThreadLocal<Random>(() => new Random());

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
        {
            return enumerable
                .Select(x => new {Random = Random.Value.Next(), Item = x})
                .OrderBy(x => x.Random)
                .Select(x => x.Item);
        }
    }
}