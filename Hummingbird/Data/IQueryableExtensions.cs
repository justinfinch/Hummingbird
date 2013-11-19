//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Hummingbird.Data
//{
//    public static class IQueryableExtensions
//    {
//        public static IQueryable<T> AsExpandable<T>(this IQueryable<T> query)
//        {
//            if (query is ExpandableQuery<T>) return (ExpandableQuery<T>)query;
//            return new ExpandableQuery<T>(query);
//        }
//    }
//}
