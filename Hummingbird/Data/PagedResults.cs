using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hummingbird.Data
{
    public class PagedResults<T> : IPagedResults<T>
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public IEnumerable<T> Results { get; private set; }

        public PagedResults(IEnumerable<T> results, int pageSize, int pageNumber, int totalCount)
        {
            Results = results;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = (totalCount + pageSize - 1) / pageSize;
        }
    }
}
