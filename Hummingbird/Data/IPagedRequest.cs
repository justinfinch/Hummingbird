using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hummingbird.Data
{
    public interface IPagedRequest<T, TSortKey>
    {
        int PageNumber { get; }
        int PageSize { get; }
        Expression<Func<T, bool>> QueryExpression { get; }
        Expression<Func<T, TSortKey>> KeySelector { get; }
        IComparer<TSortKey> Comparer { get; }

        IPagedRequest<T, TSortKey> Page(int pageIndex);
        IPagedRequest<T, TSortKey> WithSize(int pageSize);
        IPagedRequest<T, TSortKey> Query(Expression<Func<T, bool>> query);
        IPagedRequest<T, TSortKey> OrderBy(Expression<Func<T, TSortKey>> keySelector, IComparer<TSortKey> comparer = null);
    }
}
