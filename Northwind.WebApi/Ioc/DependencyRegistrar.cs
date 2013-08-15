using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hummingbird.Data;
using Hummingbird.EntityFramework;
using Northwind.Orders.DatabaseAccess;
using Northwind.Orders.Domain;
using Northwind.Orders.Data;
using Autofac;

namespace Northwind.WebApi.Ioc
{
    public class DependencyRegistrar : Module
    {
        
        protected override void Load(ContainerBuilder builder)
        {
            //Bind Contexts - ensure these are only created once per request
            builder.RegisterType<OrdersContext>().InstancePerLifetimeScope();

            //Bind domain objects to a context to create data providers
            RegisterToContext<Customer, OrdersContext>(builder);
            RegisterToContext<Order, OrdersContext>(builder);

            //Bind repositories
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>().InstancePerLifetimeScope();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>().InstancePerLifetimeScope();
        }

        //TODO: Move this into an EF Autofac dll
        protected void RegisterToContext<T, TContext>(ContainerBuilder builder)
            where T : class, IObjectWithState, IDataRow, new()
            where TContext : DbContext
        {
            builder.RegisterType<DataProvider<T, TContext>>().As<IDataProvider<T>>().InstancePerLifetimeScope();
        }
    }
}
