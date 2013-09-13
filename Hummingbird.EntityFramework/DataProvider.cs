using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Hummingbird.Data;
using System.Reflection;
using System.Data.SqlClient;

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

        //public IPager<T> FindPage(Expression<Func<T, bool>> query, int offset, int pageSize, params Expression<Func<T, object>>[] includes)
        //{
        //    IQueryable<T> results = _context.Set<T>().Where(query).Take(pageSize).Skip(offest * pageSize);

        //    if (includes != null && includes.Length > 0)
        //    {
        //        results = includes.Aggregate(results,
        //            (current, include) => current.Include(include));
        //    }

        //    return results.ToList();
        //}

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
            return items;
        }

        public IEnumerable<T> Execute(string storedProcedure, object parameters)
        {
            var arguments = PrepareArguments(storedProcedure, parameters);
            return _context.Set<T>().SqlQuery(arguments.Item1, arguments.Item2).ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        private static Tuple<string, object[]> PrepareArguments(string storedProcedure, object parameters)
        {
            var parameterNames = new List<string>();
            var parameterValues = new List<object>();

            if (parameters != null)
            {
                foreach (PropertyInfo propertyInfo in parameters.GetType().GetProperties())
                {
                    string name = "@" + propertyInfo.Name;
                    object value = propertyInfo.GetValue(parameters, null);

                    parameterNames.Add(name);
                    parameterValues.Add(new SqlParameter(name, value ?? DBNull.Value));
                }
            }

            if (parameterNames.Count > 0)
                storedProcedure += " " + string.Join(", ", parameterNames);

            return new Tuple<string, object[]>(storedProcedure, parameterValues.ToArray());
        }
    }
}

