using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Common.Infrastructure.Data
{
    public interface IRepository<T> : IDisposable
        where T : class, new()
    {
        IQueryable<T> Find(Expression<Func<T, bool>> query);
        IQueryable<T> Find<TResult>(Expression<Func<T, bool>> query, params Expression<Func<T, TResult>>[] includes);
        void Delete(T item);
        void Delete(Expression<Func<T,bool>> query);
        void Add(T item);
        void Add(IEnumerable<T> items);
        void Update(T item);

        //TODO:  Need a way to execute stored procs
        //object ExecuteCommand(string command, dynamic input);
    }
}
