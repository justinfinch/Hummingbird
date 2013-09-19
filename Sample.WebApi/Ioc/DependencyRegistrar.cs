using Autofac;
using Hummingbird.Data;
using Hummingbird.EntityFramework;
using Sample.Employees.Data;
using Sample.Employees.DatabaseAccess;
using Sample.Employees.Domain;
using Sample.Orders.Data;
using Sample.Orders.DatabaseAccess;
using Sample.Orders.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Sample.WebApi.Ioc
{
    public class DependencyRegistrar : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            //Bind Contexts - ensure these are only created once per request
            builder.RegisterType<OrdersContext>().InstancePerLifetimeScope();
            builder.RegisterType<EmployeesContext>().InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWorkFactory>().As<IUnitOfWorkFactory>().InstancePerLifetimeScope();

            //Bind domain objects to a context to create data providers
            RegisterToContext<Customer, OrdersContext>(builder);
            RegisterToContext<Order, OrdersContext>(builder);
            RegisterToContext<Employee, EmployeesContext>(builder);

            //Bind repositories
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>().InstancePerLifetimeScope();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>().InstancePerLifetimeScope();
        }

        //TODO: Move this into an EF Autofac dll
        protected void RegisterToContext<T, TContext>(ContainerBuilder builder)
            where T : class, IObjectWithState, new()
            where TContext : DbContext
        {
            builder.RegisterType<DataProvider<T, TContext>>().As<IQueryableDataProvider<T>>().InstancePerLifetimeScope();
        }
    }
}