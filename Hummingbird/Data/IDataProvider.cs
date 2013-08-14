using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Hummingbird.Data
{
    public interface IDataProvider<T> : IDisposable
        where T : class, new()
    {
        IQueryable<T> Query(params Expression<Func<T, object>>[] includes);
        IEnumerable<T> Find(Expression<Func<T, bool>> query, params Expression<Func<T, object>>[] includes);
        void Delete(T item);
        void Delete(Expression<Func<T,bool>> query);
        T InsertOrUpdate(T item);
        IEnumerable<T> InsertOrUpdate(IEnumerable<T> items);
    }
}
