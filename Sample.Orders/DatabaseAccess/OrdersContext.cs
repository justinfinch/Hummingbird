using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.Orders.Domain;
using Sample.DatabaseAccess;
using Hummingbird.EntityFramework;
using System.Data.Entity.ModelConfiguration;

namespace Sample.Orders.DatabaseAccess
{
    public class OrdersContext : BaseContext<OrdersContext>
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        public OrdersContext()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new OrderMap());
        }


    }
}
