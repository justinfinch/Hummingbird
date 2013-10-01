using Hummingbird.Data;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Configuration.DSL.Expressions;
using StructureMap.Pipeline;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hummingbird.EntityFramework.StructureMap
{
    public static class IRegistryExtensions
    {
        public static SmartInstance<DataProvider<T, TContext>> ToContext<T, TContext>(this IRegistry registry)
            where T : class, IObjectWithState, new()
            where TContext : DbContext
        {
            return registry.For<IDataProvider<T>>().HttpContextScoped().Use<DataProvider<T, TContext>>();
        }

        public static SmartInstance<DataProvider<T, TContext>> ToContextAsQueryable<T, TContext>(this IRegistry registry)
            where T : class, IObjectWithState, new()
            where TContext : DbContext
        {
            return registry.For<IQueryableDataProvider<T>>().HttpContextScoped().Use<DataProvider<T, TContext>>();
        }
    }
}


