using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hummingbird.Data
{
    public interface IQueryableDataProvider<T> : IDataProvider<T>
        where T : class, new()
    {
        IQueryable<T> Query(params Expression<Func<T, object>>[] includes);
    }
}
