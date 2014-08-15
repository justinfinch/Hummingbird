using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Hummingbird.Domain;

namespace Hummingbird.Data
{
    public class PagedRequest<T, TSortKey> : IPagedRequest<T, TSortKey>
        where T : class, IEntity
    {
        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public Expression<Func<T, bool>> QueryExpression { get; private set; }

        public Expression<Func<T, TSortKey>> KeySelector { get; private set; }

        public IComparer<TSortKey> Comparer { get; private set; }

        public SortDirection SortDirection { get; private set; }

        public PagedRequest()
        {
            SortDirection = SortDirection.Ascending;
        }

        public IPagedRequest<T, TSortKey> Page(int pageIndex)
        {
            PageNumber = pageIndex;
            return this;
        }

        public IPagedRequest<T, TSortKey> WithSize(int pageSize)
        {
            PageSize = pageSize;
            return this;
        }

        public IPagedRequest<T, TSortKey> Query(Expression<Func<T, bool>> query)
        {
            QueryExpression = query;
            return this;
        }

        public IPagedRequest<T, TSortKey> OrderBy(Expression<Func<T, TSortKey>> keySelector, IComparer<TSortKey> comparer)
        {
            KeySelector = keySelector;
            Comparer = comparer;
            return this;
        }

        public IPagedRequest<T, TSortKey> Ascending()
        {
            SortDirection = SortDirection.Ascending;
            return this;
        }

        public IPagedRequest<T, TSortKey> Descending()
        {
            SortDirection = SortDirection.Descending;
            return this;
        }
    }
}
