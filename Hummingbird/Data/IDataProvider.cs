using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Hummingbird.Data
{
    public interface IDataProvider<T> : IDisposable
        where T : class, new()
    {
        IEnumerable<T> Find(Expression<Func<T, bool>> query, params Expression<Func<T, object>>[] includes);
        void Delete(T item);
        void Delete(Expression<Func<T,bool>> query);
        T Save(T item);
        IEnumerable<T> Save(IEnumerable<T> items);
        IEnumerable<T> Execute(string sprocName, object parameters);
        void ExecuteNonQuery(string sprocName, object parameters);
    }
}
