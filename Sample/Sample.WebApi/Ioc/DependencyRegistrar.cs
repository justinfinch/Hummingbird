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
using Hummingbird.EntityFramework.Autofac;

namespace Sample.WebApi.Ioc
{
    public class DependencyRegistrar : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            //Bind Contexts - ensure these are only created once per request
            builder.RegisterType<OrdersContext>().InstancePerLifetimeScope();
            builder.RegisterType<EmployeesContext>().InstancePerLifetimeScope();

            //Bind domain objects to a context to create data providers
            builder.ToContextAsQueryable<Customer, OrdersContext>().InstancePerLifetimeScope();
            builder.ToContextAsQueryable<Order, OrdersContext>().InstancePerLifetimeScope();
            builder.ToContextAsQueryable<Employee, EmployeesContext>().InstancePerLifetimeScope();

            //Bind repositories
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>().InstancePerLifetimeScope();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>().InstancePerLifetimeScope();

            //Register Services
            builder.RegisterType<UnitOfWorkFactory>().As<IUnitOfWorkFactory>().InstancePerLifetimeScope();
        }
    }
}