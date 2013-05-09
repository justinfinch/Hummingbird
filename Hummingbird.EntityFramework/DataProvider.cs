using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Hummingbird.Data;

namespace Hummingbird.EntityFramework
{
    public class DataProvider<T, U> : IDataProvider<T>
        where T : class, IObjectWithState, IDataRow, new()
        where U : DbContext
    {
        private readonly U _context;
        public U Context
        {
            get
            {
                return _context;
            }
        }

        public DataProvider(U dbContext)
        {
            _context = dbContext;
        }

        public IQueryable<T> Query()
        {
            return Context.Set<T>();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> query, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> results = _context.Set<T>().Where(query);

            if (includes != null)
            {
                results = includes.Aggregate(results,
                    (current, include) => current.Include(include));
            }
            
            return results.ToList();
        }

        public void Delete(T item)
        {
            _context.Set<T>().Remove(item);
            _context.SaveChanges();
        }

        //Note: If you want cascade deleting you need to ensure your navigation properties are loaded
        public void Delete(Expression<Func<T, bool>> query)
        {
            Find(query).ToList().ForEach(Delete);
        }

        public T InsertOrUpdateGraph(T item)
        {
            _context.Set<T>().Add(item);
            _context.ApplyStateChanges();
            _context.SaveChanges();
            return item;
        }


        public bool Exists(Expression<Func<T, bool>> query)
        {
            return _context.Set<T>().Any(query);
        }

        [Obsolete("Use InsertOrUpdateGraph instead!!")]
        public T InsertOrUpdate(T item)
        {
            if (!item.HasKey())
            {
                _context.Entry(item).State = EntityState.Added;
            }
            else
            {
                _context.Entry(item).State = EntityState.Modified;
            }
            _context.SaveChanges();
            return item;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
