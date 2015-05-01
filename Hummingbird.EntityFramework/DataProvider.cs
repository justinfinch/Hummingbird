using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hummingbird.Data;

namespace Hummingbird.EntityFramework
{
    public class DataProvider<T, U> : IQueryableDataProvider<T>
        where T : class, IObjectWithState, new()
        where U : DbContext
    {
        protected U _context;
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

        public IEnumerable<T> Find(Expression<Func<T, bool>> query, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                return FindAsync(query, includes).Result;
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }
            
        }

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> query, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> results = _context.Set<T>().Where(query);

            if (includes != null && includes.Length > 0)
            {
                results = includes.Aggregate(results,
                    (current, include) => current.Include(include));
            }

            return await results.ToListAsync();
        }

        public IPagedResults<T> FindPage<TSortKey>(IPagedRequest<T, TSortKey> pagedRequest, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                return FindPageAsync(pagedRequest, includes).Result;
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }
        }

        public async Task<IPagedResults<T>> FindPageAsync<TSortKey>(IPagedRequest<T, TSortKey> pagedRequest, params Expression<Func<T, object>>[] includes)
        {
            if (pagedRequest.KeySelector == null)
                throw new ArgumentException("Order By must be set for a paged result set.  Call the OrderBy method on the PagedRequest to set.");

            //Build the simple query
            IQueryable<T> query = _context.Set<T>().Where(pagedRequest.QueryExpression);

            if (pagedRequest.Comparer != null)
            {
                switch (pagedRequest.SortDirection)
                {
                    case SortDirection.Ascending:
                        query = query.OrderBy(pagedRequest.KeySelector, pagedRequest.Comparer);
                        break;
                    default:
                        query = query.OrderByDescending(pagedRequest.KeySelector, pagedRequest.Comparer);
                        break;
                }
            }
            else
            {
                switch (pagedRequest.SortDirection)
                {
                    case SortDirection.Ascending:
                        query = query.OrderBy(pagedRequest.KeySelector);
                        break;
                    default:
                        query = query.OrderByDescending(pagedRequest.KeySelector);
                        break;
                }
            }

            //Get a count of matching records
            int totalCount = query.Count();

            query = query.Skip((pagedRequest.PageNumber - 1) * pagedRequest.PageSize).Take(pagedRequest.PageSize);


            if (includes != null && includes.Length > 0)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            var results = await query.ToListAsync();

            return new PagedResults<T>(results, pagedRequest.PageSize, pagedRequest.PageNumber, totalCount);
        }

        public IQueryable<T> Query(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> set = _context.Set<T>();

            if (includes != null && includes.Length > 0)
            {
                set = includes.Aggregate(set,
                    (current, include) => current.Include(include));
            }

            return set;
        }

        public void Delete(T item)
        {
            _context.Set<T>().Remove(item);
            _context.SaveChanges();
        }

        public async Task<bool> DeleteAsync(T item)
        {
            _context.Set<T>().Remove(item);
            await _context.SaveChangesAsync();

            return true;
        }

        public void Delete(Expression<Func<T, bool>> query)
        {
            Find(query).ToList().ForEach(Delete);
        }

        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> query)
        {
            var result = true;
            var itemsToDelete = await FindAsync(query);

            foreach (var itemToDelete in itemsToDelete)
            {
                result = await DeleteAsync(itemToDelete);
            }

            return result;
        }

        public T Save(T item)
        {
            _context.Set<T>().Add(item);
            _context.ApplyStateChanges();
            _context.SaveChanges();
            _context.ResetStateChanges();
            return item;
        }

        public async Task<T> SaveAsync(T item)
        {
            _context.Set<T>().Add(item);
            _context.ApplyStateChanges();

            await _context.SaveChangesAsync();
            
            _context.ResetStateChanges();
            return item;
        }

        public IEnumerable<T> Save(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                _context.Set<T>().Add(item);
            }

            _context.ApplyStateChanges();
            _context.SaveChanges();
            _context.ResetStateChanges();
            return items;
        }

        public async Task<IEnumerable<T>> SaveAsync(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                _context.Set<T>().Add(item);
            }
            _context.ApplyStateChanges();

            await _context.SaveChangesAsync();

            _context.ResetStateChanges();
            return items;
        }

        public IEnumerable<T> Execute(string sprocName, object parameters)
        {
            try
            {
                return ExecuteAsync(sprocName, parameters).Result;
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }
        }

        public async Task<IEnumerable<T>> ExecuteAsync(string sprocName, object parameters)
        {
            var arguments = parameters.PrepareSprocArguments(sprocName);
            return await _context.Set<T>().SqlQuery(arguments.Item1, arguments.Item2).ToListAsync();
        }

        public void ExecuteNonQuery(string sprocName, object parameters)
        {
            try
            {
                var result = ExecuteNonQueryAsync(sprocName, parameters).Result;
            }
            catch (AggregateException ae)
            {
                throw ae.Flatten();
            }
        }

        public async Task<bool> ExecuteNonQueryAsync(string sprocName, object parameters)
        {
            var arguments = parameters.PrepareSprocArguments(sprocName);
            await _context.Database.ExecuteSqlCommandAsync(arguments.Item1, arguments.Item2);

            return true;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        
    }
}

