using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hummingbird.Data
{
    public interface IPagedResults<T>
    {
        int PageNumber { get; }
        int TotalPages { get; }
        int PageSize { get; }

        IEnumerable<T> Results { get; } 
    }
}
