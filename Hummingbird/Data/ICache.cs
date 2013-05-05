using System;
using System.Collections.Generic;

namespace Hummingbird.Data
{
    public interface ICache<T>
    {
        void Add(string key, T item, DateTime? absoluteExpiry=null, TimeSpan? relativeExpiry=null);
        T Get(string key);
        IEnumerable<T> Get(Func<T, bool> cacheSearch, Func<IEnumerable<T>> retrieveData);
        void Put(string key, T item);
    }
}
