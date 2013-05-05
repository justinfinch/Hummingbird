using System;
using System.Collections.Generic;

namespace Hummingbird.Data
{
    public interface ICacheProvider<T>
    {
        T Fetch(string key, Func<T> retrieveData);
        IEnumerable<T> Fetch(string key, Func<IEnumerable<T>> retrieveData);

        IEnumerable<T> Find(Func<T, bool> searchCache, Func<IEnumerable<T>> retrieveData);
    }
}
