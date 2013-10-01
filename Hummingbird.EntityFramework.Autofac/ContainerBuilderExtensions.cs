using Hummingbird.Data;
using Hummingbird.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Builder;

namespace Hummingbird.EntityFramework.Autofac
{
    public static class ContainerBuilderExtensions
    {
        public static IRegistrationBuilder<DataProvider<T, TContext>, ConcreteReflectionActivatorData, SingleRegistrationStyle> ToContext<T, TContext>(this ContainerBuilder builder)
            where T : class, IObjectWithState, new()
            where TContext : DbContext
        {
            return builder.RegisterType<DataProvider<T, TContext>>().As<IQueryableDataProvider<T>>();
        }

        public static IRegistrationBuilder<DataProvider<T, TContext>, ConcreteReflectionActivatorData, SingleRegistrationStyle> ToContextAsQueryable<T, TContext>(this ContainerBuilder builder)
            where T : class, IObjectWithState, new()
            where TContext : DbContext
        {
            return builder.RegisterType<DataProvider<T, TContext>>().As<IQueryableDataProvider<T>>();
        }
    }
}
