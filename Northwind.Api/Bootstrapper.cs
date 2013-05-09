
using System.Data.Entity;
using AutoMapper;
using Hummingbird.Data;
using Hummingbird.EntityFramework;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Ninject;
using Ninject;
using Northwind.Api.Dto;
using Northwind.DatabaseAccess;
using Northwind.Orders.Data;
using Northwind.Orders.Domain;

namespace Northwind.Api
{
    public class Bootstrapper : NinjectNancyBootstrapper
    {
        protected override void ApplicationStartup(IKernel container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            Mapper.CreateMap<Customer, CustomerDto>();

        }

        protected override void ConfigureRequestContainer(IKernel container, NancyContext context)
        {
            container.Bind(typeof(ICache<>)).To(typeof(InMemoryCache<>)).InSingletonScope();
            container.Bind(typeof(ICacheProvider<>)).To(typeof(CacheProvider<>)).InSingletonScope();

            //DbContext Bindings
            container.Bind<OrdersContext>().ToSelf().InSingletonScope();
            
            //DataProviders
            BindToOrdersDataProvider<Customer>(container);

            //Repositories
            container.Bind<ICustomerRepository>().To<CustomerRepository>();

        }

        private void BindDataProvider<T, T1>(IKernel container)
            where T : class, IObjectWithState, IDataRow, new()
            where T1 : DbContext
        {
            container.Bind<IDataProvider<T>>().To<DataProvider<T, T1>>().InSingletonScope();
        }

        private void BindToOrdersDataProvider<T>(IKernel container)
            where T : class, IObjectWithState, IDataRow, new()
        {
            BindDataProvider<T, OrdersContext>(container);
        }
    }
}