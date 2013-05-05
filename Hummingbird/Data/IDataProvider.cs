using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Hummingbird.Data
{
    public interface IDatabaseProvider<T> : IDisposable
        where T : class, new()
    {
        IEnumerable<T> Find(Expression<Func<T, bool>> query, params Expression<Func<T, object>>[] includes);
        void Delete(T item);
        void Delete(Expression<Func<T,bool>> query);
        T InsertOrUpdateGraph(T item);
        T InsertOrUpdate(T item);
        bool Exists(Expression<Func<T, bool>> query);
    }
}
