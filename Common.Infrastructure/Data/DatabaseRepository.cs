using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Common.Security;

namespace Common.Infrastructure.Data
{
    public class DatabaseRepository<T> : IRepository<T>
        where T : DataRow, new()
    {
        private DbContext _context;
        private ISecurityContext _securityContext;

        public DatabaseRepository(DbContext context, ISecurityContext securityContext)
        {
            _context = context;
            _securityContext = securityContext;
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> query)
        {
            return _context.Set<T>().Where(query);
        }

        public IQueryable<T> Find<TResult>(Expression<Func<T, bool>> query, params Expression<Func<T, TResult>>[] includes)
        {
            IQueryable<T> results = Find(query);

            if (includes != null)
            {
                results = includes.Aggregate(results,
                    (current, include) => current.Include(include));
            }

            return results;
        }

        public void Delete(T item)
        {
            _context.Set<T>().Remove(item);
            _context.SaveChanges();
        }

        public void Delete(Expression<Func<T, bool>> query)
        {
            Find(query).ToList().ForEach(item => Delete(item));
        }

        public void Add(T item)
        {
            item.CreatedBy = item.LastModifiedBy = _securityContext.CurrentUser;
            item.CreatedOn = item.LastModifiedOn = DateTime.Now.ToUniversalTime();
            _context.Set<T>().Add(item);
            _context.SaveChanges();
        }

        public void Add(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                Add(item);
            } 
        }

        public void Update(T item)
        {
            item.LastModifiedBy = _securityContext.CurrentUser;
            item.LastModifiedOn = DateTime.Now.ToUniversalTime();
            if (_context.Entry(item).State == System.Data.EntityState.Detached)
            {
                _context.Set<T>().Attach(item);
                _context.Entry(item).State = System.Data.EntityState.Modified;
            }
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
