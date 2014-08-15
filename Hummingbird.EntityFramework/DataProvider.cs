using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
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
            IQueryable<T> results = _context.Set<T>().Where(query); 

            if (includes != null && includes.Length > 0)
            {
                results = includes.Aggregate(results,
                    (current, include) => current.Include(include));
            }

            return results.ToList();
        }

        public IPagedResults<T> FindPage<TSortKey>(IPagedRequest<T, TSortKey> pagedRequest, params Expression<Func<T, object>>[] includes)
        {
            if(pagedRequest.KeySelector == null)
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

            return new PagedResults<T>(query.ToList(), pagedRequest.PageSize, pagedRequest.PageNumber, totalCount);
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

        public void Delete(Expression<Func<T, bool>> query)
        {
            Find(query).ToList().ForEach(Delete);
        }

        public T Save(T item)
        {
            _context.Set<T>().Add(item);
            _context.ApplyStateChanges();
            _context.SaveChanges();
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

        public IEnumerable<T> Execute(string sprocName, object parameters)
        {
            var arguments = parameters.PrepareSprocArguments(sprocName);
            return _context.Set<T>().SqlQuery(arguments.Item1, arguments.Item2).ToList();
        }

        public void ExecuteNonQuery(string sprocName, object parameters)
        {
            var arguments = parameters.PrepareSprocArguments(sprocName);
            _context.Database.ExecuteSqlCommand(arguments.Item1, arguments.Item2);
        }

        public void Dispose()
        {
            _context.Dispose();
        }


        
    }
}

