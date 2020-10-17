using System.Collections.Generic;

namespace APIStarter.Domain.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue) =>
            dictionary.TryGetValue(key, out var value) ? value : defaultValue;
    }
}