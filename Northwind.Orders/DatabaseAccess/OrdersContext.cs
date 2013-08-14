using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwind.Orders.Domain;
using Northwind.DatabaseAccess;

namespace Northwind.Orders.DatabaseAccess
{
    public class OrdersContext : NorthwindBaseContext<OrdersContext>
    {
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new CustomerMap());
        }


    }
}
