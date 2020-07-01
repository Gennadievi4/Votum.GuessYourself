using System.Collections.Concurrent;
using System.ComponentModel;

namespace Guess.Yourself
{
    public static class PropertyChangedEventArgsCache
    {
        private static readonly ConcurrentDictionary<string, PropertyChangedEventArgs> _cache = new ConcurrentDictionary<string, PropertyChangedEventArgs>();

        public static PropertyChangedEventArgs Get(string propertyName)
        {
            if (!_cache.TryGetValue(propertyName, out PropertyChangedEventArgs result))
            {
                result = new PropertyChangedEventArgs(propertyName);
                _cache.TryAdd(propertyName, result);
            }
            return result;
        }
    }
}
