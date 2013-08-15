using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Web.Common;
using Hummingbird.Data;
using Hummingbird.EntityFramework;
using Northwind.Orders.DatabaseAccess;
using Ninject.Web.Common;
using Northwind.Orders.Domain;
using Northwind.Orders.Data;

namespace Northwind.WebApi.Ioc
{
    public class DependencyRegistrar : NinjectModule
    {

        public override void Load()
        {
            //Bind Contexts - ensure these are only created once per request
            Bind<OrdersContext>().ToSelf().InRequestScope();

            //Bind domain objects to a context to create data providers
            BindToContext<Customer, OrdersContext>();

            //Bind repositories
            Bind<ICustomerRepository>().To<CustomerRepository>().InRequestScope();
        }

        //TODO: Move this into an EF Ninject dll
        protected void BindToContext<T, TContext>()
            where T : class, IObjectWithState, IDataRow, new()
            where TContext : DbContext
        {
            Bind<IDataProvider<T>>().To<DataProvider<T, TContext>>().InRequestScope();
        }
    }
}
