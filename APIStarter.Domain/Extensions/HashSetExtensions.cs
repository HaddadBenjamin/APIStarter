using System.Collections.Generic;

namespace APIStarter.Domain.Extensions
{
    public static class HashSetExtensions
    {
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer = default) => new HashSet<T>(source, comparer);
    }
}