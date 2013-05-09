using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace Hummingbird.Data
{
    public class InMemoryCache<T> : ICache<T>
    {
        private readonly MemoryCache _cache;
        public InMemoryCache()
        {
            _cache = MemoryCache.Default;
        }

        public T Get(string key)
        {
            return (T)_cache.Get(key);
        }

        public void Put(string key, T item)
        {
            _cache[key] = item;
        }

        public void Add(string key, T item, DateTime? absoluteExpiry = null, TimeSpan? relativeExpiry = null)
        {
            if (!absoluteExpiry.HasValue)
                absoluteExpiry = ObjectCache.InfiniteAbsoluteExpiration.DateTime;

            if (!relativeExpiry.HasValue)
                relativeExpiry = ObjectCache.NoSlidingExpiration;

            var policy = new CacheItemPolicy()
            {
                AbsoluteExpiration = absoluteExpiry.Value,
                SlidingExpiration = relativeExpiry.Value
            };

            _cache.Add(key, item, policy);
        }

        public IEnumerable<T> Get(Func<T,bool> cacheSearch, Func<IEnumerable<T>> retrieveData)
        {
            var cacheSearchResults = _cache.Select(t => (T)t.Value).Where(cacheSearch);

            if (cacheSearchResults.Any())
                return cacheSearchResults;
            else
                return retrieveData();
        }
    }
}
