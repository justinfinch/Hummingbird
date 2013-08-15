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
            //Bind Contexts
            Bind<OrdersContext>().ToSelf().InRequestScope();

            //Bind types to a context to create data providers
            BindToOrdersContext<Customer>();

            //Bind repositories
            Bind<ICustomerRepository>().To<CustomerRepository>().InRequestScope();
        }


        protected void BindToContext<T, TContext>()
            where T : class, IObjectWithState, IDataRow, new()
            where TContext : DbContext
        {
            Bind<IDataProvider<T>>().To<DataProvider<T, TContext>>().InRequestScope();
        }

        protected void BindToOrdersContext<T>()
            where T : class, IObjectWithState, IDataRow, new()
        {
            BindToContext<T, OrdersContext>();
        }
    }
}
