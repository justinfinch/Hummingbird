using System;
using System.Collections.Generic;

namespace Hummingbird.Data
{
    public class CacheProvider<T> : ICacheProvider<T>
    {
        private ICache<T> _cache;
        public CacheProvider(ICache<T> cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// Fetches the object associated with the <paramref name="key"/> from the cache. 
        /// If it's not located in the cache, <paramref name="retrieveData"/> is called.
        /// </summary>
        /// <param name="key">Globally unique identifier for the object</param>
        /// <param name="retrieveData">Delegate to call for cache pass-through</param>
        /// <returns>The object that was either found in the cache or retrieved using <paramref name="retrieveData"/></returns>
        public T Fetch(string key, Func<T> retrieveData)
        {
            return FetchAndCache(key, retrieveData);
        }

        public IEnumerable<T> Fetch(string key, Func<IEnumerable<T>> retrieveData)
        {
            return FetchAndCache(key, retrieveData);
        }

        private T FetchAndCache(string key, Func<T> retrieveData)
        {
            T value;
            if (!TryGetValue(key, out value))
            {
                value = retrieveData();

                _cache.Add(key, value, null, null);
            }
            return value;
        }

        private IEnumerable<T> FetchAndCache(string key, Func<IEnumerable<T>> retrieveData)
        {
            IEnumerable<T> values;
            if (!TryGetValue(key, out values))
            {
                values = retrieveData();

                foreach (var value in values)
                {
                    _cache.Add(key, value, null, null);
                }
            }
            return values;
        }

        private bool TryGetValue<T>(string key, out T value)
        {
            object cachedValue = _cache.Get(key);
            if (cachedValue == null)
            {
                value = default(T);
                return false;
            }
            else
            {
                try
                {
                    value = (T)cachedValue;
                    return true;
                }
                catch
                {
                    value = default(T);
                    return false;
                }
            }
        }

        public IEnumerable<T> Find(Func<T,bool> searchCache, Func<IEnumerable<T>> retrieveData)
        {
            return _cache.Get(searchCache, retrieveData);
        }
    }
}
