using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hummingbird.Data
{
    public interface IDataProvider<T> : IDisposable
        where T : class, new()
    {
        IEnumerable<T> Find(Expression<Func<T, bool>> query, params Expression<Func<T, object>>[] includes);
        IPagedResults<T> FindPage<TSortKey>(IPagedRequest<T, TSortKey> pagedRequest, params Expression<Func<T, object>>[] includes);
        void Delete(T item);
        void Delete(Expression<Func<T,bool>> query);
        T Save(T item);
        IEnumerable<T> Save(IEnumerable<T> items);
        IEnumerable<T> Execute(string sprocName, object parameters);
        void ExecuteNonQuery(string sprocName, object parameters);

        Task<List<T>> FindAsync(Expression<Func<T, bool>> query, params Expression<Func<T, object>>[] includes);
        Task<IPagedResults<T>> FindPageAsync<TSortKey>(IPagedRequest<T, TSortKey> pagedRequest, params Expression<Func<T, object>>[] includes);
        Task<bool> DeleteAsync(T item);
        Task<bool> DeleteAsync(Expression<Func<T, bool>> query);
        Task<T> SaveAsync(T item);
        Task<IEnumerable<T>> SaveAsync(IEnumerable<T> items);
        Task<IEnumerable<T>> ExecuteAsync(string sprocName, object parameters);
        Task<bool> ExecuteNonQueryAsync(string sprocName, object parameters);
    }
}
